using UnityEngine;

namespace Player.Components
{
    [RequireComponent(typeof(Camera))]
    public class Zoom : MonoBehaviour
    {
    
        private Camera _camera;
        public float defaultFOV = 90;
        public float maxZoomFOV ;
        [Range(0, 1)]
        public float currentZoom;
        public float sensitivity;


        private void Start()
        {
            _camera = GetComponent<Camera>();
        }

        private void Update()
        {
            // Update the currentZoom and the camera's fieldOfView.
            if (!Input.GetKey(KeyCode.V)) return;
            currentZoom += Input.mouseScrollDelta.y * sensitivity * .05f;
            currentZoom = Mathf.Clamp01(currentZoom);
            _camera.fieldOfView = Mathf.Lerp(defaultFOV, maxZoomFOV, currentZoom);
        }
    
    }
}
