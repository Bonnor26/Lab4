using System.Collections;
using UnityEngine;
using Unity.Cinemachine;


public class CameraZoomController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera virtualCamera;
    [SerializeField] private float zoomOutMultiplier = 1.4f;
    [SerializeField] private float zoomDuration = 0.5f;

    private float normalValue;
    private Coroutine zoomRoutine;

    private void Awake()
    {
        normalValue = CurrentValue();
    }

    private void OnEnable()
    {
        GameEvents.BigMeteorSpawned += ZoomOut;
        GameEvents.BigMeteorDestroyed += ZoomIn;
        GameEvents.PlayerDied += ZoomIn;
    }

    private void OnDisable()
    {
        GameEvents.BigMeteorSpawned -= ZoomOut;
        GameEvents.BigMeteorDestroyed -= ZoomIn;
        GameEvents.PlayerDied -= ZoomIn;
    }

    private void ZoomOut() => StartZoom(normalValue * zoomOutMultiplier);
    private void ZoomIn() => StartZoom(normalValue);

    private void StartZoom(float target)
    {
        if (zoomRoutine != null) StopCoroutine(zoomRoutine);
        zoomRoutine = StartCoroutine(LerpZoom(target));
    }

    private IEnumerator LerpZoom(float target)
    {
        float start = CurrentValue();
        float t = 0f;
        while (t < zoomDuration)
        {
            t += Time.deltaTime;
            SetValue(Mathf.Lerp(start, target, t / zoomDuration));
            yield return null;
        }
        SetValue(target);
    }

    private float CurrentValue()
    {
        var lens = virtualCamera.Lens;
        return lens.Orthographic ? lens.OrthographicSize : lens.FieldOfView;
    }

    private void SetValue(float value)
    {
        var lens = virtualCamera.Lens;
        if (lens.Orthographic) lens.OrthographicSize = value;
        else lens.FieldOfView = value;
        virtualCamera.Lens = lens;
    }
}