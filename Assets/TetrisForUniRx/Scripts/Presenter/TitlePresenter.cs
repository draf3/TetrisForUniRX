using System;
using TetrisForUniRx.Scripts.Games;
using TetrisForUniRx.Scripts.Managers;
using UnityEngine;
using UniRx;
using Zenject;

namespace TetrisForUniRx.Scripts.Presenter
{
    public class TitlePresenter : MonoBehaviour
    {
        [Inject] private GameStateProvider _gameStateProvider;
        [SerializeField] private TitleManager _titleManager; 

        private void Start()
        {
            _gameStateProvider.Current
                .Where(x => x == GameState.Title)
                .Subscribe(_ =>
                {
                    _titleManager.SetActivePanel(true);
                })
                .AddTo(this);
            
            _gameStateProvider.Current
                .Where(x => x != GameState.Title)
                .Subscribe(_ =>
                {
                    _titleManager.SetActivePanel(false);
                })
                .AddTo(this);
        }
    }
}