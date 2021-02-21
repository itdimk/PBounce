using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CashManager : MonoBehaviour
{
    private ScoreManager _scoreManager;

    public float CashByScoreRate = 0.001f;
    
    public int Cash
    {
        get => PlayerPrefs.GetInt("cash");
        private set => PlayerPrefs.SetInt("cash", value);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _scoreManager = FindObjectOfType<ScoreManager>();

        if (!_scoreManager ) 
            Debug.LogError($"Can't find {nameof(ScoreManager)} and {nameof(GameManager)}");
    }

    public  void AddCashByScore()
    {
        Cash += Mathf.CeilToInt(_scoreManager.Score * CashByScoreRate);
        Debug.Log("Cash: " + Cash);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
