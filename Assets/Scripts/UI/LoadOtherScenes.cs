using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadOtherScenes : MonoBehaviour
{

	public void LoadMirrorTanksScene()
	{
		SceneManager.LoadScene("MirrorTanks");
	}
	
	public void LoadMirrorTanksCoopScene()
	{
		SceneManager.LoadScene("MirrorTanksCoop");
	}
	
	public void LoadMirrorRigidBodyBenchmarkScene()
	{
		SceneManager.LoadScene("MirrorRigidbodyBenchmark");
	}
	
	public void LoadMirrorBilliardsScene()
	{
		SceneManager.LoadScene("MirrorBilliards");
	}
	
	public void LoadMirrorBounceScene()
	{
		SceneManager.LoadScene("MirrorBounceScene");
	}
	
	public void LoadMirrorPongScene()
	{
		SceneManager.LoadScene("MirrorPong");
	}
	
	public void LoadMirrorTankTheftAutoScene()
	{
		SceneManager.LoadScene("MirrorTankTheftAuto");
	}
	
	public void LoadMirrorBilliardsPredictedScene()
	{
		SceneManager.LoadScene("MirrorBilliardsPredicted");
	}
	
	public void LoadMirrorMultipleMatchesScene()
	{
		SceneManager.LoadScene("MirrorMultipleMatches");
	}
	
	public void LoadMirrorCouchCoopScene()
	{
		SceneManager.LoadScene("MirrorCouchCoop");
	}
	
}
