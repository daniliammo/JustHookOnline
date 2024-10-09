using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Rendering;


public class BreakableWindow : NetworkBehaviour 
{
    
    [Range(2, 25)]
    public int partsX = 5;
    [Range(2, 25)]
    public int partsY = 5;

    [Space]
    public bool preCalculate = true;
    public bool addTorques = true;
    public bool hideSplintersInHierarchy;
    public bool useCollision = true;
    [Tooltip("Use 0 for breaking immediately if a collision is detected.")]
    public float health;

    [Space]
    [Space]
    [Tooltip("Seconds after window is broken that physics have to be destroyed.")]
    public float destroyPhysicsTime = 5;
    public bool destroyColliderWithPhysics = true;

    [Space]
    [Tooltip("Seconds after window is broken that splinters have to be destroyed.")]
    public float destroySplintersTime;

    [Space]
    public AudioClip breakingSound;


    [HideInInspector]
    public bool isBroken;
    
    public List<GameObject> splinters;
    private Vector3[] _vertices;
    private Vector3[] _normals;
    
    private bool _alreadyCalculated;
    private GameObject _splinterParent;
    private int[] _tris;
    
    
    private void Start()
    {
        if (preCalculate && _alreadyCalculated == false)
        {
            BakeVertices();
            BakeSplinters();
            _alreadyCalculated = true;
        }

        if (transform.rotation.eulerAngles.x != 0 || transform.rotation.eulerAngles.z != 0)
            Debug.LogWarning("Warning: Window must not be rotated around x and z!");
    }

    private void BakeVertices(bool trianglesToo = false)
    {
        _vertices = new Vector3[(partsX + 1) * (partsY + 1)];
        _normals = new Vector3[(partsX + 1) * (partsY + 1)];
        

        for (var y = 0; y < partsY + 1; y++)
        {
            for (var x = 0; x < partsX + 1; x++)
            {
                var randomX = Random.value > 0.5f ? Random.value / partsX : -Random.value / partsX;
                var randomY = Random.value > 0.5f ? Random.value / partsY : -Random.value / partsY;
                _vertices[y * (partsX + 1) + x] = new Vector3(x / (float)partsX - 0.5f + randomX, y / (float)partsY - 0.5f + randomY, 0);
                _normals[y * (partsX + 1) + x] = -Vector3.forward;
            }
        }

        if (trianglesToo)
        {
            _tris = new int[partsX * partsY * 6];
            var pos = 0;
            for (var y = 0; y < partsY; y++)
            {
                for (var x = 0; x < partsX; x++)
                {
                    _tris[pos + 0] = y * (partsX + 1) + x;
                    _tris[pos + 1] = y * (partsX + 1) + x + 1;
                    _tris[pos + 2] = (y + 1) * (partsX + 1) + x;

                    pos += 3;

                    _tris[pos + 0] = (y + 1) * (partsX + 1) + x;
                    _tris[pos + 1] = y * (partsX + 1) + x + 1;
                    _tris[pos + 2] = (y + 1) * (partsX + 1) + x + 1;

                    pos += 3;
                }
            }
        }
    }

