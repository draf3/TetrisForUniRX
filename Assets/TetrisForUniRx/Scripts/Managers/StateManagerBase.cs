using TetrisForUniRx.Scripts.Games;
using UnityEngine;
using Zenject;

namespace TetrisForUniRx.Scripts.Managers
{
    public abstract class StateManagerBase : MonoBehaviour
    {
        [SerializeField] protected GameObject _panelGo;
        [Inject] protected GameStateProvider _gameStateProvider;
    
        public void SetActivePanel(bool isActive)
        {
            _panelGo.SetActive(isActive);
        }
    }
}