using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAction : BaseAction
{
    private int _damage;
    private int _passiveDamage;
    public FireballAction(ActionData actionData)  : base(actionData)
    {
        _damage = actionData.Damage;
        _passiveDamage = actionData.PassiveDamage;
    }

    public override void Apply(UnitInteractor ownerActionInteractor, UnitInteractor opponentActionInteractor)
    {
        base.Apply(ownerActionInteractor, opponentActionInteractor);
        opponentActionInteractor.TakeDamage(_damage);
    }

    public override void OnTurn(UnitInteractor ownerActionInteractor, UnitInteractor opponentActionInteractor)
    {
        base.OnTurn(ownerActionInteractor, opponentActionInteractor);
        if (IsExpired)
            return;
        opponentActionInteractor.TakeDamage(_passiveDamage);
    }
}
