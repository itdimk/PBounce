using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [Serializable]
    public class ItemScore
    {
        public string ItemID;
        public int Score;

        public override string ToString() => ItemID;
    }

    public Inventory TargetInventory;

    public ItemScore[] ScoreByItem;

    [Tooltip("Maximum level completion time to get time bonus")]
    public float TimeBonusThreshold;

    public int TimeBonusPerSecond;

    private int _inventoryScore;
    private int _timeBonusScore;
    
    public int Score => _inventoryScore + _timeBonusScore;
    
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
        var delta = TimeBonusThreshold - Time.time;

        if (delta > 0)
            _timeBonusScore = (int) (delta * TimeBonusPerSecond);
        else
            _timeBonusScore = 0;
    }
}