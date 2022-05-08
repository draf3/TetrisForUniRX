using UniRx;
using UnityEngine;

namespace TetrisForUniRx.Scripts.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        private readonly ReactiveProperty<int> _totalScore = new IntReactiveProperty(0);

        public IReactiveProperty<int> TotalScore => _totalScore;

        public void AddScore(int score = 10)
        {
            _totalScore.Value += score;
        }
    }
}