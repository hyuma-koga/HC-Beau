using UnityEngine;
using System.Collections;

public class StageProgressByDistance : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    [Tooltip("最初にTransitionパネルを表示するY位置")]
    [SerializeField] private float firstTransitionY = 10f;

    [Tooltip("次のパネル表示までのY間隔")]
    [SerializeField] private float intervalY = 20f;

    private float nextTransitionY;
    private int stageCounter = 1;
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
            Debug.Log($"Player Y={playerTransform.position.y} 到達 → Stage進行処理へ");
            StartCoroutine(HandleStageTransitionOnly());
        }
    }

    private IEnumerator HandleStageTransitionOnly()
    {
        isTransitioning = true;

        // パネル表示のみ（ステージ生成なし）
        StageManager.Instance?.ShowTransitionPanelOnly(stageCounter);

        stageCounter++;
        nextTransitionY += intervalY;

        yield return new WaitForSecondsRealtime(2f); // 任意の待機時間

        isTransitioning = false;
    }
}