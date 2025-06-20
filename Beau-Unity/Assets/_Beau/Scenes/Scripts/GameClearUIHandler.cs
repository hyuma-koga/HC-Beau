using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameClearUIHandler : MonoBehaviour
{
    [Header("UIオブジェクト")]
    [SerializeField] private GameObject gameClearUI;
    [SerializeField] private TMP_Text finalScoreText;

    [Header("参照")]
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mouseBarrier;
    [SerializeField] private GameObject titleUI;

    public void ShowGameClear()
    {
        Time.timeScale = 0f;

        if (gameClearUI != null)
        {
            gameClearUI.SetActive(true);
        }

        if (finalScoreText != null && scoreManager != null)
        {
            finalScoreText.text = $"{scoreManager.CurrentScore}";
        }

        if (player != null)
        {
            player.SetActive(false);
        }

        if (mouseBarrier != null)
        {
            mouseBarrier.SetActive(false);
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnTitleButtonPressed()
    {
        if (gameClearUI != null)
        {
            gameClearUI.SetActive(false);
        }

        if (titleUI != null)
        {
            titleUI.SetActive(true);
        }
        if (player != null)
        {
            player.transform.position = new Vector3(0f, -2.2f, 0f);

            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
            }

            player.SetActive(true);
        }

        if (mouseBarrier != null)
        {
            mouseBarrier.SetActive(false);
        }

        Time.timeScale = 0f;
    }
}
