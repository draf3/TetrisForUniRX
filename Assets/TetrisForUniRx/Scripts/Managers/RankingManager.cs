using TetrisForUniRx.Scripts.Games;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace TetrisForUniRx.Scripts.Managers
{
    public class RankingManager : StateManagerBase
    {
        [SerializeField] private Button _topButton;

        private void Start()
        {

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