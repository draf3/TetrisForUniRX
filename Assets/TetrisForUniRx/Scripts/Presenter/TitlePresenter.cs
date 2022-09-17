using System;
using TetrisForUniRx.Scripts.Games;
using TetrisForUniRx.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;

namespace TetrisForUniRx.Scripts.Presenter
{
    public class TitlePresenter : GameStatePresenterBase
    {
        [Inject] private TetrisForUniRx.Scripts.Managers.ScoreManager _scoreManager;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _rankingButton;

        private void Start()
        {
            _gameStateProvider.Current
                .Subscribe(x =>
                {
                    var isTitle = x == GameState.Title;
                    SetActivePanel(isTitle);
                    if (isTitle)
                    {
                        _scoreManager.ResetScore();    
                    }
                    
                })
                .AddTo(this);
            
            _startButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _gameStateProvider.Current.Value = GameState.Playing;
                })
                .AddTo(this);
            
            _rankingButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _gameStateProvider.Current.Value = GameState.Ranking;
                })
                .AddTo(this);
        }
    }
}