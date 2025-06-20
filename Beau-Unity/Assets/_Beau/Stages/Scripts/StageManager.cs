using UnityEngine;
using System;
using System.Collections;

public class StageManager : MonoBehaviour
{
    [Header("ステージデータ一覧")]
    [SerializeField] private StageData[] stageList;

    [Header("背景画像を設定するRenderer")]
    [SerializeField] private SpriteRenderer backgroundRenderer;

    [Header("ステージ切り替え演出コントローラ")]
    [SerializeField] private StageTransitionController transitionController;

    private int currentStageIndex = 0;

    public static StageManager Instance { get; private set; }

    public StageData CurrentStageData =>
        (currentStageIndex >= 0 && currentStageIndex < stageList.Length)
        ? stageList[currentStageIndex]
        : null;

    public event Action<int> OnStageChanged;

    public int TotalStageCount => stageList.Length;
    public int MaxTransitionStageCount = 2;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        // 最初のステージは適用せず、背景のみ変更
        ApplyStageBackground(currentStageIndex);
    }

    public void NextStage()
    {
        if (currentStageIndex < stageList.Length - 1)
        {
            currentStageIndex++;
            StageData nextStage = stageList[currentStageIndex];
            StartCoroutine(ApplyStageRoutine(currentStageIndex, nextStage, skipTransition: false));
        }
    }

    public void ResetStage(float spawnY = 30f)
    {
        currentStageIndex = 0;
        StageData firstStage = stageList[currentStageIndex];
        StartCoroutine(ApplyStageRoutine(currentStageIndex, firstStage, skipTransition: true));
    }

    public void ResetStageIndex()
    {
        currentStageIndex = 0;
    }

    private IEnumerator ApplyStageRoutine(int index, StageData stage, bool skipTransition = false)
    {
        if (stageList == null || index < 0 || index >= stageList.Length)
            yield break;

        int stageNumber = index + 1;

        if (!skipTransition && transitionController != null)
        {
            yield return StartCoroutine(transitionController.PlayTransition(stageNumber)); // ✅ 引数追加
        }

        if (backgroundRenderer != null && stage.backgroundSprite != null)
        {
            backgroundRenderer.sprite = stage.backgroundSprite;
        }

        ObstacleSpawner spawner = FindFirstObjectByType<ObstacleSpawner>();
        if (spawner != null)
        {
            Debug.Log($"\u2705 ObstacleSpawner found → ステージ {stageNumber} を生成します");
            spawner.ClearStage();
            spawner.SpawnSpecificStage(stage);  // baseY含む
        }
        else
        {
            Debug.LogWarning("❌ ObstacleSpawner がシーン内に見つかりませんでした");
        }

        OnStageChanged?.Invoke(stageNumber);
    }

    public int GetCurrentStage()
    {
        return currentStageIndex;
    }

    public StageData GetNextStageData()
    {
        int nextIndex = currentStageIndex + 1;
        if (stageList != null && nextIndex >= 0 && nextIndex < stageList.Length)
        {
            return stageList[nextIndex];
        }
        return null;
    }

    private void ApplyStageBackground(int index)
    {
        if (stageList == null || index < 0 || index >= stageList.Length)
            return;

        StageData stage = stageList[index];

        if (backgroundRenderer != null && stage.backgroundSprite != null)
        {
            backgroundRenderer.sprite = stage.backgroundSprite;
        }
    }

    public void StartFirstStage()
    {
        if (stageList == null || stageList.Length == 0)
        {
            Debug.LogError("StageManager: stageList が空です！");
            return;
        }

        if (currentStageIndex < 0 || currentStageIndex >= stageList.Length)
        {
            Debug.LogError($"StageManager: currentStageIndex {currentStageIndex} が不正です");
            return;
        }

        StageData firstStage = stageList[currentStageIndex];

        var spawner = FindFirstObjectByType<ObstacleSpawner>();
        if (spawner != null)
        {
            spawner.SpawnNextStage(firstStage);
        }

        if (backgroundRenderer != null && firstStage.backgroundSprite != null)
        {
            backgroundRenderer.sprite = firstStage.backgroundSprite;
        }

        OnStageChanged?.Invoke(currentStageIndex);
    }

    public IEnumerator ShowTransitionPanelOnly()
    {
        if (transitionController != null)
        {
            int stageNumber = currentStageIndex + 1; // または別のロジックで番号指定
            yield return transitionController.PlayTransition(stageNumber); // ✅ 引数付き呼び出し
        }
    }

    public IEnumerator NextStageCoroutine()
    {
        if (currentStageIndex < stageList.Length - 1)
        {
            currentStageIndex++;
            StageData nextStage = stageList[currentStageIndex];
            yield return ApplyStageRoutine(currentStageIndex, nextStage, skipTransition: false);
        }
    }

    public StageData GetStageDataAt(int index)
    {
        if (stageList == null || index < 0 || index >= stageList.Length)
        {
            return null;
        }

        return stageList[index];
    }

    public void IncrementStage()
    {
        currentStageIndex++;
        OnStageChanged?.Invoke(currentStageIndex);
    }

    public IEnumerator PlayTransitionPanel(int stageNumber)
    {
        if (stageNumber > MaxTransitionStageCount)
        {
            yield break; // これ以上はパネル表示しない
        }

        if (transitionController != null)
        {
            yield return StartCoroutine(transitionController.PlayTransition(stageNumber));
        }
    }

    public void ApplyStage(int index, StageData stageData)
    {
        if (backgroundRenderer != null && stageData.backgroundSprite != null)
        {
            backgroundRenderer.sprite = stageData.backgroundSprite;
        }

        var spawner = FindFirstObjectByType<ObstacleSpawner>();
        if (spawner != null)
        {
            spawner.ClearStage();
            spawner.SpawnSpecificStage(stageData);
        }

        OnStageChanged?.Invoke(index);
    }

}
