using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
[AddComponentMenu("Enviro/Lite/AddionalCamera")]
public class EnviroLiteAdditionalCamera : MonoBehaviour {

#if ENVIRO_LW
    public bool addEnviroSkyRendering = true;
    public bool addEnviroSkyPostProcessing = true;
    public bool addWeatherEffects = true;

    private Camera myCam;
    private EnviroSkyRenderingLW skyRender;
    private EnviroPostProcessing enviroPostProcessing;

    private GameObject EffectHolder;
    private GameObject VFX;
    private List<EnviroWeatherPrefab> zoneWeather = new List<EnviroWeatherPrefab>();
    private EnviroWeatherPrefab currentWeather;

    private void OnEnable()
    {
        myCam = GetComponent<Camera>();

        if (myCam != null)
            InitImageEffects();
   
    }

    private void Start()
    {
        if (addWeatherEffects)
        {
            CreateEffectHolder();
            StartCoroutine(SetupWeatherEffects());
        }
    }

    private void Update ()
    {
        if (addWeatherEffects)
            UpdateWeatherEffects();

        if (EnviroSkyLite.instance != null)
            UpdateSkyRenderer();
    } 

    private void CreateEffectHolder ()
    {
        var childs = myCam.transform.childCount;

        for (var i = childs - 1; i >= 0; i--)
        {
            if(myCam.transform.GetChild(i).gameObject.name == "Effect Holder")
                DestroyImmediate(myCam.transform.GetChild(i).gameObject);

        }

        EffectHolder = new GameObject();
        EffectHolder.name = "Effect Holder";
        EffectHolder.transform.SetParent(myCam.transform,false);

        VFX = new GameObject();
        VFX.name = "VFX";
        VFX.transform.SetParent(EffectHolder.transform, false);

    }

    private IEnumerator SetupWeatherEffects()
    {
        yield return new WaitForSeconds(1f);

        for (var i = 0; i < EnviroSkyMgr.instance.Weather.weatherPresets.Count; i++)
        {
            //Create Weather Prefab
            var wPrefab = new GameObject();
            var wP = wPrefab.AddComponent<EnviroWeatherPrefab>();
            wP.weatherPreset = EnviroSkyMgr.instance.Weather.weatherPresets[i];
            wPrefab.name = wP.weatherPreset.Name;

            //Add Particle Effects
            for (var w = 0; w < wP.weatherPreset.effectSystems.Count; w++)
            {
                if (wP.weatherPreset.effectSystems[w] == null || wP.weatherPreset.effectSystems[w].prefab == null)
                {
                    Debug.Log("Warning! Missing Particle System Entry: " + wP.weatherPreset.Name);
                    Destroy(wPrefab);
                    break;
                }
                var eS = Instantiate(wP.weatherPreset.effectSystems[w].prefab, wPrefab.transform);
                eS.transform.localPosition = wP.weatherPreset.effectSystems[w].localPositionOffset;
                eS.transform.localEulerAngles = wP.weatherPreset.effectSystems[w].localRotationOffset;
                var pS = eS.GetComponent<ParticleSystem>();

                if (pS != null)
                    wP.effectSystems.Add(pS);
                else
                {
                    pS = eS.GetComponentInChildren<ParticleSystem>();
                    if (pS != null)
                        wP.effectSystems.Add(pS);
                    else
                    {
                        Debug.Log("No Particle System found in prefab in weather preset: " + wP.weatherPreset.Name);
                        Destroy(wPrefab);
                        break;
                    }
                }
            }
            wP.effectEmmisionRates.Clear();
            wPrefab.transform.parent = VFX.transform;
            wPrefab.transform.localPosition = Vector3.zero;
            wPrefab.transform.localRotation = Quaternion.identity;
            zoneWeather.Add(wP);
        }

        // Setup Particle Systems Emission Rates
        for (var i = 0; i < zoneWeather.Count; i++)
        {
            for (var i2 = 0; i2 < zoneWeather[i].effectSystems.Count; i2++)
            {
                zoneWeather[i].effectEmmisionRates.Add(EnviroSkyMgr.instance.GetEmissionRate(zoneWeather[i].effectSystems[i2]));
                EnviroSkyMgr.instance.SetEmissionRate(zoneWeather[i].effectSystems[i2], 0f);
            }
        }

        //Set Current Weather
        if (EnviroSkyMgr.instance.Weather.currentActiveWeatherPrefab != null)
        {
            for (var i = 0; i < zoneWeather.Count; i++)
            {
                if (zoneWeather[i].weatherPreset == EnviroSkyMgr.instance.Weather.currentActiveWeatherPrefab.weatherPreset)
                    currentWeather = zoneWeather[i];
            }
        }
    }

