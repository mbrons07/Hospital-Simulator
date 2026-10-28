using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class TriageRecord
{
    public string patient_name;
    public string selected_category;
    public string correct_category;
    public bool is_correct;
}

public class SupabaseHandler : MonoBehaviour
{
    // Paste your Supabase URL and Anon Key here
    [Header("Supabase Settings")]
    [SerializeField] private string supabaseUrl = "https://viffiaevizinqegisdbf.supabase.co/rest/v1/";
    [SerializeField] private string supabaseAnonKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZpZmZpYWV2aXppbnFlZ2lzZGJmIiwicm9sZSI6ImFub24iLCJpYXQiOjE3OTMxNTc2NzQsImV4cCI6MjEwODczMzY3NH0.8ODBsjVKiSDRPUei5FQrSBDmsi5XKnkhZmWfz6-RFKY";

    public void SendTriageResult(string patientName, string selectedCategory, string correctCategory, bool isCorrect)
    {
        StartCoroutine(PostToSupabase(patientName, selectedCategory, correctCategory, isCorrect));
    }

    private IEnumerator PostToSupabase(string name, string selected, string correct, bool isCorrect)
    {
        string endpoint = $"{supabaseUrl}/rest/v1/triage_logs";

        TriageRecord record = new TriageRecord
        {
            patient_name = name,
            selected_category = selected,
            correct_category = correct,
            is_correct = isCorrect
        };

        string jsonPayload = JsonUtility.ToJson(record);

        using (UnityWebRequest www = new UnityWebRequest(endpoint, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            
            // Required Supabase Headers
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("apikey", supabaseAnonKey);
            www.SetRequestHeader("Authorization", $"Bearer {supabaseAnonKey}");
            
            // Tells Supabase to return the created row back in the response (optional)
            www.SetRequestHeader("Prefer", "return=representation");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Supabase Error: " + www.error);
                Debug.LogError("Response: " + www.downloadHandler.text);
            }
            else
            {
                Debug.Log("Successfully saved triage log to Supabase!");
            }
        }
    }
}