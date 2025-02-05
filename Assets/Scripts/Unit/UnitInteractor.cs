using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class UnitInteractor
{
    public event Action OnDeath;
    public int Health { private set; get; }
    public int Shield { private set; get; }
    public int MaxHealth { private set; get; }

    public List<BaseAction> Actions { private set; get; }

    public UnitInteractor(int maxHealth, List<BaseAction> actions)
    {
        MaxHealth = maxHealth;
        Health = maxHealth;
        Actions = actions;
    }

    public void Reset()
    {
        Shield = 0;
        Health = MaxHealth;
        foreach(BaseAction action in Actions)
        {
            action.Reset();
        }
    }

    public void TakeDamage(int damage)
    {

        if (Shield > 0)
        {
            int absorbedDamage = Math.Min(Shield, damage);
            Shield -= absorbedDamage;
            damage -= absorbedDamage;
        }

        if (damage > 0)
        {
            Health -= damage;
            if (Health <= 0)
                OnDeath?.Invoke();
        }
    }

    public void ActivateShield(int shield)
    {
        Shield = shield;
    }

    public void DeactivateFireball()
    {
        Actions.First(a => a.Type == ActionType.Fireball).Cancel();
    }

    public void Heal(int value)
    {
        Health += value;
        if (Health > MaxHealth)
            Health = MaxHealth;
    }

    public void DeactivateShield()
    {
        Shield = 0;
    }
}
