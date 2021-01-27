using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LongPressTrigger : UIBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public float durationThreshold = 1.0f;

    public UnityEvent OnLongPress = new UnityEvent();

    private bool isPointerDown;
    private bool longPressTriggered;
    private float timePressStarted;

    private void Update()
    {
        if (isPointerDown && !longPressTriggered)
            if (Time.time - timePressStarted > durationThreshold)
            {
                longPressTriggered = true;
                OnLongPress.Invoke();
            }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        timePressStarted = Time.time;
        longPressTriggered = false;
        isPointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData) => isPointerDown = false;
    
    public void OnPointerExit(PointerEventData eventData) => isPointerDown = false;
    
}