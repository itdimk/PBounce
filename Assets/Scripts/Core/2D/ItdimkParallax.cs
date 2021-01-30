using UnityEngine;

public class ItdimkParallax : MonoBehaviour
{
    private Transform _camera;
    private Vector2 _startPos;

    private float _nextBgOffsetX;
    private float _nextBgOffsetY;

    private Transform _nextBackroundX;
    private Transform _nextBackgroundY;
    private Transform _nextBackgroundXY;


    public SpriteRenderer NextBackground;

    public float Amount = 0.7f;
    public float ReplaceCheckInterval = 0.2f;

    void Start()
    {
        _camera = Camera.main.transform;
        _startPos = _camera.position;

        Initialize();
    }

    void Update()
    {
        Vector2 cameraPos = _camera.position;
        Vector2 delta = cameraPos - _startPos;

        transform.position = _startPos + delta * Amount;

        if (ActionEx.CheckCooldown(Update, ReplaceCheckInterval))
        {
            ReplaceIfRequiredX(cameraPos);
            ReplaceIfRequiredY(cameraPos);
            ReplaceIfRequiredXY(_nextBgOffsetX, _nextBgOffsetY);
        }
    }

    void ReplaceIfRequiredX(Vector2 cameraPos)
    {
        float cameraDeltaX = cameraPos.x - _startPos.x;

        if (_nextBgOffsetX > 0 != cameraDeltaX > 0)
            _nextBackroundX.localPosition = new Vector2(_nextBgOffsetX *= -1, 0);

        if (_nextBgOffsetX > 0 == cameraPos.x > _nextBackroundX.position.x)
            _startPos = new Vector2(cameraPos.x, _startPos.y);
    }

    void ReplaceIfRequiredY(Vector2 cameraPos)
    {
        float cameraDeltaY = cameraPos.y - _startPos.y;

        if (_nextBgOffsetY > 0 != cameraDeltaY > 0)
            _nextBackgroundY.localPosition = new Vector2(0, _nextBgOffsetY *= -1);

        if (_nextBgOffsetY > 0 == cameraPos.y > _nextBackgroundY.position.y)
            _startPos = new Vector2(_startPos.x, cameraPos.y);
    }

    void ReplaceIfRequiredXY(float nextBgOffsetH, float nextBgOffsetV)
    {
        _nextBackgroundXY.localPosition = new Vector2(nextBgOffsetH, nextBgOffsetV);
    }

    void Initialize()
    {
        transform.position = _startPos;

        var size = NextBackground.bounds.size;

        _nextBgOffsetX = size.x;
        _nextBgOffsetY = size.y;

        _nextBackroundX = CloneNextBackground(_nextBgOffsetX, 0);
        _nextBackgroundY = CloneNextBackground(0, _nextBgOffsetY);
        _nextBackgroundXY = CloneNextBackground(_nextBgOffsetX, _nextBgOffsetY);

        var mainBackground = NextBackground.transform;
        mainBackground.parent = transform;
        mainBackground.localPosition = Vector3.zero;
    }

    private Transform CloneNextBackground(float localX, float localY)
    {
        var result = Instantiate(NextBackground.gameObject, transform);
        result.transform.localPosition = new Vector2(localX, localY);
        return result.transform;
    }
}