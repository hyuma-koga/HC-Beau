using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("�����ʒu�̊�i�I�v�V�����j")]
    [SerializeField] private Transform spawnRoot;

    private GameObject currentStageInstance;

    /// <summary>
    /// �w�肵��Y���W�ɃX�e�[�WPrefab�𐶐�
    /// </summary>
    /// <param name="spawnY">��������Y���W</param>
    public void SpawnStageAtY(float spawnY)
    {
        StageData stageData = StageManager.Instance?.CurrentStageData;
        if (stageData == null || stageData.stagePrefab == null)
        {
            Debug.LogWarning("StageData �܂��� stagePrefab �����ݒ�ł��B");
            return;
        }

        // �Â��X�e�[�W���폜
        if (currentStageInstance != null)
        {
            Debug.Log($"Destroying previous stage: {currentStageInstance.name}");
            Destroy(currentStageInstance);
        }

        // �V�����X�e�[�W�̐����ʒu������
        Vector3 basePos = spawnRoot != null ? spawnRoot.position : Vector3.zero;
        Vector3 spawnPos = new Vector3(basePos.x, spawnY, basePos.z);

        // �X�e�[�WPrefab�𐶐�
        Debug.Log($"Spawning new stage prefab: {stageData.stagePrefab.name} at Y={spawnY}");
        currentStageInstance = Instantiate(stageData.stagePrefab, spawnPos, Quaternion.identity);
    }

    /// <summary>
    /// ���݂̃X�e�[�WPrefab���폜
    /// </summary>
    public void ClearStage()
    {
        if (currentStageInstance != null)
        {
            Destroy(currentStageInstance);
            currentStageInstance = null;
        }
    }

    public void SpawnSpecificStage(StageData stageData)
    {
        if (stageData == null || stageData.stagePrefab == null)
        {
            Debug.LogWarning("StageData �܂��� stagePrefab �����ݒ�ł��B");
            return;
        }

        if (currentStageInstance != null)
        {
            Destroy(currentStageInstance);
        }

        Vector3 basePos = spawnRoot != null ? spawnRoot.position : Vector3.zero;
        Vector3 spawnPos = new Vector3(basePos.x, stageData.baseY, basePos.z);

        currentStageInstance = Instantiate(stageData.stagePrefab, spawnPos, Quaternion.identity);
    }

}
