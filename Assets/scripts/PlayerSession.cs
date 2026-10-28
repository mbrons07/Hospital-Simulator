using UnityEngine;
using TMPro;

public class PlayerSession : MonoBehaviour
{
    public static PlayerSession Instance; // Singleton so other scripts can easily read it

    [SerializeField] private TMP_InputField nameInputField;
    
    private string playerName = "Anonymous";

    public string PlayerName => playerName;

    private void Awake()
    {
        // Ensures this object persists when switching to the game scene
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Call this function when the player clicks the "Gameloop" start button
    public void SavePlayerNameAndStart()
    {
        if (nameInputField != null && !string.IsNullOrEmpty(nameInputField.text))
        {
            playerName = nameInputField.text;
        }
    }
}