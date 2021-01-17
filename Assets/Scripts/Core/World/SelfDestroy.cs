using UnityEngine;
using UnityEngine.Events;

public class SelfDestroy : MonoBehaviour
{
    public bool UseObjectPool = false;
    public GameObject DestroyEffect;
    public UnityEvent OnDestroy;

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