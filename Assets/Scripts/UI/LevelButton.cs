using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public GameManager Manager;
    public NumberDisplay LevelNumberDisplay;
    public NumberDisplay HighScoreDisplay;
    public Button Button;
    public int LevelIndex = 0;
    
    private void Start()
    {
        int level = LevelIndex;
        int scene = Manager.FirstLevelSceneIndex + level - 1;
        
        Button.onClick.AddListener(() => Manager.LoadLevel(level));
        LevelNumberDisplay.SetNumber(level);
        HighScoreDisplay.SetNumber(ScoreManager.LoadHighScoreOf( scene));
    }
}
