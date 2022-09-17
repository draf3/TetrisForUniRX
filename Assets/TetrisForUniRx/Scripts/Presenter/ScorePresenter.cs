using System;
using TetrisForUniRx.Scripts.Games;
using TetrisForUniRx.Scripts.Managers;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TetrisForUniRx.Scripts.Presenter
{
    public class ScorePresenter : MonoBehaviour
    {
        [Inject] protected GameStateProvider _gameStateProvider;
        [Inject] private TetrisForUniRx.Scripts.Managers.ScoreManager _scoreManager;
        
        [SerializeField] private Text _score;

        private void Start()
        {
            _scoreManager.Score
                .Subscribe(x =>
                {
                    Debug.Log(x);
                    _score.text = x.ToString();
                }).AddTo(this);
        }
    }
}