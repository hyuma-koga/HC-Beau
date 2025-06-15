using UnityEngine;

public class GameWaitUIHandler : MonoBehaviour
{
    [Header("UI�I�u�W�F�N�g")]
    [SerializeField] private GameObject waitUI;

    [Header("�Q�[���I�u�W�F�N�g")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mouseBarrier;
    [SerializeField] private ObstacleSpawner obstacleSpawner;

    private bool waitingForClick = false;
    private bool skipFirstFrame = false;
    private int skipFrameCount = 0;

    public void EnableWaitInput()
    {
        waitingForClick = true;
        skipFirstFrame = true;
        skipFrameCount = 2; // �t���[���X�L�b�v
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;

        if(waitUI != null)
        {
            waitUI.SetActive(true);
        }

        if(mouseBarrier != null)
        {
            mouseBarrier.SetActive(true);
        }

        if(player != null)
        {
            var controller = player.GetComponent<PlayerController>();
            controller?.ShowOnly();

            var camFollow = Camera.main.GetComponent<CameraFollow>();
            camFollow?.SetTarget(player.transform);
        }

        Debug.Log("�ҋ@��ʁF�N���b�N�҂���Ԃ�");
    }

    private void Update()
    {
        if (!waitingForClick)
        {
            return;
        }

        if(skipFrameCount > 0)
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
        if(waitUI != null)
        {
            waitUI.SetActive(false);
        }

        if(player != null)
        {
            var controller = player.GetComponent<PlayerController>();
            controller?.StartRise();
        }

        if(obstacleSpawner != null)
        {
            obstacleSpawner.SpawnObstacleLine();
        }

        Time.timeScale = 1f;
        waitingForClick = false;

        Debug.Log("�Q�[���J�n�F�N���b�N�ɂ悿�ҋ@��ʂ��I��");
    }
}
