using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class StageTransitionController : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI stageText;

    [Header("スクロール演出")]
    [SerializeField] private float scrollDuration = 4f;
    [SerializeField] private Vector2 startPos = new Vector2(0, 800);
    [SerializeField] private Vector2 endPos = new Vector2(0, -720);

    private RectTransform rect;

    private void Awake()
    {
        if (panel != null)
        {
            rect = panel.GetComponent<RectTransform>();
            rect.anchoredPosition = startPos;
            panel.SetActive(false);
        }
    }

    public IEnumerator PlayTransition(int stageNumber)
    {
        panel.SetActive(true);
        stageText.text = $"{stageNumber}";

        rect.anchoredPosition = startPos;

        float elapsed = 0f;
        while (elapsed < scrollDuration)
        {
            rect.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsed / scrollDuration);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        rect.anchoredPosition = endPos;

        yield return new WaitForSecondsRealtime(5f);

        panel.SetActive(false);
    }
}
