using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float offsetY = 0f;

    private void LateUpdate()
    {
        if (target == null) return;

        float balloonY = target.position.y + offsetY;
        transform.position = new Vector3(transform.position.x, balloonY, transform.position.z);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
