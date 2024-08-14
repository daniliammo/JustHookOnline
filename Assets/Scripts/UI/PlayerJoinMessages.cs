using Mirror;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerJoinMessages : NetworkBehaviour
    {
        
        public GameObject playerJoinedMessage1;
        public GameObject playerJoinedMessage2;


        [Command (requiresAuthority = false)]
        public void CmdSendPlayerJoinMessage(string joinedPlayerNickname)
        {
            RpcSendPlayerJoinMessage(joinedPlayerNickname);
        }
        
        [ClientRpc]
        private void RpcSendPlayerJoinMessage(string joinedPlayerNickname)
        {
            if (!playerJoinedMessage1.activeSelf)
            {
                PlayerJoinMessage(playerJoinedMessage1, joinedPlayerNickname);
                return;
            }

            if(!playerJoinedMessage2.activeSelf)
                PlayerJoinMessage(playerJoinedMessage2, joinedPlayerNickname);
        }

        private static void PlayerJoinMessage(GameObject playerJoinedMessage, string joinedPlayerNickname)
        {
            playerJoinedMessage.transform.GetChild(0).GetComponent<TMP_Text>().text = joinedPlayerNickname + " подключился";
            playerJoinedMessage.SetActive(true);
        }
        
    }
}
