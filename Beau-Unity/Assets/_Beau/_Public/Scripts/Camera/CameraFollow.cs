using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float offsetY = 0f;

    private void FixedUpdate()
    {
        if (target == null) return;

        float balloonY = target.position.y + offsetY;
        float cameraY = transform.position.y;

        if (balloonY > cameraY)
        {
            transform.position = new Vector3(transform.position.x, balloonY, transform.position.z);
        }
    }
}
