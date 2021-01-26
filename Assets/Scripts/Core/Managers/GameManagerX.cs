using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManagerX : MonoBehaviour
{
    private float _startTick;
    private const string LanguageKey = "language";
    private const string LevelsCompletedKey = "levels-completed";

    public int MainMenuSceneIndex;
    public int FirstLevelSceneIndex = 1;
    public int LastLevelSceneIndex = 0;
    public string PauseButton = "Cancel";
    
    public bool IsPaused { get; private set; }
    public float TimeFromStart => Time.time - _startTick;

    public UnityEvent OnPause;
    public UnityEvent OnResume;
    public UnityEvent OnQuit;
    public UnityEvent OnCompleteLevel;
    public UnityEvent OnLanguageChanged;

    public string Language
    {
        get => PlayerPrefs.GetString(LanguageKey);
        set
        {
            PlayerPrefs.SetString(LanguageKey, value);
            OnLanguageChanged?.Invoke();
        }
    }

    private int LevelsCompleted
    {
        get => PlayerPrefs.GetInt(LevelsCompletedKey);
        set => PlayerPrefs.SetInt(LevelsCompletedKey, value);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(PauseButton))
        {
            if (IsPaused)
                Resume();
            else
                Pause();
        }
     
    }

    private void Start()
    {
        _startTick = Time.time;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 1000;
    }

    public void Pause()
    {
        IsPaused = true;
        Time.timeScale = 0f;
        OnPause.Invoke();
    }

    public void Resume()
    {
        IsPaused = false;
        Time.timeScale = 1f;
        OnResume.Invoke();
    }

    public void Quit()
    {
        Resume();
        OnQuit.Invoke();
    }

    public void Exit()
    {
        Resume();
        Application.Quit();
        Debug.Log("Application exit");
    }

    public void CompleteLevel()
    {
        int currLevel = SceneManager.GetActiveScene().buildIndex - FirstLevelSceneIndex + 1;

        if (LevelsCompleted < currLevel)
            LevelsCompleted = currLevel;
        OnCompleteLevel?.Invoke();
    }

    public void RestartLevel()
    {
        Resume();
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadLastAvailableLevel()
    {
        Resume();
        int sceneIndex = Math.Min(LastLevelSceneIndex, FirstLevelSceneIndex + LevelsCompleted);
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadLevel(int level)
    {
        Resume();
        int sceneIndex = FirstLevelSceneIndex + (level - 1);
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadNextLevel()
    {
        Resume();
        int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
       
        if(sceneIndex > LastLevelSceneIndex)
            Debug.LogWarning($"Scene {sceneIndex} is out of specified level's range");
        
        if(sceneIndex <  SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(sceneIndex);
        else
            GoMainMenu();
    }

    public void GoMainMenu()
    {
        Resume();
        SceneManager.LoadScene(MainMenuSceneIndex);
    }
}