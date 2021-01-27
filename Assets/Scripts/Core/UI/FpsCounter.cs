using UnityEngine;
using UnityEngine.Events;

public class FpsCounter : MonoBehaviour
{
    private long _totalUpdates = 0;
    private double _totalFps = 0;

    [HideInInspector] public float Fps;
    [HideInInspector] public float AverageFps;

    public float RefreshInterval = 1f;

    public UnityEvent FpsUpdated;


    // Update is called once per frame
    void Update()
    {
        if (ActionEx.CheckCooldown(Update, RefreshInterval))
        {
            _totalFps += Fps;
            _totalUpdates++;

            Fps = 1f / Time.unscaledDeltaTime;
            AverageFps = (float) (_totalFps / _totalUpdates);

            FpsUpdated?.Invoke();
        }
    }
}