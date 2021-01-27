using UnityEngine;

public class MouseTargetProvider : TargetProviderBase
{
    private Camera _mainCamera;
    private GameObject _marker;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        _marker = new GameObject("MouseTargetSetterMarker");
    }
    
    public override Transform GetTarget()
    {
        _marker.transform.position = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        return _marker.transform;
    }
}