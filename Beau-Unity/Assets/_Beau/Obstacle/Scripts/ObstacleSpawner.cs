using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("�����ʒu�̊�i�ȗ��j")]
    [SerializeField] private Transform spawnRoot;

    [Header("�X�e�[�W�Ԃ�Y�Ԋu")]
    [SerializeField] private float spacingY = 20f;

    [Header("�ő�ێ��X�e�[�W��")]
    [SerializeField] private int maxStageInstances = 2;

    private List<GameObject> stageInstances = new List<GameObject>();
    private float lastSpawnY = 0f;

    /// <summary>
    /// ���݂�StageData���A����Y�ʒu�ɐςݏグ�Đ���
    /// </summary>
    public void SpawnNextStage(StageData stageData)
    {
        if (stageData == null || stageData.stagePrefab == null)
        {
            Debug.LogWarning("SpawnNextStage: �X�e�[�W�f�[�^�܂��̓v���n�u�����ݒ�ł��B");
            return;
        }

        Vector3 basePos = spawnRoot != null ? spawnRoot.position : Vector3.zero;
        Vector3 spawnPos = new Vector3(basePos.x, lastSpawnY, basePos.z);

        var instance = Instantiate(stageData.stagePrefab, spawnPos, Quaternion.identity);
        stageInstances.Add(instance);

        lastSpawnY += spacingY;

        if (stageInstances.Count > maxStageInstances)
        {
            Destroy(stageInstances[0]);
            stageInstances.RemoveAt(0);
        }
    }

    /// <summary>
    /// �w���StageData���AbaseY�ʒu�ɐ����iStageManager�p�j
    /// </summary>
    public void SpawnSpecificStage(StageData stageData)
    {
        if (stageData == null || stageData.stagePrefab == null)
        {
            Debug.LogWarning("SpawnSpecificStage: �X�e�[�W�f�[�^�܂��̓v���n�u�����ݒ�ł��B");
            return;
        }

        Vector3 basePos = spawnRoot != null ? spawnRoot.position : Vector3.zero;
        Vector3 spawnPos = new Vector3(basePos.x, stageData.baseY, basePos.z);

        var instance = Instantiate(stageData.stagePrefab, spawnPos, Quaternion.identity);
        stageInstances.Add(instance);

        if (stageInstances.Count > maxStageInstances)
        {
            Destroy(stageInstances[0]);
            stageInstances.RemoveAt(0);
        }

        // lastSpawnY�͕ύX���Ȃ� �� StageManager��baseY�Ő��䂵�Ă��邽��
    }

    /// <summary>
    /// �����I�ɃX�e�[�W���폜�i�ăX�^�[�g�p�j
    /// </summary>
    public void ClearStage()
    {
        foreach (var instance in stageInstances)
        {
            if (instance != null)
            {
                Destroy(instance);
            }
        }
        stageInstances.Clear();
    }

    /// <summary>
    /// �X�e�[�W�����ʒu�̏������i�Q�[���J�n���ɌĂԁj
    /// </summary>
    public void ResetSpawnHeight(float y)
    {
        lastSpawnY = y;
    }

    /// <summary>
    /// ����X�e�[�W�p�ɁA����Y�ʒu��1�񂾂�����
    /// </summary>
    public void SpawnStageAtY(float spawnY)
    {
        StageData stageData = StageManager.Instance?.CurrentStageData;
        if (stageData == null || stageData.stagePrefab == null)
            return;

        Vector3 basePos = spawnRoot != null ? spawnRoot.position : Vector3.zero;
        Vector3 spawnPos = new Vector3(basePos.x, spawnY, basePos.z);

        var instance = Instantiate(stageData.stagePrefab, spawnPos, Quaternion.identity);
        stageInstances.Add(instance);

        lastSpawnY = spawnY + spacingY;
    }
}
