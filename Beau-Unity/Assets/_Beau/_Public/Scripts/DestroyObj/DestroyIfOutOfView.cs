using UnityEngine;

public class DestroyIfOutOfView : MonoBehaviour
{
    [SerializeField] private string targetTag = "Destroyable";
    [SerializeField] private float offsetY = -5f; 
    [SerializeField] private Camera targetCamera;

    private void Start()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
    }

    private void Update()
    {
        float destroyY = targetCamera.transform.position.y + offsetY;

        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);

        foreach (GameObject obj in targets)
        {
            if (obj.transform.position.y < destroyY)
            {
                Destroy(obj);
            }
        }
    }
}