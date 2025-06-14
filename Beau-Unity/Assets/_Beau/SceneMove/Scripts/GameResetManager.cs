using UnityEngine;

public class GameResetManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private ObstacleSpawner obstacleSpawner;

    public void ResetGame()
    {
        if(obstacleSpawner != null)
        {
            obstacleSpawner.ClearObstacles();
        }

        if(playerController != null)
        {
            playerController.ResetPlayer();
        }
    }
}
