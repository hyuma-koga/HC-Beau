using UnityEngine;

public class BalloonController : MonoBehaviour
{
    [SerializeField] private float riseSpeed = 2f; //上昇スピード

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; //インスペクターで0にしているが、念のため
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(0f, riseSpeed);
    }
}
