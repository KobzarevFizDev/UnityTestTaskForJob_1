using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanupAction : BaseAction
{
    public CleanupAction(ActionData actionData) : base(actionData)
    {

    }

    public override void Apply(UnitInteractor ownerActionInteractor, UnitInteractor opponentActionInteractor)
    {
        base.Apply(ownerActionInteractor, opponentActionInteractor);
        opponentActionInteractor.DeactivateFireball();
    }
}
