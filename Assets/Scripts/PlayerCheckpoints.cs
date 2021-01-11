using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCheckpoints : MonoBehaviour
{
    public Checkpoint[] Checkpoints = { };
    
    // Start is called before the first frame update
    void Start()
    {
    }

    public void LoadLastCheckpoint()
    {
        var checkpoint = Checkpoints.Where(c => c.IsChecked)
            .OrderByDescending(c => c.IsCheckedTick).FirstOrDefault();

        if (checkpoint != null)
        {
            transform.position = checkpoint.transform.position;
            gameObject.SetActive(true);
        }
    }
}