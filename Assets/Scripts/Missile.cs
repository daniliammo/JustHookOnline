using Cars;
using Mirror;
using UnityEngine;


public class Missile : Explosion.Explosion
{
    
    public Vehicle target;
    public float maneuverability = 0.05f;
    public float speed = 40;
    public float minSpeed = 20;

    private Vector3 _predictTargetPosition;
    
    public bool allowPrediction;
    
    private Quaternion _previousRotation;
    private Vector3 _previousTargetPosition;

    public float lifeTime = 5;
    
    
    [Server]
    private void FixedUpdate()
    {
        lifeTime -= Time.fixedDeltaTime;

        if (lifeTime < 0)
            Destroy(gameObject);
        
        _previousRotation = transform.rotation;
        AdjustAngle();
        SpeedAdjustment();
        _previousTargetPosition = target.transform.position;

        transform.Translate(Vector3.forward * (speed * Time.fixedDeltaTime));
    }

    private void AdjustAngle()
    {
        switch (allowPrediction)
        {
            case true:
            {
                var time = Vector3.Distance(target.transform.position, transform.position) / speed;
                
                _predictTargetPosition = target.rigidbody.position + target.rigidbody.linearVelocity * time;
                
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(Vector3.forward, _predictTargetPosition - transform.position), maneuverability);
                break;
            }
            case false:
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(Vector3.forward, target.transform.position - transform.position), maneuverability);
                break;
        }

        if (Vector3.Distance(transform.position, target.transform.position) < 1)
            maneuverability = 1;
    }

    private void SpeedAdjustment()
    {
        if (target.transform.position == _previousTargetPosition && transform.rotation != _previousRotation)
        {
            if (speed > minSpeed)
                speed *= 0.99f;
        }
    }

    [Server]
    private void OnCollisionEnter()
    {
        CmdExplode();
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(255, 0, 255, 1);
        Gizmos.DrawLine(transform.position, target.transform.position);
        
        Gizmos.color = new Color(0, 255, 0, 1);
        Gizmos.DrawRay(transform.position, transform.forward * 100);
        
        if(!Application.isPlaying) return;
        Gizmos.color = new Color(0, 0, 255, 1);
        Gizmos.DrawLine(transform.position, _predictTargetPosition);
    }
    #endif
    
}
