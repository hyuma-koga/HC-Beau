using UnityEngine;

public class StageSpawnerByY : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float preSpawnOffset = 10f; // 早めに生成する距離オフセット

    private int nextStageIndex = 1; // 最初はstageList[1]を目指す（[0]はStartで生成済）
    private float nextSpawnY;

    private void Start()
    {
        UpdateNextSpawnY(); // 次の生成Y座標を初期化
    }

    private void Update()
    {
        if (playerTransform == null || StageManager.Instance == null)
            return;

        if (nextStageIndex >= StageManager.Instance.TotalStageCount)
            return;

        float playerY = playerTransform.position.y;

        if (playerY >= nextSpawnY - preSpawnOffset)
        {
            Debug.Log($"StageSpawnerByY: Player Y={playerY} → baseY={nextSpawnY} の {preSpawnOffset} 手前で → NextStage() 呼び出し");

            // 次のステージを先にindex指定で取得（安全）
            StageData upcomingStage = StageManager.Instance.GetStageDataAt(nextStageIndex);

            StageManager.Instance.NextStage(); // 生成
            nextStageIndex++;

            // baseYを直接参照せず、すでに取得したStageDataを使う
            if (upcomingStage != null)
            {
                nextSpawnY = upcomingStage.baseY;
                Debug.Log($"次のステージ生成Y = {nextSpawnY}");
            }
            else
            {
                Debug.LogWarning("次のステージデータが null です");
            }
        }
    }

    private void UpdateNextSpawnY()
    {
        if (StageManager.Instance == null || nextStageIndex >= StageManager.Instance.TotalStageCount)
        {
            Debug.Log(" すべてのステージを生成し終えました");
            return;
        }

        StageData nextStage = StageManager.Instance.GetStageDataAt(nextStageIndex);
        if (nextStage != null)
        {
            nextSpawnY = nextStage.baseY;
            Debug.Log($"次のステージ生成Y = {nextSpawnY}");
        }
    }
}
