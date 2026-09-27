using System.Collections;
using UnityEngine;
using Unity.Cinemachine; // change to "using Cinemachine;" if on Cinemachine 2.x


public class CameraZoomController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera virtualCamera;
    [SerializeField] private float normalSize = 5f;
    [SerializeField] private float zoomedOutSize = 8f;
    [SerializeField] private float zoomDuration = 0.5f;

    private Coroutine zoomRoutine;

    private void OnEnable()
    {
        GameEvents.BigMeteorSpawned += ZoomOut;
    }

    private void OnDisable()
    {
        GameEvents.BigMeteorSpawned -= ZoomOut;
    }

    private void ZoomOut()
    {
        if (zoomRoutine != null) StopCoroutine(zoomRoutine);
        zoomRoutine = StartCoroutine(LerpZoom(zoomedOutSize));
    }

    private IEnumerator LerpZoom(float target)
    {
        float start = virtualCamera.Lens.OrthographicSize;
        float t = 0f;
        while (t < zoomDuration)
        {
            t += Time.deltaTime;
            var lens = virtualCamera.Lens;
            lens.OrthographicSize = Mathf.Lerp(start, target, t / zoomDuration);
            virtualCamera.Lens = lens;
            yield return null;
        }
    }
}