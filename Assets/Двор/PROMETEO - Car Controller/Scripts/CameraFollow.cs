using UnityEngine;


public class CameraFollow : MonoBehaviour 
{

	public Transform carTransform;
	
	[Range(1, 10)]
	public float followSpeed = 2;
	
	[Range(1, 10)]
	public float lookSpeed = 5;

	private Vector3 _initialCameraPosition;
	private Vector3 _initialCarPosition;
	private Vector3 _absoluteInitCameraPosition;

	
	private void Start()
	{
		_initialCameraPosition = gameObject.transform.position;
		_initialCarPosition = carTransform.position;
		_absoluteInitCameraPosition = _initialCameraPosition - _initialCarPosition;
	}

	private void FixedUpdate()
	{
		// Look at car
		var lookDirection = new Vector3(carTransform.position.x, carTransform.position.y, carTransform.position.z) - transform.position;
		var rot = Quaternion.LookRotation(lookDirection, Vector3.up);
		transform.rotation = Quaternion.Lerp(transform.rotation, rot, lookSpeed * Time.deltaTime);
		
		// Move to car
		var targetPos = _absoluteInitCameraPosition + carTransform.transform.position;
		transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);

	}

}
