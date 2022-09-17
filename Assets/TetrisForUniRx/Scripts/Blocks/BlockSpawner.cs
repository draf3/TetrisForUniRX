using UnityEngine;
using Zenject;

namespace TetrisForUniRx.Scripts.Blocks
{
    public class BlockSpawner : MonoBehaviour
    {
        [SerializeField] private TetrisShape _shape;
        [SerializeField] private Transform _blockHolder;
        
        [Inject] DiContainer _container;

        public GameObject Spawn()
        {
            var shapeTypeIdx = Random.Range(0, _shape.ShapeType.Length);
            // var shape = Instantiate(_shape.ShapeType[shapeTypeIdx]);
            var shape = _container.InstantiatePrefab(_shape.ShapeType[shapeTypeIdx]);
            
            shape.transform.SetParent(_blockHolder);

            return shape;
        }
    }
}