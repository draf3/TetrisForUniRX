using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System; 

namespace TetrisForUniRx.Scripts.Test
{
    public class ObserveEveryValueChangedTest : MonoBehaviour
    {
        private int _currentScore = 0;
        private int _highScore = 0;

        private int _bestHighScore = 0;
        private int _totalScore = 0;

        Subject<int> OnHighScoreChange = new Subject<int>();
        public IObservable<int> OnHighScoreObservable { get { return OnHighScoreChange; } }

        void Start()
        {
            // スコアの変更を登録
            this.ObserveEveryValueChanged(_ => _highScore)
                .Subscribe(highscore =>
                {
                    Debug.Log(highscore);
                    OnHighScoreChange.OnNext(highscore);
                })
                .AddTo(this);
        }

        private void Update()
        {
            _highScore++;
        }
    }
}
