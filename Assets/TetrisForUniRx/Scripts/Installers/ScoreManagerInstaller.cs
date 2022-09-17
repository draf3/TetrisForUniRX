using UnityEngine;
using Zenject;

namespace TetrisForUniRx.Scripts.Installers
{
    public class ScoreManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<TetrisForUniRx.Scripts.Managers.ScoreManager>()
                .To<TetrisForUniRx.Scripts.Managers.ScoreManager>()
                .AsCached();
        }
    }
}