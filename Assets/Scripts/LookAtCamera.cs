using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    
    private Camera _mainCamera;
    
    
    private void Start()
    {
        _mainCamera = Camera.main;
    }
    
    private void LateUpdate()
    {
        if(!_mainCamera) return;
        transform.LookAt(_mainCamera.transform);
        transform.Rotate(0, 180, 0);
    }
    
}
