using UnityEngine;
using UniRx;

namespace TetrisForUniRx.Scripts.Inputs
{
    public interface IInputEventProvider
    {
        IReadOnlyReactiveProperty<bool> OnSpawn { get; }
        IReadOnlyReactiveProperty<bool> OnRotate { get; }
        IReadOnlyReactiveProperty<bool> OnMoveDown { get; }
        IReadOnlyReactiveProperty<bool> OnMoveLeft { get; }
        IReadOnlyReactiveProperty<bool> OnMoveRight { get; }
    }
}