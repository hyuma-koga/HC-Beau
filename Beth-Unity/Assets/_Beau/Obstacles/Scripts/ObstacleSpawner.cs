using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("生成位置の基準（省略可）")]
    [SerializeField] private Transform spawnRoot;

    [Header("ステージ間のY間隔")]
    [SerializeField] private float spacingY = 20f;

    [Header("最大保持ステージ数")]
    [SerializeField] private int maxStageInstances = 2;

    private List<GameObject> stageInstances = new List<GameObject>();
    private float lastSpawnY = 0f;

    /// <summary>
    /// 現在のStageDataを、次のY位置に積み上げて生成
    /// </summary>
    public void SpawnNextStage(StageData stageData)
    {
        if (stageData == null || stageData.stagePrefab == null)
        {
            Debug.LogWarning("SpawnNextStage: ステージデータまたはプレハブが未設定です。");
            return;
        }

        Vector3 basePos = spawnRoot != null ? spawnRoot.position : Vector3.zero;
        Vector3 spawnPos = new Vector3(basePos.x, lastSpawnY, basePos.z);

        var instance = Instantiate(stageData.stagePrefab, spawnPos, Quaternion.identity);
        stageInstances.Add(instance);

        lastSpawnY += spacingY;

        if (stageInstances.Count > maxStageInstances)
        {
            Destroy(stageInstances[0]);
            stageInstances.RemoveAt(0);
        }
    }

    /// <summary>
    /// 指定のStageDataを、baseY位置に生成（StageManager用）
    /// </summary>
    public void SpawnSpecificStage(StageData stageData)
    {
        if (stageData == null || stageData.stagePrefab == null)
        {
            Debug.LogWarning("SpawnSpecificStage: ステージデータまたはプレハブが未設定です。");
            return;
        }

        Vector3 basePos = spawnRoot != null ? spawnRoot.position : Vector3.zero;
        Vector3 spawnPos = new Vector3(basePos.x, stageData.baseY, basePos.z);

        var instance = Instantiate(stageData.stagePrefab, spawnPos, Quaternion.identity);
        stageInstances.Add(instance);

        if (stageInstances.Count > maxStageInstances)
        {
            Destroy(stageInstances[0]);
            stageInstances.RemoveAt(0);
        }

        // lastSpawnYは変更しない → StageManagerはbaseYで制御しているため
    }

    /// <summary>
    /// 明示的にステージを削除（再スタート用）
    /// </summary>
    public void ClearStage()
    {
        foreach (var instance in stageInstances)
        {
            if (instance != null)
            {
                Destroy(instance);
            }
        }
        stageInstances.Clear();
    }

    /// <summary>
    /// ステージ生成位置の初期化（ゲーム開始時に呼ぶ）
    /// </summary>
    public void ResetSpawnHeight(float y)
    {
        lastSpawnY = y;
    }

    /// <summary>
    /// 初回ステージ用に、特定Y位置に1回だけ生成
    /// </summary>
    public void SpawnStageAtY(float spawnY)
    {
        StageData stageData = StageManager.Instance?.CurrentStageData;
        if (stageData == null || stageData.stagePrefab == null)
            return;

        Vector3 basePos = spawnRoot != null ? spawnRoot.position : Vector3.zero;
        Vector3 spawnPos = new Vector3(basePos.x, spawnY, basePos.z);

        var instance = Instantiate(stageData.stagePrefab, spawnPos, Quaternion.identity);
        stageInstances.Add(instance);

        lastSpawnY = spawnY + spacingY;
    }
}
