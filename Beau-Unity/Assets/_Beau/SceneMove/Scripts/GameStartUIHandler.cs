using UnityEngine;

public class GameStartUIHandler : MonoBehaviour
{
    [Header("UIオブジェクト")]
    [SerializeField] private GameObject titleUI;
    [SerializeField] private GameObject waitUI;
    [SerializeField] private ScoreUIController scoreUI;

    [Header("ゲームオブジェクト")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mouseBarrier;

    private Vector3 initialPlayerPosition = new Vector3(0f, -2.2f, 0f);

    private void Start()
    {
        if(waitUI != null)
        {
            waitUI.SetActive(false);
        }

        if(mouseBarrier != null)
        {
            mouseBarrier.SetActive(false);
        }

        var background = FindFirstObjectByType<LoopingBackground>();
        background?.ResetPosition();

        scoreUI?.SetActive(false);
    }

    private void OnEnable()
    {
        if(titleUI != null)
        {
            titleUI.SetActive(true);
        }

        if(waitUI != null)
        {
            waitUI.SetActive(false);
        }

        if(player != null)
        {
            var controller = player.GetComponent<PlayerController>();
            controller?.ShowOnly();

            var camFollow = Camera.main.GetComponent<CameraFollow>();
            camFollow?.SetTarget(player.transform);
        }

        if(mouseBarrier != null)
        {
            mouseBarrier.SetActive(false);
        }
        scoreUI?.SetActive(false);

        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnStartButtonPressed()
    {
        if(titleUI != null)
        {
            titleUI.SetActive(false);
        }

        if(waitUI != null)
        {
            waitUI.SetActive(true);
        }

        if(player != null)
        {
            var controller = player.GetComponent<PlayerController>();
            controller?.ShowOnly();
        }

        if(mouseBarrier != null)
        {
            mouseBarrier.SetActive(true);
        }

        Time.timeScale = 0f;

        FindFirstObjectByType<GameWaitUIHandler>()?.EnableWaitInput();
    }
}
