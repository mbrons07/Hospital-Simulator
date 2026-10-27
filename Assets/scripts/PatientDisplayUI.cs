using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PatientDisplayUI : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text medicalHistoryText;

    [Header("Scene Settings")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    [SerializeField] private float delayBeforeReturn = 5f;

    public void DisplayPatient(PatientData patient)
    {
        if (patient == null) return;

        nameText.text = patient.PatientName;
        descriptionText.text = $"\"{patient.Description}\"";

        if (medicalHistoryText != null)
        {
            medicalHistoryText.text = $"{patient.MedicalHistory}";
        }
    }

    public void DisplayEndMessage(int finalScore, int totalPatients)
    {
        nameText.text = "Finished";
        descriptionText.text = $"All patients have been triaged.\n\nYour Final Score: {finalScore} / {totalPatients} correct";

        if (medicalHistoryText != null)
        {
            medicalHistoryText.text = "";
        }

        StartCoroutine(ReturnToMainMenuAfterDelay(delayBeforeReturn));
    }

    private IEnumerator ReturnToMainMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(mainMenuSceneName);
    }
}