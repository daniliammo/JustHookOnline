using Mirror;
using UnityEngine.AzureSky;


public class TimeController : NetworkBehaviour
{

	private AzureTimeController _azureTimeController;


	private void Start()
	{
		_azureTimeController = FindFirstObjectByType<AzureTimeController>();
	}
	
	[Command (requiresAuthority = false)]
	public void CmdSyncTime()
	{
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
