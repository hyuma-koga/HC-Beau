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

    public void ResetPlayer()
    {
        isStarted = false;
        rb.linearVelocity = Vector2.zero;

        // �v���C���[�������ʒu�ɖ߂��iZ = 0 ��2D�J�����Ɛ����j
        transform.position = new Vector3(initialPosition.x, initialPosition.y, 0f);
        gameObject.SetActive(true);

        // �J�����������I�Ƀv���C���[�ʒu�ɖ߂��i��ɏ����I�t�Z�b�g����j
        Camera.main.transform.position = new Vector3(
            Camera.main.transform.position.x,
            transform.position.y + 1f,  // �� offsetY�𒲐����ĉ�ʒ�����
            Camera.main.transform.position.z
        );
    }
}
