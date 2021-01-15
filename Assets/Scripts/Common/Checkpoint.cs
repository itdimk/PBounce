using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public float ActivatedAt { get; private set; }
    public bool IsActivated { get; private set; }

    public void Activate()
    {
        IsActivated = true;
        ActivatedAt = Time.time;
    }
}