using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System;

[ExecuteInEditMode]
[ImageEffectAllowedInSceneView]
[RequireComponent(typeof(Camera))]
public class EnviroSkyRenderingLW : MonoBehaviour
{
    [HideInInspector]
    public bool isAddionalCamera = false;
    private Camera myCam;
    [HideInInspector]
    public Material material;
    [HideInInspector]
    public bool simpleFog = false;
    private bool currentSimpleFog = false;


    /// <summary>
    /// 
    /// </summary>
    private void OnEnable()
    {
#if ENVIRO_LW
        if (EnviroSkyMgr.instance == null || EnviroSkyMgr.instance.currentEnviroSkyVersion != EnviroSkyMgr.EnviroSkyVersion.LW)
        {
           // Debug.Log("Deactivated EnviroSkyRenderingLW component. Not in LW mode or Manager missing.");
            this.enabled = false;
            return;
        }
#endif

        myCam = GetComponent<Camera>();

        if (myCam.actualRenderingPath == RenderingPath.Forward)
            myCam.depthTextureMode = DepthTextureMode.Depth;

        CreateFogMaterial();
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnDisable()
    {
        DestroyFogMaterial();
        //Reset to default depth mode.
        if (myCam != null && myCam.actualRenderingPath == RenderingPath.Forward)
        {
            if (myCam.depthTextureMode == DepthTextureMode.Depth)
                myCam.depthTextureMode = DepthTextureMode.None;
        }
    }

    private void CreateFogMaterial ()
    {
        if (material != null)
            DestroyImmediate(material);

        if (!EnviroSkyMgr.instance.FogSettings.useSimpleFog)
        {
            var shader = Shader.Find("Enviro/Lite/EnviroFogRendering");
            if (shader == null)
                throw new Exception("Critical Error: \"Enviro/EnviroFogRendering\" shader is missing.");
            material = new Material(shader);
        }
        else
        {
            var shader = Shader.Find("Enviro/Lite/EnviroFogRenderingSimple");
            if (shader == null)
                throw new Exception("Critical Error: \"Enviro/EnviroFogRendering\" shader is missing.");
            material = new Material(shader);
        }

        if (EnviroSkyMgr.instance.FogSettings.useSimpleFog)
        {
            Shader.EnableKeyword("ENVIRO_SIMPLE_FOG");
        }
        else
        {
            Shader.DisableKeyword("ENVIRO_SIMPLE_FOG");
        }

    }

    private void DestroyFogMaterial()
    {
        if (material != null)
            DestroyImmediate(material);
    }

    private void Update()
    {
        if (EnviroSkyMgr.instance == null)
            return;

        if (!EnviroSkyMgr.instance.IsAvailable())
            return;

        if (currentSimpleFog != EnviroSkyMgr.instance.FogSettings.useSimpleFog)
        {
            CreateFogMaterial();
            currentSimpleFog = EnviroSkyMgr.instance.FogSettings.useSimpleFog;
        }
    }


    private void OnPreRender ()
	{
        ///////////////////Matrix Information
        if (myCam.stereoEnabled)
        {
            // Both stereo eye inverse view matrices
            var left_world_from_view = myCam.GetStereoViewMatrix(Camera.StereoscopicEye.Left).inverse;
            var right_world_from_view = myCam.GetStereoViewMatrix(Camera.StereoscopicEye.Right).inverse;

            // Both stereo eye inverse projection matrices, plumbed through GetGPUProjectionMatrix to compensate for render texture
            var left_screen_from_view = myCam.GetStereoProjectionMatrix(Camera.StereoscopicEye.Left);
            var right_screen_from_view = myCam.GetStereoProjectionMatrix(Camera.StereoscopicEye.Right);
            var left_view_from_screen = GL.GetGPUProjectionMatrix(left_screen_from_view, true).inverse;
            var right_view_from_screen = GL.GetGPUProjectionMatrix(right_screen_from_view, true).inverse;

            // Negate [1,1] to reflect Unity's CBuffer state
            if (SystemInfo.graphicsDeviceType != UnityEngine.Rendering.GraphicsDeviceType.OpenGLCore && SystemInfo.graphicsDeviceType != UnityEngine.Rendering.GraphicsDeviceType.OpenGLES3 && SystemInfo.graphicsDeviceType != UnityEngine.Rendering.GraphicsDeviceType.OpenGLES2)
            {
                left_view_from_screen[1, 1] *= -1;
                right_view_from_screen[1, 1] *= -1;
            }

            Shader.SetGlobalMatrix("_LeftWorldFromView", left_world_from_view);
            Shader.SetGlobalMatrix("_RightWorldFromView", right_world_from_view);
            Shader.SetGlobalMatrix("_LeftViewFromScreen", left_view_from_screen);
            Shader.SetGlobalMatrix("_RightViewFromScreen", right_view_from_screen);
        }
        else
        {
            // Main eye inverse view matrix
            var left_world_from_view = myCam.cameraToWorldMatrix;

            // Inverse projection matrices, plumbed through GetGPUProjectionMatrix to compensate for render texture
            var screen_from_view = myCam.projectionMatrix;
            var left_view_from_screen = GL.GetGPUProjectionMatrix(screen_from_view, true).inverse;

            // Negate [1,1] to reflect Unity's CBuffer state
            if (SystemInfo.graphicsDeviceType != UnityEngine.Rendering.GraphicsDeviceType.OpenGLCore && SystemInfo.graphicsDeviceType != UnityEngine.Rendering.GraphicsDeviceType.OpenGLES3 && SystemInfo.graphicsDeviceType != UnityEngine.Rendering.GraphicsDeviceType.OpenGLES2)
                left_view_from_screen[1, 1] *= -1;

            // Store matrices
            Shader.SetGlobalMatrix("_LeftWorldFromView", left_world_from_view);
            Shader.SetGlobalMatrix("_LeftViewFromScreen", left_view_from_screen);
        }
        //////////////////////////////
    }

    [ImageEffectOpaque]
	public void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
        if (EnviroSkyLite.instance == null) {
            Graphics.Blit(source, destination);
            return;
        }

        // Set Camera to render depth in forward
        if (myCam.actualRenderingPath == RenderingPath.Forward)
            myCam.depthTextureMode |= DepthTextureMode.Depth;

        if(material == null)
            CreateFogMaterial();

        if (!Application.isPlaying && EnviroSkyLite.instance.showFogInEditor || Application.isPlaying)
        {
            RenderFog(source, destination);
        }
        else
        {
            Graphics.Blit(source, destination);
        }

    }


