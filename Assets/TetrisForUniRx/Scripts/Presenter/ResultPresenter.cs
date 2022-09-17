using TetrisForUniRx.Scripts.Games;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TetrisForUniRx.Scripts.Presenter
{
    public class ResultPresenter : GameStatePresenterBase
    {
        [Inject] private TetrisForUniRx.Scripts.Managers.ScoreManager _scoreManager;
        
        [SerializeField] private Button _replayButton;
        [SerializeField] private Button _topButton;
        [SerializeField] private Text _totalScore;

        private void Start()
        {
            SetActivePanel(false);
            
            _gameStateProvider.Current
                .Subscribe(x =>
                {
                    var isResult = x == GameState.Result;
                    SetActivePanel(isResult);
                    if (isResult)
                    {
                        _totalScore.text = _scoreManager.Score.Value.ToString();    
                    }
                })
                .AddTo(this);

            _replayButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _gameStateProvider.Current.Value = GameState.Playing;
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