using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;
using Random = UnityEngine.Random;

public class AIBrain : MonoBehaviour
{
    private UnitInteractor _remotePlayerInteractor;

    [Inject]
    private void Constructor([Inject(Id = PlayerId.RemotePlayer)] UnitInteractor remotePlayerInteractor)
    {
        _remotePlayerInteractor = remotePlayerInteractor;
    }
    
    public ActionType GetAction()
    {
        var values = Enum.GetValues(typeof(ActionType));
        return (ActionType)values.GetValue(Random.Range(0, values.Length));
    }
}
