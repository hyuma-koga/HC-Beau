using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverCountdownController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Image countdownCircle;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private float countdownTime = 10f;

    private float timer;
    private bool isCounting = false;

    public void StartCountdown()
    {
        gameObject.SetActive(true);
        timer = countdownTime;
        isCounting = true;

        Cursor.visible = true;

        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (!isCounting)
        {
            return;
        }

        timer -= Time.unscaledDeltaTime;

        //スライダー＆数字更新
        float ratio = Mathf.Clamp01(timer / countdownTime);

        if(countdownCircle != null)
        {
            countdownCircle.fillAmount = ratio;
        }

        if (countdownText != null)
        {
            countdownText.text = Mathf.CeilToInt(timer).ToString();
        }

        if(timer <= 0f)
        {
            isCounting = false;
            ShowGameOverUI();
        }
    }

    private void ShowGameOverUI()
    {
        if(gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        Cursor.visible = true;
        Time.timeScale = 0f;
    }
}
