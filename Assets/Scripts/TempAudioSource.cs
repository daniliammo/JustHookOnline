using UnityEngine;


public class TempAudioSource : MonoBehaviour
{
    
    private void Start()
    {
        Invoke(nameof(DestroyThis), GetComponent<AudioSource>().clip.length);
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }
    
}
