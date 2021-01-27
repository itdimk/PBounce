using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CheckpointManager : MonoBehaviour
{
    private Checkpoint[] Checkpoints;

    public UnityEvent CheckpointLoaded;

    void Start()
    {
        Checkpoints = FindObjectsOfType<Checkpoint>();

        if (Checkpoints.Length == 0)
            Debug.LogWarning("Can't find any checkpoints on the scene");
    }

    public void LoadLastCheckpoint()
    {
        var checkpoint = GetLastActivatedCheckpoint();

        if (checkpoint != null)
        {
            if (TryGetComponent(out Rigidbody2D physics))
                physics.velocity = Vector2.zero;

            transform.position = checkpoint.transform.position;
            gameObject.SetActive(true);
            CheckpointLoaded?.Invoke();
        }
        else
            Debug.LogWarning("Can't find any activated checkpoints on the scene");
    }

    private Checkpoint GetLastActivatedCheckpoint() => Checkpoints
        .Where(c => c.IsActivated)
        .OrderByDescending(c => c.ActivatedAt)
        .FirstOrDefault();
}