using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Serializable]
    public class ItemScore
    {
        public string ItemID;
        public int Score;
    }
    
    public Inventory TargetInventory;

    public ItemScore[] ScoreByItem;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
