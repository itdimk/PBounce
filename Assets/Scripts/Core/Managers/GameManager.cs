using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float _startTick;
    private const string LevelsCompletedKey = "levels-completed";

    public string PauseButton = "Cancel";

    public int MainMenuSceneIndex;
    public int FirstLevelSceneIndex = 1;
    public int LastLevelSceneIndex = 1;

    public bool IsPaused => Mathf.Abs(Time.timeScale) <= float.Epsilon;
    public float TimeFromStart => Time.time - _startTick;

    public UnityEvent Paused;
    public UnityEvent Resumed;
    public UnityEvent LevelCompleted;
    public UnityEvent LoadingLevel;
    
    private int LevelsCompleted
    {
        get => PlayerPrefs.GetInt(LevelsCompletedKey);
        set => PlayerPrefs.SetInt(LevelsCompletedKey, value);
    }

    private void Update()
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
        Time.timeScale = 0f;
        Paused?.Invoke();
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        Resumed?.Invoke();
    }

    public void Exit()
    {
        Resume();
        Application.Quit();
        Debug.Log("Application successfully exited");
    }

    public void CompleteLevel()
    {
        Resume();
        int currLevel = SceneManager.GetActiveScene().buildIndex - FirstLevelSceneIndex + 1;

        if (LevelsCompleted < currLevel)
            LevelsCompleted = currLevel;
        LevelCompleted?.Invoke();
    }

    public void RestartLevel()
    {
        Resume();
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        LoadingLevel?.Invoke();
        StartCoroutine(LoadSceneAsync(sceneIndex));
    }

    public void LoadLastAvailableLevel()
    {
        Resume();
        int sceneIndex = Math.Min(LastLevelSceneIndex, FirstLevelSceneIndex + LevelsCompleted);
        LoadingLevel?.Invoke();
        StartCoroutine(LoadSceneAsync(sceneIndex));
    }

    public void LoadLevel(int level)
    {
        Resume();
        int sceneIndex = FirstLevelSceneIndex + (level - 1);
        LoadingLevel?.Invoke();
        StartCoroutine(LoadSceneAsync(sceneIndex));
    }

    public void LoadNextLevel()
    {
        Resume();
        int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (sceneIndex > LastLevelSceneIndex)
            Debug.LogWarning($"Scene {sceneIndex} is out of specified level's range");

        if (sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            LoadingLevel?.Invoke();
            StartCoroutine(LoadSceneAsync(sceneIndex));
        }
        else
            GoMainMenu();
    }

    public void GoMainMenu()
    {
        Resume();
        StartCoroutine(LoadSceneAsync(MainMenuSceneIndex));
    }

    IEnumerator LoadSceneAsync(int index)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);

        while (!asyncLoad.isDone)
            yield return null;
    }
}