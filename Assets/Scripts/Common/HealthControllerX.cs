using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HealthControllerX : MonoBehaviour
{
    public float Hp = 100.0F;
    public float MaxHp = 100.0F;
    public float Armor = 0f;
    public float MaxArmor = 100.0f;
    public float ArmorAbsorption = 0.9f;
    public bool Invincible = false;
    public List<string> IgnoreDamageFrom = new List<string> ();
    
    [Space]
    public NumberDisplay HpOutput;
    public NumberDisplay ArmorOutput;

    public UnityEvent OnDeath;
    public UnityEvent OnHit;
    public UnityEvent OnHeal;
    public UnityEvent OnAddArmor;


    private void Start()
    {
        SetHp(Hp);
        SetArmor(Armor);
    }

    public void DealDamage(float amount, GameObject source)
    {
        if(Invincible || IgnoreDamageFrom.Contains(source.tag)) return;

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

    private void SetArmor(float value)
    {
        Armor = value;

        if (ArmorOutput != null)
            ArmorOutput.SetNumber(value);
    }

    private void SetHp(float value)
    {
        Hp = value;

        if (HpOutput != null)
            HpOutput.SetNumber(value);
    }

    public void Die()
    {
        SetHp(0.0f);
        OnDeath?.Invoke();
    }
}