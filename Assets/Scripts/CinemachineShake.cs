using UnityEngine;
using Unity.Cinemachine;
public class CinemachineShake : MonoBehaviour
{

    public static CinemachineShake Instance { get;private set; }
    private CinemachineCamera cinemachineCamera;
    private float ShakeTime;
    void Awake()
    {
        Instance = this;
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
         cinemachineCamera.GetCinemachineComponent(CinemachineCore.Stage.Noise)
                as CinemachineBasicMultiChannelPerlin;
        cinemachineBasicMultiChannelPerlin.AmplitudeGain = intensity;
        ShakeTime = time;
    }
    void Update()
    {
        if (ShakeTime > 0)
        {
            ShakeTime -= Time.deltaTime;
            if (ShakeTime <= 0)
            {

                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cinemachineCamera.GetCinemachineComponent(CinemachineCore.Stage.Noise)
                   as CinemachineBasicMultiChannelPerlin;
                cinemachineBasicMultiChannelPerlin.AmplitudeGain = 0.5f;
            }
        }
    }

}
