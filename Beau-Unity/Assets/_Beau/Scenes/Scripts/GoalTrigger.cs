using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"[GoalTrigger] Triggered by: {other.name}, Tag: {other.tag}");

        if (other.CompareTag("Player"))
        {
            Debug.Log("GoalTrigger: Player reached goal!");
            FindFirstObjectByType<GameClearUIHandler>()?.ShowGameClear();
        }
    }
}
