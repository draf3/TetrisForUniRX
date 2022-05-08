using TetrisForUniRx.Scripts.Games;
using TetrisForUniRx.Scripts.Managers;
using UniRx;
using UnityEngine;
using Zenject;

namespace TetrisForUniRx.Scripts.Presenter
{
    public class ResultPresenter : MonoBehaviour
    {
        [Inject] private GameStateProvider _gameStateProvider;
        [SerializeField] private ResultManager _resultManager; 

        private void Start()
        {
            _resultManager.SetActivePanel(false);
            
            _gameStateProvider.Current
                .Where(x => x == GameState.Result)
                .Subscribe(_ =>
                {
                    _resultManager.SetActivePanel(true);
                })
                .AddTo(this);
            
            _gameStateProvider.Current
                .Where(x => x != GameState.Result)
                .Subscribe(_ =>
                {
                    _resultManager.SetActivePanel(false);
                })
                .AddTo(this);
        }
    }
}