using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAction : BaseAction
{
    private int _shield;
    private UnitInteractor _ownerInteractor;
    public ShieldAction(ActionData actionData) : base(actionData)
    {
        _shield = actionData.Shield;
    }

    public override void Apply(UnitInteractor ownerActionInteractor, UnitInteractor opponentActionInteractor)
    {
        base.Apply(ownerActionInteractor, opponentActionInteractor);
        ownerActionInteractor.ActivateShield(_shield);
        _ownerInteractor = ownerActionInteractor;
    }

    public override bool IsExpired
    {
        get
        {
            if (_ownerInteractor == null)
                return true;

            if (LeftToCancel != 0 && _ownerInteractor.Shield > 0)
                return false;
            else
                return true;
        }
    }

}
