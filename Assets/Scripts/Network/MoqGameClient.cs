using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class MoqGameClient : BaseGameClient
{
    public override bool IsHost => true;

    private AIBrain _aIBrain;

    [Inject]
    public MoqGameClient(AIBrain aIBrain)
    {
        _aIBrain = aIBrain;
    }
    
    public override async UniTask ApplyAction(ActionType actionType)
    {
        InvokeRemotePlayerTurn();
        ActionType opponentAction = _aIBrain.GetAction();
        await UniTask.Delay(2000);
        InvokeOpponentAppliedAction(opponentAction);
        InvokeLocalPlayerTurn();
    }

    public override void StartGame()
    {
        InvokeStartedGame();
        InvokeLocalPlayerTurn();
    }

    public override void RestartGame()
    {
        InvokeRestartedGame();
    }

    public override void FinishGame(PlayerId winner)
    {
        InvokeFinishedGame(winner);
    }
}
