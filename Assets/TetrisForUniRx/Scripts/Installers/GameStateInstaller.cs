using TetrisForUniRx.Scripts.Games;
using UnityEngine;
using Zenject;

public class GameStateInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<GameStateProvider>()
            .To<GameStateProvider>()
            .AsCached();
    }
}