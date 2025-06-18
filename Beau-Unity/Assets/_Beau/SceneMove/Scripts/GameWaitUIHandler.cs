using UnityEngine;

public class GameWaitUIHandler : MonoBehaviour
{
    [Header("UIオブジェクト")]
    [SerializeField] private GameObject waitUI;

    [Header("ゲームオブジェクト")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mouseBarrier;

    private bool waitingForClick = false;
    private bool skipFirstFrame = false;
    private int skipFrameCount = 0;

    public void EnableWaitInput()
    {
        waitingForClick = true;
        skipFirstFrame = true;
        skipFrameCount = 2; // フレームスキップ
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;

        if (waitUI != null)
        {
            waitUI.SetActive(true);
        }

        if (mouseBarrier != null)
        {
            mouseBarrier.SetActive(true);
        }

        if (player != null)
        {
            var controller = player.GetComponent<PlayerController>();
            controller?.ShowOnly();

            var camFollow = Camera.main.GetComponent<CameraFollow>();
            camFollow?.SetTarget(player.transform);
        }
    }

    private void Update()
    {
        if (!waitingForClick)
            return;

        if (skipFrameCount > 0)
        {
            skipFrameCount--;
            return;
        }

        if (skipFirstFrame)
        {
            skipFirstFrame = false;
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        if (waitUI != null)
        {
            waitUI.SetActive(false);
        }

        if (player != null)
        {
            var controller = player.GetComponent<PlayerController>();
            controller?.StartRise();
        }

        var backgrounds = FindObjectsByType<LoopingBackground>(FindObjectsSortMode.None);
        foreach (var bg in backgrounds)
        {
            bg.ResetPosition();
        }

        var spawner = FindFirstObjectByType<ObstacleSpawner>();
        spawner?.ClearStage(); // 念のため前のステージ削除
        spawner?.ResetSpawnHeight(10f); // ←  最初のステージのY位置を10に！

        StageManager.Instance?.ResetStageIndex();
        FindFirstObjectByType<StageProgressByDistance>()?.ResetTransitionState(); // ← 追加
        StageManager.Instance?.StartFirstStage();

        Time.timeScale = 1f;
        waitingForClick = false;
    }
}
