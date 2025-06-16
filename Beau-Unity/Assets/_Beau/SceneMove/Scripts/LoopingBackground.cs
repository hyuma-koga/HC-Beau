using UnityEngine;

public class LoopingBackground : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float backgroundHeight = 0f; // é©ìÆéÊìæÉÇÅ[Éh
    [SerializeField] private float loopThresholdOffset = 2f;

    private void Start()
    {
        if (backgroundHeight == 0f)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                backgroundHeight = sr.bounds.size.y;
                Debug.Log($"[LoopingBackground] îwåiçÇÇ≥Çé©ìÆê›íË: {backgroundHeight}");
            }
            else
            {
                Debug.LogWarning("SpriteRenderer Ç™å©Ç¬Ç©ÇËÇ‹ÇπÇÒÇ≈ÇµÇΩ");
            }
        }
    }

    private void Update()
    {
        if (player == null) return;

        if (player.position.y > transform.position.y + backgroundHeight - loopThresholdOffset)
        {
            transform.position += new Vector3(0f, backgroundHeight * 2f, 0f);
        }
    }
}
