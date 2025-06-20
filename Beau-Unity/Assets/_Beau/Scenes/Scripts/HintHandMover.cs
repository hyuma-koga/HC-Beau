using UnityEngine;

public class HintHandMover : MonoBehaviour
{
    [SerializeField] private float moveRange = 200f;   // 片道距離
    [SerializeField] private float moveSpeed =0.7f;     // 速さ
    [SerializeField] private GameObject waitUI;   // 待機画面UI

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.localPosition;
    }

    private void Update()
    {
        // GameReadyUIが表示中だけループ移動
        if (waitUI != null && waitUI.activeSelf)
        {
            float offset = Mathf.Sin(Time.unscaledTime * moveSpeed) * moveRange;
            transform.localPosition = startPos + new Vector3(offset, 0f, 0f);
        }
    }
}
