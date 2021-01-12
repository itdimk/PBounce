using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class GameManagerX : MonoBehaviour
{
    [Serializable]
    public class LevelsInfo
    {
        public int FirstLevelSceneIndex = 0;
        public int LastLevelSceneIndex = 0;
    }

    public const string LanguageKey = "language";
    public const string DifficultyKey = "difficulty";
    public const string LevelsCompletedKey = "levels-completed";

    public UnityEvent OnPause;
    public UnityEvent OnResume;
    public UnityEvent OnQuit;
    public UnityEvent OnCompleteLevel;

    public string PauseButton = "Cancel";
    public LevelsInfo Levels;
    [HideInInspector] public bool IsPaused;

    public string Language
    {
        get => PlayerPrefs.GetString(LanguageKey);
        set => PlayerPrefs.SetString(LanguageKey, value);
    }

    public int Difficulty
    {
        get => PlayerPrefs.GetInt(DifficultyKey);
        set => PlayerPrefs.SetInt(DifficultyKey, value);
    }

    public int LevelsCompleted
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
        int currLevel = SceneManager.GetActiveScene().buildIndex - Levels.FirstLevelSceneIndex + 1;

        if (LevelsCompleted < currLevel)
            LevelsCompleted = currLevel;
        OnCompleteLevel?.Invoke();
    }

    public void RestartLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadLastAvailableLevel()
    {
        int sceneIndex = Math.Min(Levels.LastLevelSceneIndex, Levels.FirstLevelSceneIndex + LevelsCompleted);
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadLevel(int level)
    {
        int sceneIndex = Levels.FirstLevelSceneIndex + (level - 1);
        SceneManager.LoadScene(sceneIndex);
    }
}