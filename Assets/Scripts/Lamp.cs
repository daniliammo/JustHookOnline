using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.AzureSky;


public class Lamp : NetworkBehaviour
{

    public List<Light> lights;
    public BreakableWindow breakableWindow;
    private AzureTimeController _azureTimeController;

    [Header("Время отключения")]
    public HourMinute disableTime;

    [Header("Время включения")]
    public HourMinute enableTime;


    public override void OnStartClient()
    {
        _azureTimeController = FindFirstObjectByType<AzureTimeController>();
        _azureTimeController.m_onMinuteChange.AddListener(OnMinuteChange);
    }

    [Command (requiresAuthority = false)]
    private void OnMinuteChange()
    {
        if (disableTime.hour <= _azureTimeController.m_hour && disableTime.minute <= _azureTimeController.m_minute)
            RpcSetActiveLights(true);

        if (disableTime.hour >= _azureTimeController.m_hour && disableTime.minute >= _azureTimeController.m_minute)
            RpcSetActiveLights(false);

        if (enableTime.hour >= _azureTimeController.m_hour && enableTime.minute >= _azureTimeController.m_minute)
            RpcSetActiveLights(true);
    }

    [Command (requiresAuthority = false)]
    public void CmdBreakLamp()
    {
        gameObject.tag = "Untagged";
        RpcBreakLamp();
        breakableWindow.RpcBreakWindow();
        Destroy(this);
    }

    [ClientRpc]
    private void RpcBreakLamp()
    {
        foreach (var lightComponent in lights)
            lightComponent.enabled = false;
    }

    [ClientRpc]
     private void RpcSetActiveLights(bool isActive)
    {
        foreach (var lightComponent in lights)
            lightComponent.enabled = isActive;
    }

}
