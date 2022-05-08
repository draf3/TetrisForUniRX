using TetrisForUniRx.Scripts.Games;
using TetrisForUniRx.Scripts.Managers;
using UniRx;
using UnityEngine;
using Zenject;

namespace TetrisForUniRx.Scripts.Presenter
{
    public class PlayPresenter : MonoBehaviour
    {
        [Inject] private GameStateProvider _gameStateProvider;
        [SerializeField] private PlayManager _playManager; 

        private void Start()
        {
            _playManager.SetActivePanel(false);
            
            _gameStateProvider.Current
                .Where(x => x == GameState.Play)
                .Subscribe(_ =>
                {
                    _playManager.SetActivePanel(true);
                })
                .AddTo(this);
            
            _gameStateProvider.Current
                .Where(x => x != GameState.Play)
                .Subscribe(_ =>
                {
                    _playManager.SetActivePanel(false);
                })
                .AddTo(this);
        }
    }
}