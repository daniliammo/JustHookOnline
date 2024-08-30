using System;
using Mirror;
using UnityEngine.AzureSky;


public class TimeController : NetworkBehaviour
{

	private AzureTimeController _azureTimeController;


	private void Start()
	{
		_azureTimeController = FindObjectOfType<AzureTimeController>();
		InvokeRepeating(nameof(SyncTime), 1, 7);
	}
	
	[Server]
	private void SyncTime()
	{
		if(isServer)
			CmdSetTime(_azureTimeController.GetTimeline());
	}
	
	[Command (requiresAuthority = false)]
	private void CmdSetTime(float time)
	{
		RpcSetTime(time);
	}

	[ClientRpc]
	private void RpcSetTime(float time)
	{
		_azureTimeController.SetTimeline(time);
	}
	
}
