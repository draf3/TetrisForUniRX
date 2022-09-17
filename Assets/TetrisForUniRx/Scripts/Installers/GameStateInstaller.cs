using TetrisForUniRx.Scripts.Games;
using UnityEngine;
using Zenject;

namespace TetrisForUniRx.Scripts.Installers
{
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
}