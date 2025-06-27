using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUIHandler : MonoBehaviour
{
    [Header("オブジェクト参照")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mouseBarrier;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject waitUI;
    [SerializeField] private GameObject titleUI;
    [SerializeField] private ScoreUIController scoreUI;

    [Header("カウントダウン設定")]
    [SerializeField] private Image countdownCircleImage;
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private float delayBeforeWaitUI = 10f;

    public static GameOverUIHandler Instance { get; private set; }

    private Vector3 initialPlayerPosition;
    private bool isCounting = false;
    private float timer;

    private void Start()
    {
        Instance = this;

        if (player != null)
        {
            initialPlayerPosition = player.transform.position;
        }
    }

    private void Update()
    {
        if (!isCounting)
        {
            return;
        }

        timer -= Time.unscaledDeltaTime;

        if (countdownCircleImage != null)
        {
            float ratio = Mathf.Clamp01(timer / delayBeforeWaitUI);
            countdownCircleImage.fillAmount = ratio;
        }

        if (countdownText != null)
        {
            countdownText.text = Mathf.CeilToInt(timer).ToString();
        }

        if (timer <= 0f)
        {
            isCounting = false;
            ShowWaitUI();
        }
    }

    private void CleanupObstacles()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach(var obs in obstacles)
        {
            Destroy(obs);
        }
    }

    public void ShowGameOverUI()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        Time.timeScale = 0f;
        timer = delayBeforeWaitUI;
        isCounting = true;

        if (countdownCircleImage != null)
        {
            countdownCircleImage.fillAmount = 1f;
        }

        if (countdownText != null)
        {
            countdownText.text = Mathf.CeilToInt(timer).ToString();
        }

        if (player != null)
        {
            player.SetActive(false);
        }

        if (mouseBarrier != null)
        {
            mouseBarrier.SetActive(false);
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnRestartButtonPressed()
    {
        isCounting = false;
        ShowWaitUI();
    }

    public void OnExitToTitlePressed()
    {
        isCounting = false;

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }

        if (waitUI != null)
        {
            waitUI.SetActive(false);
        }

        if (scoreUI != null)
        {
            scoreUI.SetActive(false);
        }

        if (titleUI != null)
        {
            titleUI.SetActive(true);
        }

        var spawner = FindFirstObjectByType<ObstacleSpawner>();
        spawner?.ClearStage();
        ResetPlayerPosition();
        StageManager.Instance?.ResetStageIndex();
        FindFirstObjectByType<StageProgressByDistance>()?.ResetTransitionState();


        if (player != null)
        {
            player.SetActive(true);
        }

        if (mouseBarrier != null)
        {
            mouseBarrier.SetActive(false);
        }

        Time.timeScale = 0f;
    }

    private void ShowWaitUI()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }

        if (waitUI != null)
        {
            waitUI.SetActive(true);
        }

        if (scoreUI != null)
        {
            scoreUI.SetActive(false);
        }

        FindFirstObjectByType<GameWaitUIHandler>()?.EnableWaitInput();
        CleanupObstacles();

        // ステージ位置とインデックスを初期化
        var spawner = FindFirstObjectByType<ObstacleSpawner>();
        spawner?.ClearStage();
        spawner?.ResetSpawnHeight(10f);

        StageManager.Instance?.ResetStageIndex(); // これを追加！
        FindFirstObjectByType<StageProgressByDistance>()?.ResetTransitionState();

        ResetPlayerPosition();

        if (player != null)
        {
            var controller = player.GetComponent<PlayerController>();
            controller?.PrepareReset();
            controller?.ShowOnly();

            var camFollow = Camera.main.GetComponent<CameraFollow>();
            camFollow?.SetTarget(player.transform);
        }

        if (mouseBarrier != null)
        {
            mouseBarrier.SetActive(true);
        }

        Time.timeScale = 1f;
    }

    private void ResetPlayerPosition()
    {
        if (player != null)
        {
            player.transform.position = initialPlayerPosition;
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
    }
}