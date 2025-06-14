using UnityEngine;

public class MouseBarrierController : MonoBehaviour
{
    private Camera mainCamera;

    // 制限範囲（ワールド座標で設定）
    [SerializeField] private float minX = -2.5f;
    [SerializeField] private float maxX = 2.5f;
    [SerializeField] private float minYOffset = -4.6f;
    [SerializeField] private float maxYOffset = 4.6f;

    void Start()
    {
        mainCamera = Camera.main;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;

        // ワールド座標に変換
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);

        //カメラYに対する相対制限
        float minY = mainCamera.transform.position.y + minYOffset;
        float maxY = mainCamera.transform.position.y + maxYOffset;

        float clampedX = Mathf.Clamp(worldPos.x, minX, maxX);
        float clampedY = Mathf.Clamp(worldPos.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, 0f);
    }
}
