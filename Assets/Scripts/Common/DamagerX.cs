﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class DamagerX : MonoBehaviour
{
    public float Amount = 1.0F;
    public bool OnePunch = false;

    [Space] public bool DamageOnTrigger = false;
    public bool DamageOnCollision = true;

    [Space] public bool DamageOnStay;
    public bool DamageOnEnter = true;
    public bool DamageOnExit;

    [Space] public float PushPower = 1000.0F;
    [Space] public List<string> Targets = new List<string> {"Player"};

    public float Cooldown = 0.2f;

    private void Start()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (DamageOnCollision && DamageOnEnter)
            DealDamageIfRequired(other.gameObject, other.GetContact(0).point);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (DamageOnCollision && DamageOnExit)
            DealDamageIfRequired(other.gameObject, other.GetContact(0).point);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (DamageOnCollision && DamageOnStay)
            DealDamageIfRequired(other.gameObject, other.GetContact(0).point);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (DamageOnTrigger && DamageOnStay)
            DealDamageIfRequired(other.gameObject, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (DamageOnTrigger && DamageOnEnter)
            DealDamageIfRequired(other.gameObject, transform.position);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (DamageOnTrigger && DamageOnExit)
            DealDamageIfRequired(other.gameObject, transform.position);
    }

    private void DealDamageIfRequired(GameObject target, Vector2 contact)
    {
        if (Targets.Contains(target.tag))
            Extensions.InvokeWithCooldown(DealDamage, (target, contact), Cooldown);
    }

    private void DealDamage((GameObject, Vector2 ) args)
    {
        var healthCtr = args.Item1.GetComponent<HealthControllerX>();

        if (healthCtr == null) return;

        if (OnePunch)
            healthCtr.Die();
        else
        {
            healthCtr.DealDamage(Amount, gameObject);

            if (args.Item2 != default)
                Push(args.Item1, args.Item2);
        }
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