    private void UpdateWeatherEffects()
    {
        if (EnviroSkyMgr.instance.Weather.currentActiveWeatherPrefab == null || currentWeather == null)
            return;


     if(EnviroSkyMgr.instance.Weather.currentActiveWeatherPrefab.weatherPreset != currentWeather.weatherPreset)
        {
            for(var i = 0; i < zoneWeather.Count; i++)
            {
                if (zoneWeather[i].weatherPreset == EnviroSkyMgr.instance.Weather.currentActiveWeatherPrefab.weatherPreset)
                    currentWeather = zoneWeather[i];
            }    
        }

        UpdateEffectSystems(currentWeather, true);
    }

    private void UpdateEffectSystems(EnviroWeatherPrefab id, bool withTransition)
    {
        if (id != null)
        {
            var speed = 500f * Time.deltaTime;

            if (withTransition)
                speed = EnviroSkyMgr.instance.WeatherSettings.effectTransitionSpeed * Time.deltaTime;

            for (var i = 0; i < id.effectSystems.Count; i++)
            {

                if (id.effectSystems[i].isStopped)
                    id.effectSystems[i].Play();

                // Set EmissionRate
                var val = Mathf.Lerp(EnviroSkyMgr.instance.GetEmissionRate(id.effectSystems[i]), id.effectEmmisionRates[i] * EnviroSkyLite.instance.qualitySettings.GlobalParticleEmissionRates, speed) * EnviroSkyMgr.instance.InteriorZoneSettings.currentInteriorWeatherEffectMod;
                EnviroSkyMgr.instance.SetEmissionRate(id.effectSystems[i], val);
            }

            for (var i = 0; i < zoneWeather.Count; i++)
            {
                if (zoneWeather[i].gameObject != id.gameObject)
                {
                    for (var i2 = 0; i2 < zoneWeather[i].effectSystems.Count; i2++)
                    {
                        var val2 = Mathf.Lerp(EnviroSkyMgr.instance.GetEmissionRate(zoneWeather[i].effectSystems[i2]), 0f, speed);

                        if (val2 < 1f)
                            val2 = 0f;

                        EnviroSkyMgr.instance.SetEmissionRate(zoneWeather[i].effectSystems[i2], val2);

                        if (val2 == 0f && !zoneWeather[i].effectSystems[i2].isStopped)
                        {
                            zoneWeather[i].effectSystems[i2].Stop();
                        }
                    }
                }
            }
        }
    }

    private void InitImageEffects()
    {
        if (addEnviroSkyRendering)
        {
            skyRender = myCam.gameObject.GetComponent<EnviroSkyRenderingLW>();

            if (skyRender == null)
                skyRender = myCam.gameObject.AddComponent<EnviroSkyRenderingLW>();

            skyRender.isAddionalCamera = true;
        }

        if (addEnviroSkyPostProcessing)
        {
            enviroPostProcessing = myCam.gameObject.GetComponent<EnviroPostProcessing>();

            if (enviroPostProcessing == null)
                enviroPostProcessing = myCam.gameObject.AddComponent<EnviroPostProcessing>();
        }
    }

    private void UpdateSkyRenderer()
    {
        if (EnviroSkyMgr.instance.FogSettings.useUnityFog && EnviroSkyMgr.instance.Camera != null && EnviroSkyMgr.instance.Camera.renderingPath == RenderingPath.Forward)
        {
            RenderSettings.fog = true;
            if (skyRender != null && skyRender.isActiveAndEnabled)
                skyRender.enabled = false;
        }
        else
        {
            //Enable or Disable Enviro Fog Post Effect
            if (EnviroSkyLite.instance.usePostEffectFog && skyRender != null && !skyRender.isActiveAndEnabled)
                skyRender.enabled = true;
            else if (!EnviroSkyLite.instance.usePostEffectFog && skyRender != null && skyRender.isActiveAndEnabled)
                skyRender.enabled = false;
        }
    }
#endif
}
