using TetrisForUniRx.Scripts.Games;
using TetrisForUniRx.Scripts.Managers;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TetrisForUniRx.Scripts.Presenter
{
    public class RankingPresenter : GameStatePresenterBase
    {
        [SerializeField] private Button _topButton;
        
        private void Start()
        {
            SetActivePanel(false);
            
            _gameStateProvider.Current
                .Subscribe(x =>
                {
                    var isRanking = x == GameState.Ranking;
                    SetActivePanel(isRanking);
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