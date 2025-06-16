using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float riseSpeed = 2f; //上昇スピード

    private Vector3 initialPosition;
    private Rigidbody2D rb;
    private bool isStarted = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.linearVelocity = Vector2.zero;
        initialPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (!isStarted || rb == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = new Vector2(0f, riseSpeed);
    }

    public void StartRise()
    {
        isStarted = true;
    }

    // プレイヤーのステータスだけ初期化（表示はしない）
    public void PrepareReset()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        isStarted = false;
        rb.linearVelocity = Vector2.zero;
        transform.position = initialPosition;
        rb.simulated = true;
        rb.gravityScale = 0;

        Collider2D col = GetComponent<Collider2D>();

        if(col != null)
        {
            col.enabled = true;
        }

        GetComponent<PlayerCollisionHandle>()?.ResetCollision();
    }

    public void ShowOnly()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        gameObject.SetActive(true);
        isStarted = false;
        rb.linearVelocity = new Vector2(0f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameOverUIHandler.Instance?.ShowGameOverUI();
    }
}
