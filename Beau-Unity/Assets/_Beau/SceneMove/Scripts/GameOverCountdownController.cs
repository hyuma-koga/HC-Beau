using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverCountdownController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Image countdownCircle;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private GameObject waitUI;
    [SerializeField] private GameObject titleUI;
    [SerializeField] private float countdownTime = 10f;
    [SerializeField] private GameResetManager gameResetManager;

    private float timer;
    private bool isCounting = false;
    private bool hasAutoReturned = false;

    public void StartCountdown()
    {
        gameObject.SetActive(true);
        timer = countdownTime;
        isCounting = true;
        hasAutoReturned = false;

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

        if(timer <= 0f && !hasAutoReturned)
        {
            hasAutoReturned = true;
            isCounting = false;
            ShowTitleUI();
        }
    }

    private void ShowTitleUI()
    {
        Time.timeScale = 0f;
        gameOverUI.SetActive(false);

        if(gameResetManager != null)
        {
            gameResetManager.ResetGame();
        }

        titleUI.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void OnRestartButtonPressed()
    {
        Time.timeScale = 0f;
        gameOverUI.SetActive(false);
        gameResetManager.ResetGame();
        waitUI.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void OnExitButtonPressed()
    {
        ShowTitleUI();
    }
}
