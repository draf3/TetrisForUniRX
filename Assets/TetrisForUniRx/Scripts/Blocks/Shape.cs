using UnityEngine;

namespace TetrisForUniRx.Scripts.Blocks
{
    [System.Serializable]
    public class Shape : MonoBehaviour
    {
        [SerializeField] private GameObject[] _shapeTypes;
    }
}