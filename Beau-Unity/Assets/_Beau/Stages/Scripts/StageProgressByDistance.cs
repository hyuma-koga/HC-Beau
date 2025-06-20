using UnityEngine;
using System.Collections;

public class StageProgressByDistance : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    [Tooltip("�ŏ��̃p�l���\���ƃX�e�[�W�������s��Y�ʒu")]
    [SerializeField] private float firstTransitionY = 10f;

    [Tooltip("���̃p�l���\���ƃX�e�[�W�����܂ł�Y�Ԋu")]
    [SerializeField] private float intervalY = 20f;

    private float nextTransitionY;
    private bool isTransitioning = false;

    private void Start()
    {
        if (playerTransform == null)
        {
            Debug.LogWarning("�v���C���[Transform���ݒ肳��Ă��܂���");
            enabled = false;
            return;
        }

        nextTransitionY = firstTransitionY;
    }

    private void Update()
    {
        if (!isTransitioning && playerTransform.position.y >= nextTransitionY)
        {
            StartCoroutine(HandleStageTransitionAndSpawn());
        }
    }

    private IEnumerator HandleStageTransitionAndSpawn()
    {
        isTransitioning = true;

        // �X�e�[�W�J�E���g+1 & UI�X�V
        StageManager.Instance.IncrementStage();
        int stageNumber = StageManager.Instance.GetCurrentStage();

        // �p�l���\���i�X�e�[�W�ԍ��Ɋ�Â��j
        yield return StartCoroutine(StageManager.Instance.PlayTransitionPanel(stageNumber));

        // �X�e�[�W���������iObstacle��w�i�̓K�p�j
        StageData stageData = StageManager.Instance.GetStageDataAt(stageNumber);
        if (stageData != null)
        {
            var spawner = FindFirstObjectByType<ObstacleSpawner>();
            spawner?.SpawnNextStage(stageData);
        }

        nextTransitionY += intervalY;
        isTransitioning = false;
    }

    public void ResetTransitionState()
    {
        nextTransitionY = firstTransitionY;
        isTransitioning = false;
    }
}
