using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    public float Hp = 100.0F;
    public float MaxHp = 100.0F;
    public float Armor = 0f;
    public float MaxArmor = 100.0f;
    public float ArmorAbsorption = 0.9f;
    public int Lives = 1;
    public int MaxLives = 3;
    public bool Invincible = false;
    public List<string> IgnoreDamageFromTags = new List<string>();

    public UnityEvent HpChanged;
    public UnityEvent ArmorChanged;
    public UnityEvent LivesChanged;
    public UnityEvent LifeWasted;
    public UnityEvent Death;

    private void Start()
    {
        SetHp(Hp);
        SetArmor(Armor);
        SetLives(Lives);
    }

    public void DealDamage(float amount, GameObject source)
    {
        if (Invincible || Hp == 0 || IgnoreDamageFromTags.Contains(source.tag)) return;

        float absorbed = Math.Min(amount * ArmorAbsorption, Armor * ArmorAbsorption);

        SetArmor(Math.Max(0, Armor - absorbed));
        SetHp(Math.Max(0, Hp - (amount - absorbed)));
        
        if (Hp <= 0)
            Die();
    }

    public void AddHp(float amount) => SetHp(Math.Min(MaxHp, Hp + amount));

    public void AddArmor(float amount) => SetArmor(Math.Min(MaxArmor, Armor + amount));

    public void AddLives(int count) => SetLives(Math.Min(MaxLives, Lives + count));

    private void SetArmor(float value)
    {
        Armor = value;
        ArmorChanged?.Invoke();
    }

    private void SetHp(float value)
    {
        Hp = value;
        HpChanged?.Invoke();
    }

    private void SetLives(int value)
    {
        Lives = value;
        LivesChanged?.Invoke();
    }

    public void Die()
    {
        if (Invincible) return;

        SetHp(0.0f);
        SetLives(Math.Max(0, Lives - 1));

        LifeWasted?.Invoke();

        if (Lives == 0)
            Death?.Invoke();
    }
}