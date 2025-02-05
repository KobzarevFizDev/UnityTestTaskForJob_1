using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : BaseAction
{
    private int _damage;
    public AttackAction(ActionData actionData) : base(actionData) 
    {
        _damage = actionData.Damage;
    }
    
    public override void Apply(UnitInteractor ownerActionInteractor, UnitInteractor opponentActionInteractor)
    {
        base.Apply(ownerActionInteractor, opponentActionInteractor);
        opponentActionInteractor.TakeDamage(_damage);
    }

}
