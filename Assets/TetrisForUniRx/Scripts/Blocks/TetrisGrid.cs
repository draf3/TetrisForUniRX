using UnityEngine;
using UniRx;

namespace TetrisForUniRx.Scripts.Blocks
{
    [System.Serializable]
    public class TetrisGrid
    {
        private const int COLUMN_SIZE = 10;
        private const int ROW_SIZE = 20;
        public Column[] Columns = new Column[COLUMN_SIZE];

        public int ColumnLength => COLUMN_SIZE;
        public int RowLength => ROW_SIZE;

        [System.Serializable]
        public class Column
        {
            public Transform[] Rows = new Transform[ROW_SIZE];
        }
    }
}
