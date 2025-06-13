using UnityEngine;

public class BalloonController : MonoBehaviour
{
    [SerializeField] private float riseSpeed = 2f; //�㏸�X�s�[�h

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; //�C���X�y�N�^�[��0�ɂ��Ă��邪�A�O�̂���
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(0f, riseSpeed);
    }
}
