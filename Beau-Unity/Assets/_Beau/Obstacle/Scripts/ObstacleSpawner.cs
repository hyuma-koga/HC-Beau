using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("�����ʒu�̊�i�I�v�V�����j")]
    [SerializeField] private Transform spawnRoot;

    private GameObject currentStageInstance;

    public void SpawnStageAtY(float spawnY)
    {
        StageData stageData = StageManager.Instance?.CurrentStageData;
        if (stageData == null || stageData.stagePrefab == null)
        {
            return;
        }

        if (currentStageInstance != null)
        {
            Destroy(currentStageInstance, 3f);
        }

        Vector3 basePos = spawnRoot != null ? spawnRoot.position : Vector3.zero;
        Vector3 spawnPos = new Vector3(basePos.x, spawnY, basePos.z);

        currentStageInstance = Instantiate(stageData.stagePrefab, spawnPos, Quaternion.identity);
    }

    public void ClearStage()
    {
        if (currentStageInstance != null)
        {
            Destroy(currentStageInstance, 3f);
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
            Destroy(currentStageInstance, 3f);
        }

        Vector3 basePos = spawnRoot != null ? spawnRoot.position : Vector3.zero;
        Vector3 spawnPos = new Vector3(basePos.x, stageData.baseY, basePos.z);

        currentStageInstance = Instantiate(stageData.stagePrefab, spawnPos, Quaternion.identity);
    }

}
