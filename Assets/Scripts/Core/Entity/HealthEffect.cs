using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthEffect : MonoBehaviour
{
    public enum EffectType
    {
        AddHp,
        AddArmor,
        AddLives,
        DealDamage
    };

    public EffectType Action;
    public float Amount;

    [Space] public bool ApplyOnTrigger = true;
    public bool ApplyOnCollision;

    [Space] public bool ApplyOnEnter = true;
    public bool ApplyOnStay;
    public bool ApplyOnExit;

    [Space] public float PushPower;
    public float Cooldown = 0.2f;

    [Space] public List<string> TargetTags = new List<string> {"Player"};

    private void Start()
    {
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (ApplyOnCollision && ApplyOnEnter)
            ApplyIfRequired(other.gameObject, other.GetContact(0).point);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (ApplyOnCollision && ApplyOnEnter)
            ApplyIfRequired(other.gameObject, other.GetContact(0).point);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (ApplyOnCollision && ApplyOnExit)
            ApplyIfRequired(other.gameObject, other.GetContact(0).point);
    }

    private void OnCollisionExit(Collision other)
    {
        if (ApplyOnCollision && ApplyOnExit)
            ApplyIfRequired(other.gameObject, other.GetContact(0).point);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (ApplyOnCollision && ApplyOnStay)
            ApplyIfRequired(other.gameObject, other.GetContact(0).point);
    }

    private void OnCollisionStay(Collision other)
    {
        if (ApplyOnCollision && ApplyOnStay)
            ApplyIfRequired(other.gameObject, other.GetContact(0).point);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (ApplyOnTrigger && ApplyOnStay)
            ApplyIfRequired(other.gameObject, transform.position);
    }

    private void OnTriggerStay(Collider other)
    {
        if (ApplyOnTrigger && ApplyOnStay)
            ApplyIfRequired(other.gameObject, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ApplyOnTrigger && ApplyOnEnter)
            ApplyIfRequired(other.gameObject, transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ApplyOnTrigger && ApplyOnEnter)
            ApplyIfRequired(other.gameObject, transform.position);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (ApplyOnTrigger && ApplyOnExit)
            ApplyIfRequired(other.gameObject, transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        if (ApplyOnTrigger && ApplyOnExit)
            ApplyIfRequired(other.gameObject, transform.position);
    }

    private void ApplyIfRequired(GameObject target, Vector2 contact)
    {
        if (TargetTags.Contains(target.tag) && ((Action<GameObject, Vector2>) Apply).CheckCooldown(Cooldown))
            Apply(target, contact);
    }

    private void Apply(GameObject target, Vector2 hitPoint)
    {
        var healthCtr = target.GetComponent<HealthController>();

        if (healthCtr == null) return;

        switch (Action)
        {
            case EffectType.AddHp:
                healthCtr.AddHp(Amount);
                break;
            case EffectType.AddArmor:
                healthCtr.AddArmor(Amount);
                break;
            case EffectType.AddLives:
                healthCtr.AddLives(Mathf.RoundToInt(Amount));
                break;
            case EffectType.DealDamage:
                healthCtr.DealDamage(Amount, gameObject);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (hitPoint != default)
            Push(target, hitPoint);
    }

    private void Push(GameObject other, Vector2 contact)
    {
        Vector3 originPos = contact;
        Vector3 target = other.transform.position;

        Vector3 forceVector = new Vector3(target.x - originPos.x, target.y - originPos.y).normalized;

        forceVector.Scale(new Vector3(PushPower, PushPower));

        if (other.TryGetComponent(out Rigidbody2D physics))
            physics.AddForce(forceVector);
    }
}