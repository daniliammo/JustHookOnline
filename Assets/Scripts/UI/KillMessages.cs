using Mirror;
using TMPro;
using UnityEngine;

namespace UI
{
    public class KillMessages : NetworkBehaviour
    {
        
        public GameObject killMessage1;
        public GameObject killMessage2;
        public GameObject killMessage3;
        public GameObject killMessage4;


        [Command (requiresAuthority = false)]
        public void CmdSendKillMessage(string killerNickName, string killedPlayerNickName)
        {
            RpcSendKillMessage(killerNickName, killedPlayerNickName);
        }
        
        [ClientRpc]
        private void RpcSendKillMessage(string killerNickName, string killedPlayerNickName)
        {
            if (!killMessage1.activeSelf)
            {
                KillMessage(killMessage1, killerNickName, killedPlayerNickName);
                return;
            }

            if(!killMessage2.activeSelf)
            {
                KillMessage(killMessage2, killerNickName, killedPlayerNickName);
                return;
            }
            
            if(!killMessage3.activeSelf)
            {
                KillMessage(killMessage3, killerNickName, killedPlayerNickName);
                return;
            }
            
            if(!killMessage4.activeSelf)
                KillMessage(killMessage4, killerNickName, killedPlayerNickName);
        }

        private static void KillMessage(GameObject killMessage, string killerNickName, string killedPlayerNickName)
        {
            killMessage.transform.GetChild(0).GetComponent<TMP_Text>().text = killerNickName;
            killMessage.transform.GetChild(2).GetComponent<TMP_Text>().text = killedPlayerNickName;
            killMessage.SetActive(true);
        }

    }
}
