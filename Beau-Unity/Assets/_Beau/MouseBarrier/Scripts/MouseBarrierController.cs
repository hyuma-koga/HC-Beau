using UnityEngine;

public class MouseBarrierController : MonoBehaviour
{
    [SerializeField] private float minX = -2.5f;
    [SerializeField] private float maxX = 2.5f;
    [SerializeField] private float minYOffset = -4.6f;
    [SerializeField] private float maxYOffset = 4.6f;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;

        // ���[���h���W�ɕϊ�
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);

        //�J����Y�ɑ΂��鑊�ΐ���
        float minY = mainCamera.transform.position.y + minYOffset;
        float maxY = mainCamera.transform.position.y + maxYOffset;

        float clampedX = Mathf.Clamp(worldPos.x, minX, maxX);
        float clampedY = Mathf.Clamp(worldPos.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, 0f);
    }
}
