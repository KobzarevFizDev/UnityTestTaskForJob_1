using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;


public class ActionHandler : MonoBehaviour
{
    [SerializeField] private ActionItemUI[] _actionButtons;

    private BaseGameClient _gameClient;

    private UnitInteractor _localPlayerInteractor;
    private UnitInteractor _remotePlayerInteractor;

    private GameState _gameState;


    [Inject]
    private void Constructor(BaseGameClient gameClient,
                            [Inject(Id = PlayerId.LocalPlayer)] UnitInteractor localPlayerInteractor,
                            [Inject(Id = PlayerId.RemotePlayer)] UnitInteractor remotePlayerInteractor,
                            GameState gameState)  
    {
        _gameState = gameState;
        _gameClient = gameClient;
        _gameClient.RemoteAppliedAction += OnRemotePlayerActionApplied;
        _gameClient.LocalPlayerTurn += LocalPlayerTurn;
        _gameClient.RemotePlayerTurn += RemotePlayerTurn;

        _gameClient.StartedGame += UpdateActionsStates;
        _gameClient.RestartedGame += UpdateActionsStates;


        _localPlayerInteractor = localPlayerInteractor;
        _remotePlayerInteractor = remotePlayerInteractor;
    }

    private void Awake()
    {
        foreach(ActionItemUI actionButton in _actionButtons)
            actionButton.Clicked += ActionApply;
    }

    private void LocalPlayerTurn()
    {
        var localPlayerActions = _localPlayerInteractor.Actions;
        foreach (BaseAction action in localPlayerActions)
        {
            UnitInteractor ownerInteractor = _localPlayerInteractor;
            UnitInteractor opponentInteractor = _remotePlayerInteractor;
            action.OnTurn(ownerInteractor, opponentInteractor);
        }

        _gameState.OnLocalPlayerTurn();
    }

    private void RemotePlayerTurn()
    {
        var remotePlayerAction = _remotePlayerInteractor.Actions;
        foreach (BaseAction action in remotePlayerAction)
        {
            UnitInteractor ownerInteractor = _remotePlayerInteractor;
            UnitInteractor opponentInteractor = _localPlayerInteractor;
            action.OnTurn(ownerInteractor, opponentInteractor);
        }

        _gameState.OnRemotePlayerTurn();
    }

    private void OnRemotePlayerActionApplied(ActionType type)
    {
        Debug.Log($"Удаленный игрок использует = {type}");
        BaseAction action = _remotePlayerInteractor.Actions.FirstOrDefault(a => a.Type == type);
        if (action == null)
            throw new System.InvalidOperationException($"Not found action with type = {type} for remote player");

        UnitInteractor ownerInteractor = _remotePlayerInteractor;
        UnitInteractor opponentInteractor = _localPlayerInteractor;
        action.Apply(ownerInteractor, opponentInteractor);
    }

    private void ActionApply(ActionType type)
    {
        Debug.Log($"Локальный игрок использует = {type}");
        BaseAction action = _localPlayerInteractor.Actions.FirstOrDefault(a => a.Type == type);
        if (action == null)
            throw new System.InvalidOperationException($"Not found action with type = {type} for local player");

        UnitInteractor ownerInteractor = _localPlayerInteractor;
        UnitInteractor opponentInteractor = _remotePlayerInteractor;
        action.Apply(ownerInteractor, opponentInteractor);

        UpdateActionsStates();

        _gameClient.ApplyAction(type);
    }

    private void UpdateActionsStates()
    {
        foreach(var action in _localPlayerInteractor.Actions)
        {
            ActionItemUI button = _actionButtons.FirstOrDefault(b => b.Type == action.Type);
            if (button == null)
                throw new System.InvalidOperationException($"Not found button with action type = {action.Type}");

            button.UpdateState(action.LeftToRestore, action.Cooldown);
        }
    }

    private void OnDestroy()
    {
        foreach (ActionItemUI actionButton in _actionButtons)
            actionButton.Clicked -= ActionApply;

        _gameClient.RemoteAppliedAction -= OnRemotePlayerActionApplied;
        _gameClient.LocalPlayerTurn -= LocalPlayerTurn;
        _gameClient.RemotePlayerTurn -= RemotePlayerTurn;

        _gameClient.StartedGame -= UpdateActionsStates;
        _gameClient.RestartedGame -= UpdateActionsStates;
    }
}
