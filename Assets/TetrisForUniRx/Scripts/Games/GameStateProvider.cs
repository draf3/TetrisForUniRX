using UnityEngine;
using UniRx;

namespace TetrisForUniRx.Scripts.Games
{
    public class GameStateProvider
    {
        public readonly ReactiveProperty<GameState> Current = new ReactiveProperty<GameState>(GameState.None);
    }
}