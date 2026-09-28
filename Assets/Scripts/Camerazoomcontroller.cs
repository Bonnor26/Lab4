using UnityEngine;
using Unity.Cinemachine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class CameraShakeTrigger : MonoBehaviour
{
    private CinemachineImpulseSource impulseSource;

    private void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void OnEnable()
    {
        GameEvents.AsteroidDestroyed += Shake;
    }

    private void OnDisable()
    {
        GameEvents.AsteroidDestroyed -= Shake;
    }

    private void Shake()
    {
        impulseSource.GenerateImpulse();
    }
}