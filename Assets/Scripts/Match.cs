using Mirror;
using UnityEngine;

public class Match : NetworkBehaviour
{
    
    public MatchConfig config;


    [Command (requiresAuthority = false)]
    public void CmdSetMatchConfig(MatchConfig newConfig)
    {
        RpcSetMatchConfig(newConfig);
    }

    [ClientRpc]
    private void RpcSetMatchConfig(MatchConfig newConfig)
    {
        config = newConfig;
        RpcLoadMatchConfig();
    }

    [ClientRpc]
    private void RpcLoadMatchConfig()
    {
        Physics.gravity = config.gravity;
    }

    [Command (requiresAuthority = false)]
    public void CmdSyncConfig()
    {
        RpcSetMatchConfig(config);
    }
    
}
