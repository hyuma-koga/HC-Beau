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

        //Balloon���A�N�e�B�u�ɂ���
        gameObject.SetActive(false);

        //�}�E�X�o���A�ƃJ�[�\���𖳌���
        if (mouseBarrier != null)
        {
            mouseBarrier.SetActive(false);
        }

        Cursor.visible = false;

        //�J�E���g�_�E���J�n
        if (countdownController != null)
        {
            countdownController.StartCountdown();
        }
    }
}