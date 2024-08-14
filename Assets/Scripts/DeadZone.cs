using UnityEngine;


public class DeadZone : MonoBehaviour
{

	private void OnCollisionEnter(Collision other)
	{
		if (other.transform.CompareTag("Player"))
		{
			var player = other.transform.GetComponent<Player.Player>();
			player.Death("Зона Смерти");
		}

		if (!other.transform.CompareTag("Player"))
			Destroy(other.gameObject);
	}
	
}
