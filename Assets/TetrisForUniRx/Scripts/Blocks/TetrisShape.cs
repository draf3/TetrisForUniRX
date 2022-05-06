using UnityEngine;

namespace TetrisForUniRx.Scripts.Blocks
{
    [System.Serializable]
    public class TetrisShape
    {
        [SerializeField] private GameObject[] _shapeTypes;

        public GameObject[] ShapeType => _shapeTypes;
    }
}