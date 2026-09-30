using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    private string baseUrl = "http://localhost:3000/api";

    [System.Serializable]
    public class LoginData
    {
        public string username;
        public string password;
    }

    [System.Serializable]
    public class ResultData
    {
        public int playerId;
        public int score;
        public int patientsTreated;
        public int completionTime;
    }

    // Call this method from your Login UI Button
    public void Login(string username, string password)
    {
        StartCoroutine(SendLoginRequest(username, password));
    }

    // Call this method when a hospital game session finishes
    public void SaveResult(int playerId, int score, int patientsTreated, int completionTime)
    {
        StartCoroutine(SendResultRequest(playerId, score, patientsTreated, completionTime));
    }

    private IEnumerator SendLoginRequest(string username, string password)
    {
        string url = baseUrl + "/login";
        LoginData data = new LoginData { username = username, password = password };
        string json = JsonUtility.ToJson(data);

        using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Response: " + www.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error: " + www.error);
            }
        }
    }

    private IEnumerator SendResultRequest(int playerId, int score, int patientsTreated, int completionTime)
    {
        string url = baseUrl + "/results";
        ResultData data = new ResultData 
        { 
            playerId = playerId, 
            score = score, 
            patientsTreated = patientsTreated, 
            completionTime = completionTime 
        };
        string json = JsonUtility.ToJson(data);

        using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Result Saved: " + www.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error saving result: " + www.error);
            }
        }
    }
}