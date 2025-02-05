using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameLauncher : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    private BaseGameClient _gameClient;

    [Inject]
    private void Constructor(BaseGameClient gameClient)
    {
        _gameClient = gameClient;
        _restartButton.onClick.AddListener(OnRestart);
    }

    private void OnRestart()
    {
        _gameClient.RestartGame();
    }

    private void Awake()
    {
        _gameClient.StartGame();
    }

    private void OnDestroy()
    {
        _restartButton.onClick.RemoveListener(OnRestart);
    }
}
