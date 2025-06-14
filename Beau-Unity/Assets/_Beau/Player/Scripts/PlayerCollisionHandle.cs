using UnityEngine;

public class PlayerCollisionHandle : MonoBehaviour
{
    [SerializeField] private GameObject mouseBarrier;
    [SerializeField] private GameOverCountdownController countdownController;

    private bool isGameOver = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isGameOver || !collision.gameObject.CompareTag("Obstacle"))
        {
            return;
        }

        isGameOver = true;

        //Balloonを非アクティブにして
        gameObject.SetActive(false);

        //マウスバリアとカーソルを無効に
        if (mouseBarrier != null)
        {
            mouseBarrier.SetActive(false);
        }

        Cursor.visible = false;

        //カウントダウン開始
        if (countdownController != null)
        {
            countdownController.StartCountdown();
        }
    }
}