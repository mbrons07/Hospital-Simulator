using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HospitalTestUI : MonoBehaviour
{
    [Header("Network Reference")]
    public NetworkManager networkManager;

    [Header("Auth Inputs")]
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;

    [Header("Game Metric Inputs")]
    public TMP_InputField scoreInput;
    public TMP_InputField patientsInput;
    public TMP_InputField timeInput;

    [Header("Buttons")]
    public Button registerButton;
    public Button loginButton;
    public Button saveScoreButton;

    [Header("Status Feedback")]
    public TMP_Text statusText;

    private void Start()
    {
        registerButton.onClick.AddListener(OnRegisterClicked);
        loginButton.onClick.AddListener(OnLoginClicked);
        saveScoreButton.onClick.AddListener(OnSaveScoreClicked);
    }

    private void OnRegisterClicked()
    {
        statusText.text = "Registering...";
        networkManager.RegisterUser(usernameInput.text, passwordInput.text, (response) => {
            statusText.text = response;
        });
    }

    private void OnLoginClicked()
    {
        statusText.text = "Logging in...";
        networkManager.LoginUser(usernameInput.text, passwordInput.text, (response) => {
            statusText.text = response;
        });
    }

    private void OnSaveScoreClicked()
    {
        int score = int.TryParse(scoreInput.text, out int s) ? s : 100;
        int patients = int.TryParse(patientsInput.text, out int p) ? p : 5;
        int time = int.TryParse(timeInput.text, out int t) ? t : 180;

        statusText.text = "Saving score...";
        networkManager.SaveGameResult(score, patients, time, (response) => {
            statusText.text = response;
        });
    }
}