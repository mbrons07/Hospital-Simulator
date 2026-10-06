using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int correctAnswers = 0;

    public int CorrectAnswers => correctAnswers;

    private void Start()
    {
        ResetScore();
    }

    public void AddPoint()
    {
        correctAnswers++;
        Debug.Log($"Score updated! Current score: {correctAnswers}");
    }

    public void ResetScore()
    {
        correctAnswers = 0;
    }
}