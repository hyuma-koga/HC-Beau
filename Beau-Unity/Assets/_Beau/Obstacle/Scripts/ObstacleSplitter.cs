using UnityEngine;

public class ObstacleSplitter : MonoBehaviour
{
    [SerializeField] private GameObject wholeSprite;
    [SerializeField] private GameObject leftHalf;
    [SerializeField] private GameObject rightHalf;
    [SerializeField] private float splitForce = 1f;

    private bool isSplit = false;

    public void Split()
    {
        if (isSplit)
        {
            return;
        }

        isSplit = true;
        
        //�����X�v���C�g���\��
        wholeSprite.SetActive(false);
        leftHalf.SetActive(true);
        rightHalf.SetActive(true);

        //���E�ɕ����I�ȗ͂�������
        var leftRb = leftHalf.GetComponent<Rigidbody2D>();
        var rightRb = rightHalf.GetComponent<Rigidbody2D>();

        if (leftRb != null)
        {
            leftRb.AddForce(new Vector2(-splitForce, -splitForce), ForceMode2D.Impulse);
        }

        if (rightRb != null)
        {
            rightRb.AddForce(new Vector2(splitForce, -splitForce), ForceMode2D.Impulse);
        }
    }
}
