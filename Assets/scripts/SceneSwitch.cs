using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void SwitchScene()
    {
        SceneManager.LoadScene("Hospital");
    }
}
