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
    public int hour;
    public int minute;

    [Header("Время включения")]
    public int hour2;
    public int minute2;


    public override void OnStartClient()
    {
        _azureTimeController = FindObjectOfType<AzureTimeController>();
        _azureTimeController.m_onMinuteChange.AddListener(OnMinuteChange);
    }

    [Command(requiresAuthority = false)]
    private void OnMinuteChange()
    {
        if (hour <= _azureTimeController.m_hour && minute <= _azureTimeController.m_minute)
            RpcSetActiveLights(true);

        if (hour >= _azureTimeController.m_hour && minute >= _azureTimeController.m_minute)
            RpcSetActiveLights(false);

        if (hour2 >= _azureTimeController.m_hour && minute2 >= _azureTimeController.m_minute)
            RpcSetActiveLights(true);
    }

    [Command(requiresAuthority = false)]
    public void CmdBreakLamp()
    {
        RpcBreakLamp();
    }

    [ClientRpc]
    private void RpcBreakLamp()
    {
        foreach (var lightComponent in lights)
            lightComponent.enabled = false;

        breakableWindow.RpcBreakWindow();
    }

    [ClientRpc]
     private void RpcSetActiveLights(bool isActive)
    {
        foreach (var lightComponent in lights)
            lightComponent.enabled = isActive;
    }

}
