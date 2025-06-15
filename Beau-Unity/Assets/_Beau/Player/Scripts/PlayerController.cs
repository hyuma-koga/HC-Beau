using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float riseSpeed = 2f; //�㏸�X�s�[�h

    private Vector3 initialPosition;
    private Rigidbody2D rb;
    private bool isStarted = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.linearVelocity = Vector2.zero;
        initialPosition = transform.position;
        Debug.Log($"[Start] �����ʒu: {initialPosition}");
    }

    private void FixedUpdate()
    {
        if (!isStarted || rb == null)
        {
            rb.linearVelocity = Vector2.zero;
            Debug.Log($"[FixedUpdate] isStarted: false �܂��� rb���ݒ� �� �㏸���Ȃ� (vel={rb?.linearVelocity})");
            return;
        }

        rb.linearVelocity = new Vector2(0f, riseSpeed);
        Debug.Log($"[FixedUpdate] isStarted: true �� �㏸�� (vel={rb.linearVelocity})");
    }

    public void StartRise()
    {
        isStarted = true;
        Debug.Log("[StartRise] �v���C���[�㏸�J�n");
    }

    // �v���C���[�̃X�e�[�^�X�����������i�\���͂��Ȃ��j
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

        Debug.Log($"[PrepareReset] ��ԃ��Z�b�g: pos={transform.position}, vel={rb.linearVelocity}");
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
