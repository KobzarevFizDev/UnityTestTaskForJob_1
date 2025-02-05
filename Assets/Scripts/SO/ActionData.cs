using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ActionType { Attack, Shield, Health, Fireball, Cleanup }

[CreateAssetMenu(fileName = "ActionData", menuName = "Data/CreateAction")]
public class ActionData : ScriptableObject
{
    [field: SerializeField] public ActionType Type { private set; get; }
    [field: SerializeField] public int Cooldown { private set; get; }
    [field: SerializeField] public int Duration { private set; get; }
    [field: SerializeField] public int Damage { private set; get; }
    [field: SerializeField] public int PassiveDamage { private set; get; }
    [field: SerializeField] public int Heal { private set; get; }
    [field: SerializeField] public int Shield { private set; get; }
   
}
