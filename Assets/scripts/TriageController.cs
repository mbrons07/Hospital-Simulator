using System.Collections.Generic;
using UnityEngine;

public class TriageController : MonoBehaviour
{
    [Header("Patient List")]
    [SerializeField] private List<PatientData> patientQueue = new List<PatientData>();

    [Header("Dependencies")]
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private PatientDisplayUI displayUI;

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

        if (selectedCategory == currentPatient.CorrectCategory)
        {
            scoreManager.AddPoint();
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