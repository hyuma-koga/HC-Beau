using UnityEngine;
using TMPro;

public class ScoreUIController : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Update()
    {
        if(scoreManager != null && scoreText != null)
        {
            scoreText.text = scoreManager.CurrentScore.ToString();
        }
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
