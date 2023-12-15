using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public static CameraShake sharedInstanceCS;

    private CinemachineVirtualCamera _cam;
    public float shakeIntensity = 1f;
    public float shakeTime = 0.2f;

    private float timer;
    private CinemachineBasicMultiChannelPerlin _multiChannelPerlin;

    private void Awake()
    {
        if (sharedInstanceCS == null)
        {
            sharedInstanceCS = this;
        }

        _cam = GetComponent<CinemachineVirtualCamera>();

    }

    private void Start()
    {
        StopShake();
    }

    private void Update()
    {

        if (timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                StopShake();
            }
        }
    }

    public void ShakeCamera()
    {
        _multiChannelPerlin = _cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _multiChannelPerlin.m_AmplitudeGain = shakeIntensity;

        timer = shakeTime;
    }

    public void StopShake()
    {
        _multiChannelPerlin = _cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _multiChannelPerlin.m_AmplitudeGain = 0f;

        timer = 0f;
    }


}
