using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private ObstacleSplitter splitter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MouseBarrier"))
        {
            splitter.Split();
            Destroy(this); //��x���ꂽ��Փ˔��菈�����Ȃ�
        }   
    }
}
