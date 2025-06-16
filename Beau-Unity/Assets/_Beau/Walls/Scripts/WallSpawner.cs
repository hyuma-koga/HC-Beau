using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private float wallYOffset = -1.0f;
    [SerializeField] private float leftX = -2.7f;
    [SerializeField] private float rightX = 2.7f;
    [SerializeField] private int obstacleCount = 20;
    [SerializeField] private float obstacleSpacingY = 2f;

    private bool wallsSpawned = false;

    public void SpawnWallsAround(Vector3 firstObstaclePosition)
    {
        if (wallsSpawned)
        {
            return;
        }

        if (wallPrefab == null)
        {
            Debug.LogWarning("WallPrefab が設定されていません");
            return;
        }

        float totalHeight = (obstacleCount - 1) * obstacleSpacingY;
        float centerY = firstObstaclePosition.y + totalHeight / 2f + wallYOffset;
        float wallHeight = totalHeight + obstacleSpacingY;
        Vector3 newScale = new Vector3(1f, wallHeight / 2f, 1f);

        Vector3 leftWallPos = new Vector3(leftX, centerY, 0f);
        GameObject leftWall = Instantiate(wallPrefab, leftWallPos, Quaternion.identity);
        leftWall.transform.localScale = newScale;

        Vector3 rightWallPos = new Vector3(rightX, centerY, 0f);
        GameObject rightWall = Instantiate(wallPrefab, rightWallPos, Quaternion.identity);
        rightWall.transform.localScale = newScale;

        wallsSpawned = true;
    }
}
