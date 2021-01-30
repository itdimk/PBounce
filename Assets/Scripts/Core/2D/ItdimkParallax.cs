using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ItdimkParallax : MonoBehaviour
{
    private Camera _camera;
    private Vector2 _startPos;

    private float _nextBgOffsetH;
    private float _nextBgOffsetV;

    private Transform _nextBgTransformH;
    private Transform _nextBgTransformV;
    private Transform _nextBgTransformHV;

    public SpriteRenderer NextBackgroundH;
    public SpriteRenderer NextBackgroundV;
    public SpriteRenderer NextBackgroundHV;

    public float Amount = 0.7f;

    void Start()
    {
        _camera = Camera.main;
        _startPos = _camera.transform.position;

        Initialize();
    }

    void Update()
    {
        Vector2 cameraPos = _camera.transform.position;
        Vector2 delta = cameraPos - _startPos;

        transform.position = _startPos + delta * Amount;
        RepositionIfRequiredH(cameraPos);
        RepositionIfRequiredV(cameraPos);
        RepositionIfRequiredHV(_nextBgOffsetH, _nextBgOffsetV);
    }

    void RepositionIfRequiredH(Vector2 cameraPos)
    {
        if (!NextBackgroundH) return;
        Vector2 cameraDelta = cameraPos - _startPos;

        if (_nextBgOffsetH > 0 != cameraDelta.x > 0)
            _nextBgTransformH.localPosition = new Vector2(_nextBgOffsetH *= -1, 0);

        if (_nextBgOffsetH > 0 == cameraPos.x > _nextBgTransformH.position.x)
            _startPos = new Vector2(cameraPos.x, _startPos.y);
    }

    void RepositionIfRequiredV(Vector2 cameraPos)
    {
        if (!NextBackgroundV) return;
        Vector2 cameraDelta = cameraPos - _startPos;

        if (_nextBgOffsetV > 0 != cameraDelta.y > 0)
            _nextBgTransformV.localPosition = new Vector2(0, _nextBgOffsetV *= -1);

        if (_nextBgOffsetV > 0 == cameraPos.y > _nextBgTransformV.position.y)
            _startPos = new Vector2(_startPos.x, cameraPos.y);
    }

    void RepositionIfRequiredHV(float nextBgOffsetH, float nextBgOffsetV)
    {
        if (!NextBackgroundHV) return;
        _nextBgTransformHV.localPosition = new Vector2(nextBgOffsetH, nextBgOffsetV);
    }

    void Initialize()
    {
        transform.position = _startPos;

        var size = GetComponent<SpriteRenderer>().bounds.size;

        _nextBgOffsetH = size.x + size.x / 2;
        _nextBgOffsetV = size.y + size.y / 2;

        if (NextBackgroundH)
        {
            _nextBgTransformH = NextBackgroundH.transform;
            _nextBgTransformH.parent = transform;
            _nextBgTransformH.localPosition = new Vector2(_nextBgOffsetH, 0);
        }

        if (NextBackgroundV)
        {
            _nextBgTransformV = NextBackgroundV.transform;
            _nextBgTransformV.parent = transform;
            _nextBgTransformV.localPosition = new Vector2(0, _nextBgOffsetV);
        }

        if (NextBackgroundHV)
        {
            _nextBgTransformHV = NextBackgroundHV.transform;
            _nextBgTransformHV.parent = transform;
            _nextBgTransformHV.localPosition = new Vector2(_nextBgOffsetH, _nextBgOffsetV);
        }
    }
}