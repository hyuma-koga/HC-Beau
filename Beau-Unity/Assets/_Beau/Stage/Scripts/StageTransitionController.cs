using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class StageTransitionController : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI stageText;

    [Header("�X�N���[�����o")]
    [SerializeField] private float scrollDuration = 4f;
    [SerializeField] private Vector2 startPos = new Vector2(0, 800);
    [SerializeField] private Vector2 endPos = new Vector2(0, -720);

    private RectTransform rect;

    private void Awake()
    {
        if (panel != null)
        {
            rect = panel.GetComponent<RectTransform>();
            rect.anchoredPosition = startPos; // �����ʒu���Z�b�g
            panel.SetActive(false);
        }
    }

    public IEnumerator PlayTransition(int stageNumber)
    {
        if (panel == null || rect == null) yield break;

        rect.anchoredPosition = startPos; // ��Ɉʒu���Z�b�g
        stageText.text = $"{stageNumber}";
        panel.SetActive(true);            // �\���͈ʒu�Z�b�g���

        float elapsed = 0f;
        while (elapsed < scrollDuration)
        {
            rect.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsed / scrollDuration);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        rect.anchoredPosition = endPos;

        // �����\���L�[�v�i�C�Ӂj
        yield return new WaitForSeconds(0f);

        panel.SetActive(false);
    }
}
