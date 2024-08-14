using Mirror;

namespace Utils
{
	public class NetworkEvents : NetworkBehaviour
	{

		public delegate void OnClientStop();
		public static event OnClientStop OnClientStopped;

		public delegate void OnClientStart();
		public static event OnClientStart OnClientStarted;
		
		public override void OnStopClient()
		{
			OnClientStopped?.Invoke();
		}
		
		public override void OnStartClient()
		{
			OnClientStarted?.Invoke();
		}
		
	}
}
