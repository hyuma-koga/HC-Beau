using UnityEngine;

public class PlayerCollisionHandle : MonoBehaviour
{
    [SerializeField] private GameObject mouseBarrier;
    [SerializeField] private GameOverUIHandler gameOverUIHandler;

    private bool isGameOver = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isGameOver || !collision.gameObject.CompareTag("Obstacle"))
        {
            return;
        }

        isGameOver = true;

        gameObject.SetActive(false);

        if (mouseBarrier != null)
        {
            mouseBarrier.SetActive(false);
        }

        Cursor.visible = false;

        if(gameOverUIHandler != null)
        {
            gameOverUIHandler.ShowGameOverUI();
        }

        Debug.Log("�Q�[���I�[�o�[:�v���C���[�ƃo���A��\���AUI�\��");
    }

    public void ResetCollision()
    {
        isGameOver = false;
    }
}