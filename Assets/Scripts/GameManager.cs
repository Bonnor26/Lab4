using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private string gameSceneName = "Week5Lab";

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
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
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