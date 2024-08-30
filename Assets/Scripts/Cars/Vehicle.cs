using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;

namespace Cars
{
    public class Vehicle : NetworkBehaviour
    {

        private List<GameObject> _missiles;
        
        public bool rocketAlert;

        public CarEngine[] engines;
        
        public Transform[] wheels;
        
        [SyncVar]
        public byte hp = 255;

        [SyncVar]
        public bool isExploded;

        [Tooltip("Добавьте сюда объекты которые могут оторваться при взрыве.")]
        public GameObject[] components;

        [Range(25, 100)]
        [Tooltip("Шанс отрыва объектов")]
        public int chance = 25;
        
        [Tooltip("Добавьте сюда места где будет появлятся огонь после взрыва.")]
        public Transform[] firePlaces;
        
        #nullable enable
        public Light[]? farLights;
        public Light[]? headLights;
        public Light? rightTurnSignal;
        public Light? leftTurnSignal;
        public Light[]? rearLightsRed;
        public Light[]? rearLightsWhite;
        public Light[]? neonSets;
        #nullable disable
        public Color neonSetsColor;
        public Color headLightsColor;
        public Color farLightsColor;
        
        // [Tooltip("Максимальное количество игроков в машине. (Включая водителя).")]
        // public byte maxPlayersInTheVehicle;

        public PlaceInTheVehicle driverPlace;
        public PlaceInTheVehicle[] passengerPlaces;
        
        public Player.Player driver;
        public List<Player.Player> passengers;

        public Rigidbody rigidbody;


        [Command(requiresAuthority = false)]
        public void CmdPlayMissileAimAlert()
        {
            RpcPlayMissileAlert(0);
        }

        [ClientRpc]
        public void RpcPlayMissileAlert(byte s)
        {
            
        }
        
        [Command (requiresAuthority = false)]
        public void CmdPlayMissileAlert()
        {
            if(_missiles.Count < 0 && !rocketAlert) return;

            var distances = _missiles.Select(missile => Vector3.Distance(missile.transform.position, transform.position)).ToList();

            var min = distances[0];
            var minDistance = distances.Prepend(min).Min();

            if(minDistance < 15)
                RpcPlayMissileAlert(5);
            
            if(minDistance < 30)
                RpcPlayMissileAlert(4);
            
            if(minDistance < 40)
                RpcPlayMissileAlert(3);
            
            if(minDistance < 65)
                RpcPlayMissileAlert(2);
            
            if(minDistance < 90)
                RpcPlayMissileAlert(1);
            
            print(minDistance);
        }

        public void AddMissile(GameObject missile)
        {
            _missiles.Add(missile);
        }

        [Command (requiresAuthority = false)]
        public void CmdTryToSitOnADriverPlace(Player.Player passenger)
        {
            if(!driverPlace.isEmployed)
                PutThePlayerInThePlace(driverPlace, passenger);
        }

        [Command (requiresAuthority = false)]
        public void CmdTryToSitOnAPassengerPlace(Player.Player passenger)
        {
            foreach (var passengerPlace in passengerPlaces)
            {
                if (passengerPlace.isEmployed) continue;
                PutThePlayerInThePlace(passengerPlace, passenger);
                return;
            }
        }

        [Command (requiresAuthority = false)]
        public void CmdTryToAChangeAPassengerPlace(Player.Player passenger)
        {
            foreach (var passengerPlace in passengerPlaces)
            {
                if (passengerPlace.owner == passenger)
                    FreeUpPlace(passengerPlace);
            }

            foreach (var passengerPlace in passengerPlaces)
            {
                if (passengerPlace.isEmployed == false)
                    PutThePlayerInThePlace(passengerPlace, passenger);
            }
        }

        private static void FreeUpPlace(PlaceInTheVehicle place)
        {
            place.isEmployed = false;
            place.owner = null;
        }
        
        private static void PutThePlayerInThePlace(PlaceInTheVehicle place, Player.Player player)
        {
            place.isEmployed = true;
            place.owner = player;
            
            player.transform.position = place.transform.position;
            player.transform.rotation = place.transform.rotation;
        }
        
        [Command (requiresAuthority = false)]
        private void CmdEnableAllLights()
        {
            RpcEnableAllLights();
        }

        [ClientRpc]
        private void RpcEnableAllLights()
        {
            foreach (var farLight in farLights!)
                farLight.enabled = true;
            
            foreach (var headLight in headLights!)
                headLight.enabled = true;
            
            foreach (var rearLightRed in rearLightsRed!)
                rearLightRed.enabled = true;

            foreach (var rearLightWhite in rearLightsWhite!)
                rearLightWhite.enabled = true;

            foreach (var neonSet in neonSets!)
                neonSet.enabled = true;
        }

        [Command (requiresAuthority = false)]
        private void CmdDisableAllLights()
        {
            RpcDisableAllLights();
        }
        
        [ClientRpc]
        private void RpcDisableAllLights()
        {
            foreach (var farLight in farLights!)
                farLight.enabled = false;
            
            foreach (var headLight in headLights!)
                headLight.enabled = false;

            rightTurnSignal!.enabled = false;
            leftTurnSignal!.enabled = false;
            
            foreach (var rearLightRed in rearLightsRed!)
                rearLightRed.enabled = false;

            foreach (var rearLightWhite in rearLightsWhite!)
                rearLightWhite.enabled = false;

            foreach (var neonSet in neonSets!)
                neonSet.enabled = false;
        }

        [Command (requiresAuthority = false)]
        public void CmdExplode(Player.Player killer)
        {
            driver.CmdChangeHp((byte)driver.maxHp, killer.transform, killer.playerDisplayName);
            foreach (var passenger in passengers)
                passenger.CmdChangeHp((byte)passenger.maxHp, killer.transform, killer.playerDisplayName);

            RpcDisableAllLights();

            foreach (var component in components)
            {
                if(RandomBoolean.GetRandomBoolean(chance))
                    component.transform.SetParent(null);
            }
        }
        
    }
}
