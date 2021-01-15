using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerCheckpoints : MonoBehaviour
{
    public Checkpoint[] Checkpoints = { };
    public bool AutoFind = true;

    // Start is called before the first frame update
    void Start()
    {
        if (AutoFind)
        {
            var activeScene = SceneManager.GetActiveScene();
            Checkpoints = Resources.FindObjectsOfTypeAll<Checkpoint>()
                .Where(c => c.gameObject.scene == activeScene)
                .ToArray();
        }
    }

    public void LoadLastCheckpoint()
    {
        var checkpoint = Checkpoints.Where(c => c.IsActivated)
            .OrderByDescending(c => c.ActivatedAt).FirstOrDefault();

        if (checkpoint != null)
        {
            transform.position = checkpoint.transform.position;
            gameObject.SetActive(true);
        }
    }
}