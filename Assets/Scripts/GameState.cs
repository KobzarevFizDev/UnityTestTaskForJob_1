using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;
using Cysharp.Threading.Tasks;

public enum PlayerId { LocalPlayer, RemotePlayer }

public class GameState : MonoBehaviour
{
    [SerializeField] private GameObject _actionsContainer;
    [SerializeField] private GameObject _restartButton;

    [SerializeField] private UnitCanvas _localPlayerCanvas;
    [SerializeField] private UnitCanvas _remotePlayerCanvas;
    [SerializeField] private Pedestal _localPlayerPedestal;
    [SerializeField] private Pedestal _remotePlayerPedestal;
    [SerializeField] private TextMeshProUGUI _turnOwnerText;

    [SerializeField] private Image _finishGamePanel;
    [SerializeField] private TextMeshProUGUI _finishGameText;

    private const string LOCAL_PLAYER_TURN_TEXT = "Your turn!";
    private const string REMOTE_PLAYER_TURN_TEXT = "Remote player turn!";

    private const string YOUR_LOSE = "YOUR LOSE";
    private const string YOUR_WIN = "YOUR WIN";

    private BaseGameClient _gameClient;
    private UnitInteractor _localPlayerInteractor;
    private UnitInteractor _remotePlayerInteractor;

    [Inject]
    private void Constructor(BaseGameClient gameClient,
                             [Inject(Id = PlayerId.LocalPlayer)] UnitInteractor localPlayerInteractor,
                             [Inject(Id = PlayerId.RemotePlayer)] UnitInteractor remotePlayerInteractor)
    {
        _gameClient = gameClient;

        _localPlayerInteractor = localPlayerInteractor;
        _remotePlayerInteractor = remotePlayerInteractor;

        _localPlayerInteractor.OnDeath += OnLocalPlayerIsDeath;
        _remotePlayerInteractor.OnDeath += OnRemotePlayerIsDeath;

        _gameClient.FinshedGame += OnFinishedGame;
        _gameClient.StartedGame += OnStartedGame;
        _gameClient.RestartedGame += OnRestartedGame;
    }

    private void OnStartedGame()
    {
        _localPlayerInteractor.Reset();
        _remotePlayerInteractor.Reset();

        UpdateUnitInteractors();
    }

    private void OnRestartedGame()
    {
        _localPlayerInteractor.Reset();
        _remotePlayerInteractor.Reset();

        UpdateUnitInteractors();
    }

    private void OnRemotePlayerIsDeath()
    {
        if (_gameClient.IsHost == false)
            return;

        _gameClient.FinishGame(PlayerId.LocalPlayer);
        _gameClient.RestartGame();
    }

    private void OnLocalPlayerIsDeath()
    {
        if (_gameClient.IsHost == false)
            return;

        _gameClient.FinishGame(PlayerId.RemotePlayer);
        _gameClient.RestartGame();
    }

    private void OnFinishedGame(PlayerId winner)
    {
        ShowWinnerAndRestart(winner);
    }

    private async UniTask ShowWinnerAndRestart(PlayerId winner)
    {
        _finishGamePanel.gameObject.SetActive(true);
        if (winner == PlayerId.LocalPlayer)
        {
            _finishGameText.SetText(YOUR_WIN);
            _finishGameText.color = Color.green;
        }
        else
        {
            _finishGameText.SetText(YOUR_LOSE);
            _finishGameText.color = Color.red;
        }

        await UniTask.Delay(1000);
        _gameClient.RestartGame();
        _finishGamePanel.gameObject.SetActive(false);
    }

    public void OnLocalPlayerTurn()
    {
        _actionsContainer.gameObject.SetActive(true);
        _restartButton.gameObject.SetActive(true);

        _remotePlayerPedestal.UnsetOwnerTurn();
        _localPlayerPedestal.SetOwnerTurn();
        _turnOwnerText.SetText(LOCAL_PLAYER_TURN_TEXT);
        _turnOwnerText.color = Color.green;

        UpdateUnitInteractors();
    }

    public void OnRemotePlayerTurn()
    {
        _actionsContainer.gameObject.SetActive(false);
        _restartButton.gameObject.SetActive(false);

        _remotePlayerPedestal.SetOwnerTurn();
        _localPlayerPedestal.UnsetOwnerTurn();
        _turnOwnerText.SetText(REMOTE_PLAYER_TURN_TEXT);
        _turnOwnerText.color = Color.red;

        UpdateUnitInteractors();
    }

    private void UpdateUnitInteractors()
    {
        _localPlayerCanvas.UpdateHealth(_localPlayerInteractor.Health, _localPlayerInteractor.MaxHealth);
        _remotePlayerCanvas.UpdateHealth(_remotePlayerInteractor.Health, _remotePlayerInteractor.MaxHealth);

        foreach (BaseAction action in _localPlayerInteractor.Actions)
            _localPlayerCanvas.UpdateIconStatusForEffect(action);

        foreach (BaseAction action in _remotePlayerInteractor.Actions)
            _remotePlayerCanvas.UpdateIconStatusForEffect(action);
    }

    private void OnDestroy()
    {
        _gameClient.FinshedGame -= OnFinishedGame;


        _localPlayerInteractor.OnDeath -= OnLocalPlayerIsDeath;
        _remotePlayerInteractor.OnDeath -= OnRemotePlayerIsDeath;

        _gameClient.StartedGame -= OnStartedGame;
        _gameClient.RestartedGame -= OnRestartedGame;

    }
}
