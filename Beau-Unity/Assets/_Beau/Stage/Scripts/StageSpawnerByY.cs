using UnityEngine;

public class StageSpawnerByY : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float preSpawnOffset = 10f; // ���߂ɐ������鋗���I�t�Z�b�g

    private int nextStageIndex = 1; // �ŏ���stageList[1]��ڎw���i[0]��Start�Ő����ρj
    private float nextSpawnY;

    private void Start()
    {
        UpdateNextSpawnY(); // ���̐���Y���W��������
    }

    private void Update()
    {
        if (playerTransform == null || StageManager.Instance == null)
            return;

        if (nextStageIndex >= StageManager.Instance.TotalStageCount)
            return;

        float playerY = playerTransform.position.y;

        if (playerY >= nextSpawnY - preSpawnOffset)
        {
            Debug.Log($"StageSpawnerByY: Player Y={playerY} �� baseY={nextSpawnY} �� {preSpawnOffset} ��O�� �� NextStage() �Ăяo��");

            // ���̃X�e�[�W����index�w��Ŏ擾�i���S�j
            StageData upcomingStage = StageManager.Instance.GetStageDataAt(nextStageIndex);

            StageManager.Instance.NextStage(); // ����
            nextStageIndex++;

            // baseY�𒼐ڎQ�Ƃ����A���łɎ擾����StageData���g��
            if (upcomingStage != null)
            {
                nextSpawnY = upcomingStage.baseY;
                Debug.Log($"���̃X�e�[�W����Y = {nextSpawnY}");
            }
            else
            {
                Debug.LogWarning("���̃X�e�[�W�f�[�^�� null �ł�");
            }
        }
    }

    private void UpdateNextSpawnY()
    {
        if (StageManager.Instance == null || nextStageIndex >= StageManager.Instance.TotalStageCount)
        {
            Debug.Log(" ���ׂẴX�e�[�W�𐶐����I���܂���");
            return;
        }

        StageData nextStage = StageManager.Instance.GetStageDataAt(nextStageIndex);
        if (nextStage != null)
        {
            nextSpawnY = nextStage.baseY;
            Debug.Log($"���̃X�e�[�W����Y = {nextSpawnY}");
        }
    }
}
