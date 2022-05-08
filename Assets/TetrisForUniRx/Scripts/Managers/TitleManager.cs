using System;
using TetrisForUniRx.Scripts.Games;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;

namespace TetrisForUniRx.Scripts.Managers
{
    public class TitleManager : StateManagerBase
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _rankingButton;

        private void Start()
        {
            _startButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _gameStateProvider.Current.Value = GameState.Start;
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