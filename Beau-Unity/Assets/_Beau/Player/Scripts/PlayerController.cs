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

        // プレイヤーを初期位置に戻す（Z = 0 で2Dカメラと整合）
        transform.position = new Vector3(initialPosition.x, initialPosition.y, 0f);
        gameObject.SetActive(true);

        // カメラを強制的にプレイヤー位置に戻す（上に少しオフセットあり）
        Camera.main.transform.position = new Vector3(
            Camera.main.transform.position.x,
            transform.position.y + 1f,  // ← offsetYを調整して画面中央に
            Camera.main.transform.position.z
        );
    }
}
