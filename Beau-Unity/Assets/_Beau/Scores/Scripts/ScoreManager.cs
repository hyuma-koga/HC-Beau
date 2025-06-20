using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private float scorePerSecond = 4f;
    private float currentScore = 0f;
    private bool isGameRunning = false;

    public float CurrentScore => Mathf.FloorToInt(currentScore);

    public void StartScore()
    {
        isGameRunning = true;
    }

    public void StopScore()
    {
        isGameRunning = false;
    }

    public void ResetScore()
    {
        currentScore = 0f;
    }

    private void Update()
    {
        if (isGameRunning)
        {
            currentScore += scorePerSecond * Time.deltaTime;
        }
    }
}
