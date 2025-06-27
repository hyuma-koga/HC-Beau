using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private ScoreUIController scoreUI;
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
        scoreManager?.ResetScore();
        scoreManager?.StartScore();
        scoreUI?.SetActive(true);
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

        scoreManager?.StopScore();
        scoreUI?.SetActive(false);

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
        scoreUI?.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle")) // ← 落下や障害物など
        {
            GameOverUIHandler.Instance?.ShowGameOverUI();
        }

        if (other.CompareTag("Goal"))
        {
            FindFirstObjectByType<GameClearUIHandler>()?.ShowGameClear();
        }
    }
}
