using UnityEngine;
using System.Collections;

public class StageProgressByDistance : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    [Tooltip("最初のパネル表示とステージ生成を行うY位置")]
    [SerializeField] private float firstTransitionY = 10f;

    [Tooltip("次のパネル表示とステージ生成までのY間隔")]
    [SerializeField] private float intervalY = 20f;

    private float nextTransitionY;
    private bool isTransitioning = false;

    private void Start()
    {
        if (playerTransform == null)
        {
            Debug.LogWarning("プレイヤーTransformが設定されていません");
            enabled = false;
            return;
        }

        nextTransitionY = firstTransitionY;
    }

    private void Update()
    {
        if (!isTransitioning && playerTransform.position.y >= nextTransitionY)
        {
            StartCoroutine(HandleStageTransitionAndSpawn());
        }
    }

    private IEnumerator HandleStageTransitionAndSpawn()
    {
        isTransitioning = true;

        // ステージカウント+1 & UI更新
        StageManager.Instance.IncrementStage();

        int stageNumber = StageManager.Instance.GetCurrentStage();

        // パネル表示（ステージ番号に基づく）
        yield return StartCoroutine(StageManager.Instance.PlayTransitionPanel(stageNumber));

        // ステージ生成処理（Obstacleや背景の適用）
        StageData stageData = StageManager.Instance.GetStageDataAt(stageNumber);
        if (stageData != null)
        {
            StageManager.Instance.ApplyStage(stageNumber, stageData);
        }

        nextTransitionY += intervalY;
        isTransitioning = false;
    }
}
