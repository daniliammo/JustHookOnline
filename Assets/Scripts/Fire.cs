using UnityEngine;


public class Fire : MonoBehaviour
{
    
    public byte lifetime;
    
    
    private void Start()
    {
        Invoke(nameof(Stop), lifetime);
    }

    private void Stop()
    {
        Destroy(gameObject);
    }
    
}
