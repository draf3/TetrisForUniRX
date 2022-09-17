using UniRx;
using UnityEngine;

namespace TetrisForUniRx.Scripts.Managers
{
    public class ScoreManager
    {
        private readonly ReactiveProperty<int> _score = new IntReactiveProperty(0);

        public IReactiveProperty<int> Score => _score;

        public void AddScore(int score = 10)
        {
            _score.Value += score;
        }
        
        public void ResetScore()
        {
            _score.Value = 0;
        }
    }
}