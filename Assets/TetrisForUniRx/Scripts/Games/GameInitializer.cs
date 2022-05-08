using System;
using UnityEngine;
using Zenject;

namespace TetrisForUniRx.Scripts.Games
{
    public class GameInitializer : MonoBehaviour
    {
        [Inject] private GameStateProvider _gameStateProvider;

        private void Start()
        {
            _gameStateProvider.Current.Value = GameState.Title;
        }
    }
}