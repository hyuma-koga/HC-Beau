using UnityEngine;

public class HintHandMover : MonoBehaviour
{
    [SerializeField] private float moveRange = 200f;   // �Г�����
    [SerializeField] private float moveSpeed =0.7f;     // ����
    [SerializeField] private GameObject waitUI;   // �ҋ@���UI

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.localPosition;
    }

    private void Update()
    {
        // GameReadyUI���\�����������[�v�ړ�
        if (waitUI != null && waitUI.activeSelf)
        {
            float offset = Mathf.Sin(Time.unscaledTime * moveSpeed) * moveRange;
            transform.localPosition = startPos + new Vector3(offset, 0f, 0f);
        }
    }
}
