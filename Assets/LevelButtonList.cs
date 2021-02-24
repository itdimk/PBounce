using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButtonList : MonoBehaviour
{
    public LevelButton Prefab;
    public int[] Levels;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var level in Levels)
        {
            var lvlButton = Prefab.gameObject.GetCloneFromPool(transform);
            lvlButton.GetComponent<LevelButton>().LevelIndex = level;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
