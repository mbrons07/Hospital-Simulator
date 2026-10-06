using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PatientDisplayUI : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;

    public void DisplayPatient(PatientData patient)
    {
        if (patient == null) return;

        nameText.text = patient.PatientName;
        descriptionText.text = $"\"{patient.Description}\"";
    }

    public void DisplayEndMessage(int finalScore, int totalPatients)
    {
        nameText.text = "Finished";
        descriptionText.text = $"All patients have been triaged.\n\nYour Final Score: {finalScore} / {totalPatients} correct";

        // Start the 5-second countdown timer
        StartCoroutine(ReturnToMainMenuAfterDelay(5f));
    }

    private IEnumerator ReturnToMainMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("Main Menu");
    }
}