using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

public abstract class BaseGameClient
{
    public event Action<ActionType> RemoteAppliedAction;
    public event Action LocalPlayerTurn;
    public event Action RemotePlayerTurn;
    public event Action StartedGame;
    public event Action RestartedGame;
    public event Action<PlayerId> FinshedGame;

    public abstract bool IsHost { get; }

    public abstract UniTask ApplyAction(ActionType actionType);


    protected void InvokeOpponentAppliedAction(ActionType actionType)
    {
        RemoteAppliedAction?.Invoke(actionType);
    }

    protected void InvokeLocalPlayerTurn()
    {
        LocalPlayerTurn?.Invoke();
    }

    protected void InvokeRemotePlayerTurn()
    {
        RemotePlayerTurn?.Invoke();
    }

    protected void InvokeRestartedGame()
    {
        RestartedGame?.Invoke();
    }

    protected void InvokeStartedGame()
    {
        StartedGame?.Invoke();
    }

    protected void InvokeFinishedGame(PlayerId winner)
    {
        FinshedGame?.Invoke(winner);
    }


    public abstract void StartGame();
    public abstract void RestartGame();
    public abstract void FinishGame(PlayerId winner);
}
