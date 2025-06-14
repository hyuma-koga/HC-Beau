using UnityEngine;

public class GameReadyUIController : MonoBehaviour
{
    [SerializeField] private GameObject gameReadyUI;
    [SerializeField] private GameObject mouseBarrier;
    [SerializeField] private PlayerController balloonController;
    [SerializeField] private ObstacleSpawner obstacleSpawner;

    private bool waitingForClick = false;
    private bool skipFirstFrame = false;

    public void ShowGameReadyUI()
    {
        gameReadyUI.SetActive(true);
        Time.timeScale = 0f;

        if (mouseBarrier != null)
        {
            mouseBarrier.SetActive(true);
        }

        skipFirstFrame = true;
        waitingForClick = true;
    }

    private void Update()
    {
        if (!waitingForClick) return;

        if (skipFirstFrame)
        {
            skipFirstFrame = false;
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        gameReadyUI.SetActive(false);

        if (balloonController != null)
        {
            balloonController.StartRise();
        }

        if (obstacleSpawner != null)
        {
            obstacleSpawner.SpawnObstacleLine();
        }

        Time.timeScale = 1f;
        waitingForClick = false;
    }
}
