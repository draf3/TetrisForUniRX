using UnityEngine;
using Zenject;
using TetrisForUniRx.Scripts.Blocks;
    
public class TetrisGridInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<TetrisGrid>()
            .To<TetrisGrid>()
            .AsCached();
    }
}