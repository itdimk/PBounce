using UnityEngine;

public class TimeOnOff : OnOff
{
    public float OnInSeconds = -1f;
    public float OffInSeconds = -1f;

    private float startTick;
    private void OnEnable()
    {
        startTick = Time.time;
    }

    private void Update()
    {
        if (OnInSeconds > 0 && Time.time >= startTick + OnInSeconds)
            TurnOn();

        if (OffInSeconds > 0 && Time.time >= startTick + OffInSeconds)
            TurnOff();
    }
}