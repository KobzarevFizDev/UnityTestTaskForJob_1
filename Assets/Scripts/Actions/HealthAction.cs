using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAction : BaseAction
{
    private int _heal;
    public HealthAction(ActionData actionData) : base(actionData)
    {
        _heal = actionData.Heal;
    }

    public override void Apply(UnitInteractor ownerActionInteractor, UnitInteractor opponentActionInteractor)
    {
        base.Apply(ownerActionInteractor, opponentActionInteractor);
        ownerActionInteractor.Heal(_heal);
    }

    public override void OnTurn(UnitInteractor ownerActionInteractor, UnitInteractor opponentActionInteractor)
    {
        base.OnTurn(ownerActionInteractor, opponentActionInteractor);
        if (IsExpired)
            return;

        ownerActionInteractor.Heal(_heal);
    }
}
