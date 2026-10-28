using System.Collections.Generic;
using UnityEngine;

public class TriageController : MonoBehaviour
{
    [Header("Patient List")]
    [SerializeField] private List<PatientData> patientQueue = new List<PatientData>();

    [Header("Dependencies")]
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private PatientDisplayUI displayUI;

    [Header("Database Bridge")]
    [SerializeField] private SupabaseHandler supabaseHandler;

    private int currentIndex = 0;

    private void Start()
    {
        ShowCurrentPatient();
    }

public void SubmitAnswer(int categoryIndex)
{
    if (currentIndex >= patientQueue.Count) return;

    PatientData currentPatient = patientQueue[currentIndex];
    TriageCategory selectedCategory = (TriageCategory)categoryIndex;

    bool isCorrect = (selectedCategory == currentPatient.CorrectCategory);

    if (isCorrect)
    {
        scoreManager.AddPoint();
    }

    // Get the name the player typed on the menu screen
    string activePlayerName = "Anonymous";
    if (PlayerSession.Instance != null)
    {
        activePlayerName = PlayerSession.Instance.PlayerName;
    }

    // SEND DATA TO SUPABASE WITH THE PLAYER'S TYPED NAME
    if (supabaseHandler != null)
    {
        supabaseHandler.SendTriageResult(
            activePlayerName,                     // Player's name goes here!
            selectedCategory.ToString(), 
            currentPatient.CorrectCategory.ToString(), 
            isCorrect
        );
    }

    currentIndex++;
    ShowCurrentPatient();
}

    private void ShowCurrentPatient()
    {
        if (currentIndex < patientQueue.Count)
        {
            displayUI.DisplayPatient(patientQueue[currentIndex]);
        }
        else
        {
            displayUI.DisplayEndMessage(scoreManager.CorrectAnswers, patientQueue.Count);
        }
    }
}