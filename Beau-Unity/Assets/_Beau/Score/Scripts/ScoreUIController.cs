using UnityEngine;
using TMPro;

public class ScoreUIController : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI stageLevelText;

    private void OnEnable()
    {
        StageManager.Instance.OnStageChanged += UpdateStageLevelText;
        UpdateStageLevelText(StageManager.Instance.GetCurrentStage());
    }

    private void OnDisable()
    {
        if (StageManager.Instance != null)
        {
            StageManager.Instance.OnStageChanged -= UpdateStageLevelText;
        }
    }

    private void Update()
    {
        if (scoreManager != null && scoreText != null)
        {
            scoreText.text = scoreManager.CurrentScore.ToString();
        }
    }

    private void UpdateStageLevelText(int stageNumber)
    {
        if (stageLevelText != null)
        {
            // 最初は「0」ステージとして表示、それ以降は「1」から進める
            stageLevelText.text = stageNumber == 0 ? "0" : $"{stageNumber}";
        }
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}