    private void RenderFog(RenderTexture source, RenderTexture destination)
    {
        var FdotC = myCam.transform.position.y - EnviroSkyLite.instance.fogSettings.height;
        var paramK = FdotC <= 0.0f ? 1.0f : 0.0f;
        var sceneMode = RenderSettings.fogMode;
        var sceneDensity = RenderSettings.fogDensity;
        var sceneStart = RenderSettings.fogStartDistance;
        var sceneEnd = RenderSettings.fogEndDistance;
        Vector4 sceneParams;
        var linear = sceneMode == FogMode.Linear;
        var diff = linear ? sceneEnd - sceneStart : 0.0f;
        var invDiff = Mathf.Abs(diff) > 0.0001f ? 1.0f / diff : 0.0f;
        sceneParams.x = sceneDensity * 1.2011224087f; // density / sqrt(ln(2)), used by Exp2 fog mode
        sceneParams.y = sceneDensity * 1.4426950408f; // density / ln(2), used by Exp fog mode
        sceneParams.z = linear ? -invDiff : 0.0f;
        sceneParams.w = linear ? sceneEnd * invDiff : 0.0f;
        Shader.SetGlobalVector("_SceneFogParams", sceneParams);
        Shader.SetGlobalVector("_SceneFogMode", new Vector4((int)sceneMode, EnviroSkyLite.instance.fogSettings.useRadialDistance ? 1 : 0, 0, 0));
        Shader.SetGlobalVector("_HeightParams", new Vector4(EnviroSkyLite.instance.fogSettings.height, FdotC, paramK, EnviroSkyLite.instance.fogSettings.heightDensity * 0.5f));
        Shader.SetGlobalVector("_DistanceParams", new Vector4(-Mathf.Max(EnviroSkyLite.instance.fogSettings.startDistance, 0.0f), 0, 0, 0));

        material.SetTexture("_MainTex", source);
        Graphics.Blit(source, destination, material);

    }
}