
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class MainMenuManagerScript : MonoBehaviour
{
   
    public static MainMenuManagerScript Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        
    }
    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
    public void ChangeSettings(SettingsType type)
    {

    }
  
    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

public enum SettingsType
{
    FOV,
    Volume,
    MouseSensitivity,
    MouseInversion,
}