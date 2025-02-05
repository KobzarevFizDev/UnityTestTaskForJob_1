using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction
{

    public int LeftToCancel { private set; get; }
    public int LeftToRestore { private set; get; }
    public int Cooldown { private set; get; }
    public ActionType Type { private set; get; }

    private int _duration;

    public BaseAction(ActionData actionData)
    {
        Cooldown = actionData.Cooldown;
        _duration = actionData.Duration;

        Type = actionData.Type;
    }

    public bool IsAvailable => LeftToRestore == 0;

    public virtual void Apply(UnitInteractor ownerActionInteractor, UnitInteractor opponentActionInteractor)
    {
        LeftToRestore = Cooldown;
        LeftToCancel = _duration;
    }

    public virtual void OnTurn(UnitInteractor ownerActionInteractor, UnitInteractor opponentActionInteractor) 
    {
        LeftToCancel -= 1;
        LeftToRestore -= 1;

        if (LeftToCancel < 0)
            LeftToCancel = 0;

        if (LeftToRestore < 0)
            LeftToRestore = 0;
    }

    public void Reset()
    {
        LeftToCancel = 0;
        LeftToRestore = 0;
    }

    public virtual bool IsExpired => LeftToCancel == 0; 

    public void Cancel()
    {
        LeftToCancel = 0;
    }
}
