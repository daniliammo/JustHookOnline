using UnityEngine;


public class Window : LifeEntity
{
    
    private void Awake()
    {
        OnDeath += DestroyWindow;
    }

    private void DestroyWindow(string unused)
    {
        OnDeath -= DestroyWindow;
        gameObject.AddComponent<Rigidbody>();
        Destroy(this);
    }
    
}
