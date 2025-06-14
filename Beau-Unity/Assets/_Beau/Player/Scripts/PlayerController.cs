using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float riseSpeed = 2f; //�㏸�X�s�[�h

    private Rigidbody2D rb;
    private bool isStarted = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.linearVelocity = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if (!isStarted)
        {
            return;
        }

        rb.linearVelocity = new Vector2(0f, riseSpeed);
    }

    public void StartRise()
    {
        isStarted = true;
    }
}
