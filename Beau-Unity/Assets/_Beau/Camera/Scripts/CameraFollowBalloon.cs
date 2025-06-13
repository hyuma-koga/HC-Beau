using UnityEngine;

public class CameraFollowBalloon : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float offsetY = 0f; //���D���ǂꂾ����ɃJ������u����

    private float initialY; //�J�����̊J�n�ʒu

    private void Start()
    {
        if(target == null)
        {
            enabled = false;
            return;
        }

        initialY = transform.position.y;
    }

    private void LateUpdate()
    {
        float balloonY = target.position.y + offsetY;
        float cameraY = transform.position.y;

        if(balloonY > cameraY)
        {
           transform.position = new Vector3(transform.position.x, balloonY, transform.position.z);
        }
    }
}
