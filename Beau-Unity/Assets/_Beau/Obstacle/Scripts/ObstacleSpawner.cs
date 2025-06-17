using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("生成位置の基準（オプション）")]
    [SerializeField] private Transform spawnRoot;

    private GameObject currentStageInstance;

    /// <summary>
    /// 指定したY座標にステージPrefabを生成
    /// </summary>
    /// <param name="spawnY">生成するY座標</param>
    public void SpawnStageAtY(float spawnY)
    {
        StageData stageData = StageManager.Instance?.CurrentStageData;
        if (stageData == null || stageData.stagePrefab == null)
        {
            Debug.LogWarning("StageData または stagePrefab が未設定です。");
            return;
        }

        // 古いステージを削除
        if (currentStageInstance != null)
        {
            Debug.Log($"Destroying previous stage: {currentStageInstance.name}");
            Destroy(currentStageInstance);
        }

        // 新しいステージの生成位置を決定
        Vector3 basePos = spawnRoot != null ? spawnRoot.position : Vector3.zero;
        Vector3 spawnPos = new Vector3(basePos.x, spawnY, basePos.z);

        // ステージPrefabを生成
        Debug.Log($"Spawning new stage prefab: {stageData.stagePrefab.name} at Y={spawnY}");
        currentStageInstance = Instantiate(stageData.stagePrefab, spawnPos, Quaternion.identity);
    }

    /// <summary>
    /// 現在のステージPrefabを削除
    /// </summary>
    public void ClearStage()
    {
        if (currentStageInstance != null)
        {
            Destroy(currentStageInstance);
            currentStageInstance = null;
        }
    }

    public void SpawnSpecificStage(StageData stageData)
    {
        if (stageData == null || stageData.stagePrefab == null)
        {
            Debug.LogWarning("StageData または stagePrefab が未設定です。");
            return;
        }

        if (currentStageInstance != null)
        {
            Destroy(currentStageInstance);
        }

        Vector3 basePos = spawnRoot != null ? spawnRoot.position : Vector3.zero;
        Vector3 spawnPos = new Vector3(basePos.x, stageData.baseY, basePos.z);

        currentStageInstance = Instantiate(stageData.stagePrefab, spawnPos, Quaternion.identity);
    }

}
