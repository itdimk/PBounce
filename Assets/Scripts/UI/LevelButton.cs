using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public GameManagerX Manager;
    public NumberDisplay LevelNumberDisplay;
    public NumberDisplay HighScoreDisplay;
    public Button Button;
    
    private void Start()
    {
        int level = GetLevelIndex();
        int scene = Manager.FirstLevelSceneIndex + level - 1;
        
        Button.onClick.AddListener(() => Manager.LoadLevel(level));
        LevelNumberDisplay.SetNumber(level);
        HighScoreDisplay.SetNumber(ScoreManager.LoadHighScoreOf( scene));
    }

    private int GetLevelIndex()
    {
        return transform.GetSiblingIndex() + 1;
    }
}
