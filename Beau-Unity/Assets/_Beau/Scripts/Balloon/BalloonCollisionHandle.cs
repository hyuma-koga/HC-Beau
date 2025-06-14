using UnityEngine;

public class BalloonCollisionHandle : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            //Balloon���A�N�e�B�u�ɂ���
            gameObject.SetActive(false);

            //GameOverUI��\��
            if(gameOverUI != null)
            {
                gameOverUI.SetActive(true);
            }
        }
    }
}
