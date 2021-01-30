using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FakeParallax : MonoBehaviour
{
    private Camera _camera;
    private Vector2 _startPos;
    private Vector2 _size;
    
    public GameObject NextBackgroundX;
    
    public float Amount = 0.7f;

    private bool _nextOnRight;
    
    void Start()
    {
        _camera = Camera.main;
        _startPos = _camera.transform.position;
        _size = GetComponent<SpriteRenderer>().bounds.size;

        InitializePosition();

    }

    void Update()
    {
        Vector2 cameraPos = _camera.transform.position;
        Vector2 delta = cameraPos  - _startPos;
        
        transform.position = _startPos + delta * Amount;
        RepositionIfRequired(cameraPos);
    }

    void RepositionIfRequired(Vector2 cameraPos)
    {
        var nextTransform = NextBackgroundX.transform;
        
        if (!_nextOnRight && cameraPos.x > _startPos.x)
        {
            nextTransform.localPosition = new Vector2(_size.x + _size.x / 2, 0); 
            _nextOnRight = true;
        }
        
        if (_nextOnRight && cameraPos.x < _startPos.x)
        {
            nextTransform.localPosition = new Vector2(-_size.x - _size.x / 2, 0); 
            _nextOnRight = false;
        }
        
        if (_nextOnRight && cameraPos.x > nextTransform.position.x)
        {
            _startPos = cameraPos;
            transform.position = _startPos;
        }
        
        if (!_nextOnRight && cameraPos.x < nextTransform.position.x)
        {
            _startPos = cameraPos;
            transform.position = _startPos;
        }
    }

    void InitializePosition()
    {
        transform.position = _startPos;

        if (NextBackgroundX)
        {
            NextBackgroundX.transform.parent = transform;
            NextBackgroundX.transform.localPosition = new Vector2(_size.x + _size.x / 2, 0);
            _nextOnRight = true;
        }
    }
}