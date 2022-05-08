using TetrisForUniRx.Scripts.Games;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace TetrisForUniRx.Scripts.Managers
{
    public class ResultManager : StateManagerBase
    {
        [SerializeField] private Button _replayButton;
        [SerializeField] private Button _topButton;

        private void Start()
        {
            _replayButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _gameStateProvider.Current.Value = GameState.Start;
                })
                .AddTo(this);
            
            _topButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _gameStateProvider.Current.Value = GameState.Title;
                })
                .AddTo(this);
        }
    }
}