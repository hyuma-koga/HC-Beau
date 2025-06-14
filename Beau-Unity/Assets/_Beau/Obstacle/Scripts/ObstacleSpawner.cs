using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private int spawnCount = 20;
    [SerializeField] private float spacingY = 2f;
    [SerializeField] private float baseY = 6f; // カメラからのオフセットY位置
    [SerializeField] private float fixedX = -0.3f;
    [SerializeField] private Transform cameraTarget;

    private List<GameObject> spawnedObstacles = new List<GameObject>();

    public void SpawnObstacleLine()
    {
        if (obstaclePrefab == null || cameraTarget == null)
        {
            return;
        }

        float startY = cameraTarget.position.y + baseY;

        for (int i = 0; i < spawnCount; i++)
        {
            float y = startY + (spacingY * i);
            Vector3 spawnPos = new Vector3(fixedX, y, 0f);

            GameObject obstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
            spawnedObstacles.Add(obstacle);
        }
    }

    public void ClearObstacles()
    {
        foreach(var obs in spawnedObstacles)
        {
            if(obs != null)
            {
                Destroy(obs);
            }
        }

        spawnedObstacles.Clear();
    }
}
