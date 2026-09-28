using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Unity.Cinemachine; // change to "using Cinemachine;" if your project uses Cinemachine 2.x

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private string gameSceneName = "Week5Lab";
    [SerializeField] private CinemachineCamera virtualCamera;

    public bool gameOver = false;

    private void OnEnable()
    {
        GameEvents.PlayerDied += HandlePlayerDied;
    }

    private void OnDisable()
    {
        GameEvents.PlayerDied -= HandlePlayerDied;
    }

    private void Start()
    {
        GameObject player = Instantiate(playerPrefab, transform.position, Quaternion.identity);

        if (virtualCamera != null)
        {
            virtualCamera.Follow = player.transform;
        }
    }

    private void Update()
    {
        var keyboard = Keyboard.current;
        if (gameOver && keyboard != null && keyboard.rKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(gameSceneName);
        }
    }

    private void HandlePlayerDied()
    {
        gameOver = true;
    }
}