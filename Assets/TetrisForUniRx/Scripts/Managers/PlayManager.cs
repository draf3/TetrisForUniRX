using TetrisForUniRx.Scripts.Games;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace TetrisForUniRx.Scripts.Managers
{
    public class PlayManager : StateManagerBase
    {
        [SerializeField] private Text _score;
        [SerializeField] private ScoreManager _scoreManager;
        
        private void Start()
        {
            _scoreManager.TotalScore
                .Where(_ => _gameStateProvider.Current.Value == GameState.Play)
                .Subscribe(x =>
                {
                    _score.text = x.ToString();
                })
                .AddTo(this);
        }
    }
}