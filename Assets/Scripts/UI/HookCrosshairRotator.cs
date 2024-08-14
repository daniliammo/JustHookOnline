using UnityEngine;

namespace UI
{
    public class HookCrosshairRotator : MonoBehaviour
    {
        private void FixedUpdate()
        {
            transform.Rotate(new Vector3(0, 0, 3.6f));
        }
    }
}
