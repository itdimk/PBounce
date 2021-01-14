﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthControllerX : MonoBehaviour
{
    public float Hp = 100.0F;
    public float MaxHp = 100.0F;
    public float Armor = 0f;
    public float MaxArmor = 100.0f;
    public float ArmorAbsorption = 0.9f;
    public int Lives = 1;
    public int MaxLives = 3;
    public bool Invincible = false;
    public List<string> IgnoreDamageFrom = new List<string>();
    
    public UnityEvent OnHit;
    public UnityEvent OnHeal;
    public UnityEvent OnAddArmor;
    public UnityEvent OnAddLife;
    public UnityEvent OnLifeWasted;
    public UnityEvent OnDeath;

    private void Start()
    {
        SetHp(Hp);
        SetArmor(Armor);
        SetLives(Lives);
    }

    public void DealDamage(float amount, GameObject source)
    {
        if (Invincible || Hp == 0 || IgnoreDamageFrom.Contains(source.tag)) return;

        float absorbed = Math.Min(amount * ArmorAbsorption, Armor * ArmorAbsorption);

        SetArmor(Math.Max(0, Armor - absorbed));
        SetHp(Math.Max(0, Hp - (amount - absorbed)));

        OnHit?.Invoke();

        if (Hp <= 0)
            Die();
    }

    public void Heal(float amount)
    {
        SetHp(Math.Min(MaxHp, Hp + amount));

        OnHeal?.Invoke();
    }

    public void AddArmor(float amount)
    {
        SetArmor(Math.Min(MaxArmor, Armor + amount));

        OnAddArmor?.Invoke();
    }

    public void AddLives(int count)
    {
        SetLives(Math.Min(MaxLives, Lives + count));

        OnAddLife?.Invoke();
    }

    private void SetArmor(float value)
    {
        Armor = value;
    }

    private void SetHp(float value)
    {
        Hp = value;
    }

    private void SetLives(int value)
    {
        Lives = value;
    }

    public void Die()
    {
        SetHp(0.0f);
        SetLives(Math.Max(0, Lives - 1));
        
        OnLifeWasted?.Invoke();

        if (Lives == 0)
            OnDeath?.Invoke();
    }
}