using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    // Start is called before the first frame update

    public CinemachineVirtualCamera vcam;
    CinemachineBasicMultiChannelPerlin noise;
    void Awake()
    {
        vcam = GameObject.FindGameObjectWithTag("vCam").GetComponent<CinemachineVirtualCamera>();
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Noise(float amplitudeGain, float frequencyGain)
    {
        noise.m_AmplitudeGain = amplitudeGain;
        noise.m_FrequencyGain = frequencyGain;
    }

    private IEnumerator ProcessShake(float shakeIntensity = 5f, float shakeTiming = 0.5f)
    {
        Noise(1f, shakeIntensity);
        yield return new WaitForSeconds(shakeTiming);
        Noise(0, 0);
    }

    public void CameraShake()
    {
        StartCoroutine(ProcessShake());
    }
}
