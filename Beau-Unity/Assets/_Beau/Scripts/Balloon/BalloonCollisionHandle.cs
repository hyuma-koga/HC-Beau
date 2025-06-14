using UnityEngine;

public class BalloonCollisionHandle : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            //Balloonを非アクティブにして
            gameObject.SetActive(false);

            //GameOverUIを表示
            if(gameOverUI != null)
            {
                gameOverUI.SetActive(true);
            }
        }
    }
}
