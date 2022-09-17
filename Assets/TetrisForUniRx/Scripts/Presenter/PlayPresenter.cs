using TetrisForUniRx.Scripts.Games;
using TetrisForUniRx.Scripts.Managers;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TetrisForUniRx.Scripts.Presenter
{
    public class PlayPresenter : GameStatePresenterBase
    {
        [Inject] private TetrisForUniRx.Scripts.Managers.ScoreManager _scoreManager;

        private void Start()
        {
            SetActivePanel(false);
            
            _gameStateProvider.Current
                .Subscribe(x =>
                {
                    var isPlaying = x == GameState.Playing;
                    SetActivePanel(isPlaying);
                    if (isPlaying)
                    {
                        _scoreManager.ResetScore();    
                    }
                })
                .AddTo(this);
        }
    }
}