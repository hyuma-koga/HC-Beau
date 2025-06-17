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
            rect.anchoredPosition = startPos; // 初期位置リセット
            panel.SetActive(false);
        }
    }

    public IEnumerator PlayTransition(int stageNumber)
    {
        if (panel == null || rect == null) yield break;

        rect.anchoredPosition = startPos; // 先に位置リセット
        stageText.text = $"{stageNumber}";
        panel.SetActive(true);            // 表示は位置セット後に

        float elapsed = 0f;
        while (elapsed < scrollDuration)
        {
            rect.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsed / scrollDuration);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        rect.anchoredPosition = endPos;

        // 少し表示キープ（任意）
        yield return new WaitForSeconds(0f);

        panel.SetActive(false);
    }
}
