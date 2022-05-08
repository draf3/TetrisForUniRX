using TetrisForUniRx.Scripts.Games;
using TetrisForUniRx.Scripts.Managers;
using UniRx;
using UnityEngine;
using Zenject;

namespace TetrisForUniRx.Scripts.Presenter
{
    public class RankingPresenter : MonoBehaviour
    {
        [Inject] private GameStateProvider _gameStateProvider;
        [SerializeField] private RankingManager _rankingManager; 

        private void Start()
        {
            _rankingManager.SetActivePanel(false);
            
            _gameStateProvider.Current
                .Where(x => x == GameState.Ranking)
                .Subscribe(_ =>
                {
                    _rankingManager.SetActivePanel(true);
                })
                .AddTo(this);
            
            _gameStateProvider.Current
                .Where(x => x != GameState.Ranking)
                .Subscribe(_ =>
                {
                    _rankingManager.SetActivePanel(false);
                })
                .AddTo(this);
        }
    }
}