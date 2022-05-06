using UnityEngine;

namespace TetrisForUniRx.Scripts.Games
{
    [CreateAssetMenu(fileName = "Config", menuName = "ScriptableObjects/Config", order = 0)]
    public class Config : ScriptableObject
    {
        public int ColumnSize = 10;
        public int RowSize = 20;
    }
}