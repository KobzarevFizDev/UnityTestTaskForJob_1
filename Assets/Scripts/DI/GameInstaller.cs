using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private int _healthOfPlayer = 30;

    [SerializeField] private AIBrain _aiBrain;
    [SerializeField] private ActionFactory _actionFactory;
    [SerializeField] private MoqGameClient _moqGameClient;
    [SerializeField] private GameState _gameLoop;
    public override void InstallBindings()
    {
        BindInteractors();
        BindActionFactory();
        BindAIBrain();
        BindGameLoop();
        BindGameClient();
    }

    private void BindInteractors()
    {
        List<BaseAction> actionsOfRemotePlayer = _actionFactory.CreateActions();
        List<BaseAction> actionsOfLocalPlayer = _actionFactory.CreateActions();


        var localInteractor = new UnitInteractor(_healthOfPlayer, actionsOfLocalPlayer);
        var remoteInteractor = new UnitInteractor(_healthOfPlayer, actionsOfRemotePlayer);

        Container
            .Bind<UnitInteractor>()
            .WithId(PlayerId.LocalPlayer)
            .FromInstance(localInteractor);


        Container
            .Bind<UnitInteractor>()
            .WithId(PlayerId.RemotePlayer)
            .FromInstance(remoteInteractor);
    }

    private void BindActionFactory()
    {
        Container
            .Bind<ActionFactory>()
            .FromInstance(_actionFactory)
            .AsSingle()
            .NonLazy();
    }

    private void BindAIBrain()
    {
        Container
            .Bind<AIBrain>()
            .FromInstance(_aiBrain)
            .AsSingle()
            .NonLazy();
    }

    private void BindGameLoop()
    {
        Container
            .Bind<GameState>()
            .FromInstance(_gameLoop)
            .AsSingle()
            .NonLazy();
    }

    private void BindGameClient()
    {
        Container
            .Bind<BaseGameClient>()
            .To<MoqGameClient>()
            .AsSingle()
            .NonLazy();
    }
}
