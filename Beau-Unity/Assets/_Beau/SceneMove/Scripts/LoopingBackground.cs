using UnityEngine;

public class LoopingBackground : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float backgroundHeight = 0f; // �����擾���[�h
    [SerializeField] private float loopThresholdOffset = 2f;

    private void Start()
    {
        if (backgroundHeight == 0f)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                backgroundHeight = sr.bounds.size.y;
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
