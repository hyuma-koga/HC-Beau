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

    private IEnumerator ApplyStageRoutine(int index, StageData stage, bool skipTransition = false)
    {
        if (stageList == null || index < 0 || index >= stageList.Length)
            yield break;

        int stageNumber = index + 1;

        if (!skipTransition && transitionController != null)
        {
            yield return StartCoroutine(transitionController.PlayTransition(stageNumber));
        }

        if (backgroundRenderer != null && stage.backgroundSprite != null)
        {
            backgroundRenderer.sprite = stage.backgroundSprite;
        }

        ObstacleSpawner spawner = FindFirstObjectByType<ObstacleSpawner>();
        if (spawner != null)
        {
            Debug.Log($"✅ ObstacleSpawner found → ステージ {stageNumber} を生成します");
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
        StageData firstStage = stageList[currentStageIndex];
        StartCoroutine(ApplyStageRoutine(currentStageIndex, firstStage, skipTransition: true));
    }

    public void ShowTransitionPanelOnly(int stageNumber)
    {
        if (transitionController != null)
        {
            StartCoroutine(transitionController.PlayTransition(stageNumber));
        }
    }

    public IEnumerator NextStageCoroutine(int stageNumber)
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
        if(stageList == null || index < 0 || index >= stageList.Length)
        {
            return null;
        }

        return stageList[index];
    }
}
