using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    private string baseUrl = "http://localhost:3000/api";
    public int currentUserId = -1; // Stores logged-in player ID

    [System.Serializable]
    public class AuthData
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

    public void RegisterUser(string username, string password, System.Action<string> onResponse)
    {
        StartCoroutine(PostRequest("/register", JsonUtility.ToJson(new AuthData { username = username, password = password }), onResponse));
    }

    public void LoginUser(string username, string password, System.Action<string> onResponse)
    {
        StartCoroutine(PostRequest("/login", JsonUtility.ToJson(new AuthData { username = username, password = password }), (response) => {
            // Simple parsing to grab playerId from response
            if (response.Contains("playerId"))
            {
                string[] parts = response.Split(',');
                foreach (var part in parts)
                {
                    if (part.Contains("playerId"))
                    {
                        int.TryParse(part.Split(':')[1].Replace("}", "").Trim(), out currentUserId);
                    }
                }
            }
            onResponse?.Invoke(response);
        }));
    }

    public void SaveGameResult(int score, int patients, int timeInSeconds, System.Action<string> onResponse)
    {
        if (currentUserId == -1)
        {
            onResponse?.Invoke("Error: Must log in first!");
            return;
        }

        ResultData data = new ResultData
        {
            playerId = currentUserId,
            score = score,
            patientsTreated = patients,
            completionTime = timeInSeconds
        };

        StartCoroutine(PostRequest("/results", JsonUtility.ToJson(data), onResponse));
    }

    private IEnumerator PostRequest(string endpoint, string jsonBody, System.Action<string> callback)
    {
        using (UnityWebRequest www = new UnityWebRequest(baseUrl + endpoint, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                callback?.Invoke(www.downloadHandler.text);
            }
            else
            {
                callback?.Invoke("Error: " + www.downloadHandler.text);
            }
        }
    }
}