    private void GenerateSingleSplinter(IReadOnlyList<int> tris, Transform parent)
    {
        var v = new Vector3[3];
        var n = new Vector3[3];
        var t = new int[6];

        v[0] = Vector3.zero;
        v[1] = _vertices[tris[1]] - _vertices[tris[0]];
        v[2] = _vertices[tris[2]] - _vertices[tris[0]];

        n[0] = _normals[t[0]];
        n[1] = _normals[t[1]];
        n[2] = _normals[t[2]];

        t[0] = 0;
        t[1] = 1;
        t[2] = 2;
        t[3] = 2;
        t[4] = 1;
        t[5] = 0;

        var m = new Mesh
        {
            vertices = v,
            normals = n,
            triangles = t
        };
        
        var obj = new GameObject
        {
            transform =
            {
                position = new Vector3(_vertices[tris[0]].x * transform.localScale.x + transform.position.x, _vertices[tris[0]].y * transform.localScale.y + transform.position.y, transform.position.z)
            }
        };
        obj.transform.SetParent(null);
        obj.transform.RotateAround(transform.position, transform.up, transform.rotation.eulerAngles.y);
        obj.transform.localScale = transform.localScale;
        obj.transform.rotation = transform.rotation;
        obj.layer = 0;
        obj.tag = "PhysicalBody";
        obj.name = "Glass Splinter";
        
        if (destroySplintersTime > 0)
            Destroy(obj, destroySplintersTime);


        if (preCalculate)
            obj.transform.parent = parent;

        if (hideSplintersInHierarchy) obj.hideFlags = HideFlags.HideInHierarchy;
        splinters.Add(obj);

        var mf = obj.AddComponent<MeshFilter>();
        mf.mesh = m;
        
        var col = obj.AddComponent<MeshCollider>();
        // col.inflateMesh = true;
        col.convex = true;
        
        if (destroyPhysicsTime > 0 && destroyColliderWithPhysics) 
            Destroy(col, destroyPhysicsTime);
        
        var rigid = obj.AddComponent<Rigidbody>();
        rigid.centerOfMass = (v[0] + v[1] + v[2]) / 3f;
        if (addTorques && preCalculate == false) rigid.AddTorque(new Vector3(Random.value > 0.5f ? Random.value * 50 : -Random.value * 50, Random.value > 0.5f ? Random.value * 50 : -Random.value * 50, Random.value > 0.5f ? Random.value * 50 : -Random.value * 50));
        if (destroyPhysicsTime > 0) Destroy(rigid, destroyPhysicsTime);

        var mr = obj.AddComponent<MeshRenderer>();
        mr.materials = GetComponent<Renderer>().materials;
        mr.shadowCastingMode = ShadowCastingMode.Off;
        mr.receiveShadows = false;
    }

    private void BakeSplinters()
    {
        var t = new int[3];
        splinters = new List<GameObject>();
        _splinterParent = new GameObject("Splinters");
        _splinterParent.transform.parent = transform;

        if (preCalculate) _splinterParent.SetActive(false);

        for (var y = 0; y < partsY; y++)
        {
            for (var x = 0; x < partsX; x++)
            {
                t[0] = y * (partsX + 1) + x;
                t[1] = y * (partsX + 1) + x + 1;
                t[2] = (y + 1) * (partsX + 1) + x;

                GenerateSingleSplinter(t, _splinterParent.transform);

                t[0] = (y + 1) * (partsX + 1) + x;
                t[1] = y * (partsX + 1) + x + 1;
                t[2] = (y + 1) * (partsX + 1) + x + 1;

                GenerateSingleSplinter(t, _splinterParent.transform);
            }
        }
    }

    [Command (requiresAuthority = false)]
    public void CmdBreakWindow()
    {
        RpcBreakWindow();
    }
    
    /// <summary>
    /// Breaks the window and returns an array of all splinter gameobjects.
    /// </summary>
    /// <returns>Returns an array of all splinter gameobjects.</returns>
    [ClientRpc]
    public void RpcBreakWindow()
    {
        if (isBroken == false)
        {
            isBroken = true;
            
            if (_alreadyCalculated)
            {
                _splinterParent.SetActive(true);
                if (addTorques)
                {
                    foreach (var t in splinters)
                        t.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.value > 0.5f ? Random.value * 50 : -Random.value * 50, Random.value > 0.5f ? Random.value * 50 : -Random.value * 50, Random.value > 0.5f ? Random.value * 50 : -Random.value * 50));
                }
            }
            else
            {
                BakeVertices();
                BakeSplinters();
            }
        }

        var tempAudioObject = new GameObject("Breaking Sound (Temp)")
        {
            transform =
            {
                position = transform.position,
                parent = null
            }
        };

        var audioSource = tempAudioObject.AddComponent<AudioSource>();
        if(!breakingSound)
            Debug.LogError($"breakingSound не задан на объекте: {name}. Звук не будет проигран");
        audioSource.clip = breakingSound;
        audioSource.spatialBlend = 1;
        audioSource.maxDistance = 150;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.Play();

        tempAudioObject.AddComponent<TempAudioSource>();
        
        _splinterParent.transform.SetParent(null);
        
        NetworkServer.Destroy(gameObject);
        Destroy(gameObject);
    }


    private void OnCollisionEnter(Collision col)
    {
        if (useCollision)
        {
            if (health > 0)
            {
                health -= col.impulse.magnitude;
                if (health < 0)
                {
                    health = 0;
                    CmdBreakWindow();
                }
            }
            else CmdBreakWindow();
        }
    }
    
}
