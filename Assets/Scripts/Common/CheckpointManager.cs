using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    private Checkpoint[] Checkpoints;

    // Start is called before the first frame update
    void Start()
    {
        var activeScene = SceneManager.GetActiveScene();
        Checkpoints = Resources.FindObjectsOfTypeAll<Checkpoint>()
            .Where(c => c.gameObject.scene == activeScene)
            .ToArray();
        
        if(Checkpoints.Length == 0)
            Debug.LogWarning("Can't find any checkpoints on the scene");
    }

    public void LoadLastCheckpoint()
    {
        var checkpoint = Checkpoints.Where(c => c.IsActivated)
            .OrderByDescending(c => c.ActivatedAt).FirstOrDefault();

        if (TryGetComponent(out Rigidbody2D physics))
            physics.velocity = Vector2.zero;

        if (checkpoint != null)
        {
            transform.position = checkpoint.transform.position;
            gameObject.SetActive(true);
        }
    }
}