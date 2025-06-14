using UnityEngine;

public class TitleUIController : MonoBehaviour
{
    [SerializeField] private GameObject titleUI;
    [SerializeField] private GameObject gameReadyUI;
    [SerializeField] private GameObject mouseBarrier;
    [SerializeField] private GameReadyUIController gameReadyUIController;

    private void Start()
    {
        titleUI.SetActive(true);
        gameReadyUI.SetActive(false);
        Time.timeScale = 0f;

        if (mouseBarrier != null)
        {
            mouseBarrier.SetActive(false);
        }
    }

    public void OnStartButtonPressed()
    {
        titleUI.SetActive(false);
        gameReadyUIController.ShowGameReadyUI();  // Å© UIï\é¶ + éûä‘í‚é~
    }
}
