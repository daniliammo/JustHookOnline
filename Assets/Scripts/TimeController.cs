using Mirror;
using UnityEngine.AzureSky;


public class TimeController : NetworkBehaviour
{

	private AzureTimeController _azureTimeController;


	private void Start()
	{
		_azureTimeController = FindObjectOfType<AzureTimeController>();
	}
	
	[Command]
	public void CmdSetTime(float time)
	{
		RpcSetTime(time);
	}

	[ClientRpc]
	private void RpcSetTime(float time)
	{
		_azureTimeController.SetTimeline(time);
	}
	
}
