using UnityEngine;
using UnityEngine.Events;

public class SelfDestroyX : MonoBehaviour
{
    public float DestroyOnDelay = 1.0f;

    public GameObject DestroyEffect;

    public bool UseObjectPool = true;
    public UnityEvent OnDestroy;

    private float _startTick;

    // Start is called before the first frame update
    void OnEnable()
    {
        _startTick = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > _startTick + DestroyOnDelay)
            Destruct();
    }

    public void Destruct()
    {
        if (UseObjectPool)
            gameObject.PutToPool();
        else
            Destroy(gameObject);

        OnDestroy?.Invoke();

        if (DestroyEffect != null)
            Instantiate(DestroyEffect, transform.position, Quaternion.identity).SetActive(true);
    }
}