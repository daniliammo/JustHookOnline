using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Mirror;
using UnityEngine;

namespace Cars
{
    public class Vehicle : Explosion.Explosion
    {

        private List<GameObject> _missiles;
        
        public bool rocketAlert;
        
        public VehicleEngine[] engines;
        
        public Transform[] wheels;
        
        [SyncVar]
        [Range(1, 255)]
        public byte hp = 255;

        [SyncVar]
        public bool isExploded;

        [Tooltip("Добавьте сюда объекты которые могут оторваться при взрыве.")]
        public GameObject[] components;

        [Range(25, 100)]
        [Tooltip("Шанс отрыва объектов из поля components")]
        public int chance = 25;
        
        [Tooltip("Добавьте сюда места где будет появляться огонь после взрыва.")]
        public Transform[] firePlaces;
        
        #nullable enable
        [Header("Фары и неоновые наборы")]
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

        public PlaceInTheVehicle driverPlace;
        public PlaceInTheVehicle[] passengerPlaces;
        
        public Player.Player driver;
        public List<Player.Player> passengers;
        public bool killPassengersAndDriverAfterVehicleDestroy;
        
        
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

        [Pure]
        private PlaceInTheVehicle SearchPlaceByOwner(Player.Player owner)
        {
            foreach (var passengerPlace in passengerPlaces)
            {
                if (passengerPlace.owner == owner)
                    return passengerPlace;
            }

            throw new Exception($"Не удалось найти место по владельцу {owner.playerDisplayName}");
        }
        
        private void Exit(Player.Player player)
        {
            var place = SearchPlaceByOwner(player);
            
            player.transform.position = place.exitPlace;
            player.transform.rotation = place.transform.rotation;
            
            FreeUpPlace(place);

            player.ExitOutOfVehicle();
        }
        
        private static void FreeUpPlace(PlaceInTheVehicle place)
        {
            place.isEmployed = false;
            place.owner = null;
        }
        
        private void PutThePlayerInThePlace(PlaceInTheVehicle place, Player.Player player)
        {
            place.isEmployed = true;
            place.owner = player;
            
            player.transform.position = place.exitPlace;
            player.transform.rotation = transform.rotation;
            
            player.transform.SetParent(transform, true);

            player.GotIntoVehicle(this);
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
            CmdExplode();
            driver.SetHp(driver.maxHp, killer.playerDisplayName);
            Exit(driver);
            
            if(killPassengersAndDriverAfterVehicleDestroy)
            {
                foreach (var passenger in passengers)
                {
                    passenger.SetHp(passenger.maxHp, killer.playerDisplayName);
                    Exit(passenger);
                }
            }

            RpcDisableAllLights();

            foreach (var component in components)
            {
                if (!RandomBoolean.GetRandomBoolean(chance)) continue;
                component.transform.SetParent(null);
                component.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, radius, upwardsModifier, ForceMode.Impulse);
            }
            
            CmdExplode();
            
        }
        
    }
}
