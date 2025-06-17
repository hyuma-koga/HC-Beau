using UnityEngine;
using System.Collections;

public class StageProgressByDistance : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    [Tooltip("�ŏ���Transition�p�l����\������Y�ʒu")]
    [SerializeField] private float firstTransitionY = 10f;

    [Tooltip("���̃p�l���\���܂ł�Y�Ԋu")]
    [SerializeField] private float intervalY = 20f;

    private float nextTransitionY;
    private int stageCounter = 1;
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
            Debug.Log($"Player Y={playerTransform.position.y} ���B �� Stage�i�s������");
            StartCoroutine(HandleStageTransitionOnly());
        }
    }

    private IEnumerator HandleStageTransitionOnly()
    {
        isTransitioning = true;

        // �p�l���\���̂݁i�X�e�[�W�����Ȃ��j
        StageManager.Instance?.ShowTransitionPanelOnly(stageCounter);

        stageCounter++;
        nextTransitionY += intervalY;

        yield return new WaitForSecondsRealtime(2f); // �C�ӂ̑ҋ@����

        isTransitioning = false;
    }
}