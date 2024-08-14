using UnityEngine;
using System.Collections;
namespace EnviroSamples
{
public class FPSController : MonoBehaviour {

	public float speed = 2f;
	public float sensitivity = 2f;
	private CharacterController player;

	public GameObject eyes;

	private float moveFB;
	private float moveLR;

	private float rotX;
	private float rotY;

	// Use this for initialization
	private void Start () {

		player = GetComponent<CharacterController> ();

	}

	// Update is called once per frame
	private void Update () {

		moveFB = Input.GetAxis ("Vertical") * speed;
		moveLR = Input.GetAxis ("Horizontal") * speed;

		rotX = Input.GetAxis ("Mouse X") * sensitivity;
		rotY -= Input.GetAxis ("Mouse Y") * sensitivity;

		rotY = Mathf.Clamp (rotY, -60f, 60f);

		var movement = new Vector3 (moveLR, 0, moveFB);
		transform.Rotate (0, rotX, 0);
		eyes.transform.localRotation = Quaternion.Euler(rotY, 0, 0);
		//eyes.transform.Rotate (-rotY, 0, 0);

		movement = transform.rotation * movement;
        movement.y -= 4000f * Time.deltaTime;
        player.Move (movement * Time.deltaTime);

	}
	}
}