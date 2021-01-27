using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [Serializable]
    public class ItemScore
    {
        public string ItemID;
        public int Score;

        public override string ToString() => ItemID;
    }

    public GameManager Manager;
    public Inventory TargetInventory;

    public ItemScore[] ScoreByItem;

    [Tooltip("Maximum level completion time to get time bonus")]
    public float TimeBonusThreshold;

    public int TimeBonusPerSecond;

    private int _inventoryScore;
    private int _timeBonusScore;

    public int Score => _inventoryScore + _timeBonusScore;

    public int HiScore
    {
        get
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            return PlayerPrefs.GetInt($"score-{sceneIndex}");
        }
    }

    private void Start()
    {
        TargetInventory.ItemsChanged.AddListener(RefreshInventoryScore);
    }

    private void Update()
    {
        RefreshTimeBonusScore();
    }


    private void RefreshInventoryScore()
    {
        _inventoryScore = TargetInventory.Items.Sum(i
            => ScoreByItem.FirstOrDefault(o => o.ItemID == i.ID)?.Score * i?.Count ?? 0);
    }

    private void RefreshTimeBonusScore()
    {
        var delta = TimeBonusThreshold - Manager.TimeFromStart;

        if (delta > 0)
            _timeBonusScore = (int) (delta * TimeBonusPerSecond);
        else
            _timeBonusScore = 0;
    }

    public void SaveHiScore()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (Score > HiScore)
        {
            PlayerPrefs.SetInt($"score-{sceneIndex}", Score);
            PlayerPrefs.Save();
        }
    }

    public static int LoadHighScoreOf(int sceneIndex)
    {
        return  PlayerPrefs.GetInt($"score-{sceneIndex}");
    }
}