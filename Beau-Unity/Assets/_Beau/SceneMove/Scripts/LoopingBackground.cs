using UnityEngine;

public class LoopingBackground : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float backgroundHeight = 0f; // 自動取得モード
    [SerializeField] private float loopThresholdOffset = 2f;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;

        if (backgroundHeight == 0f)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                backgroundHeight = sr.bounds.size.y;
            }
        }
    }

    private void Update()
    {
        if (player == null) return;

        if (player.position.y > transform.position.y + backgroundHeight - loopThresholdOffset)
        {
            transform.position += new Vector3(0f, backgroundHeight * 2f, 0f);
        }
    }

    /// <summary>
    /// 背景位置を初期状態に戻す（ゲームリスタート用）
    /// </summary>
    public void ResetPosition()
    {
        transform.position = initialPosition;
    }
}
