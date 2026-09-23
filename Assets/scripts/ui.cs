using UnityEngine;
using TMPro;
using Unity.VectorGraphics; 
using UnityEngine.SceneManagement;

public class ui : MonoBehaviour
{
    public TextMeshProUGUI patientText; 
    public int patientsTreatedCount = 0;

    void Start()
    {
        UpdateUI();
    }

    public void Update()
    {
        if (patientsTreatedCount >= 10)
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
    public void OnTreatPatient()
    {
        patientsTreatedCount++;
        UpdateUI();
    }
    public void OnMouseDown()
    {
        OnTreatPatient();
    }

    private void UpdateUI()
    {
        patientText.text = "patients treated: " + patientsTreatedCount;
    }
}