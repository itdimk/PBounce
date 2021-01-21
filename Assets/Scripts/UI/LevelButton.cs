using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public GameManagerX Manager;
    public NumberDisplay LevelNumberDisplay;
    public Button Button;
    
    private void Start()
    {
        int level = GetLevelIndex();
        Button.onClick.AddListener(() => Manager.LoadLevel(level));
        LevelNumberDisplay.SetNumber(level);
    }

    private int GetLevelIndex()
    {
        return transform.GetSiblingIndex() + 1;
    }
}
