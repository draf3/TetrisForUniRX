using System;
using UnityEngine;
using UniRx;
using TetrisForUniRx.Scripts.Blocks;
using TetrisForUniRx.Scripts.Games;
using Zenject;

namespace TetrisForUniRx.Scripts.Managers
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private int _decreaseMillisecondes = 500;
        [SerializeField] private BlockManager _blockManager;
        [SerializeField] private Transform _blockHolder;
        
        [Inject, SerializeField] private TetrisGrid _grid;
        [Inject] private GameStateProvider _gameStateProvider;
        [Inject] private ScoreManager _scoreManager;
        
        public TetrisGrid.Column[] Columns = new TetrisGrid.Column[10];

        private void Start()
        {
            _grid.Columns = Columns;
            
            Observable
                .Timer(TimeSpan.FromMilliseconds(_decreaseMillisecondes), TimeSpan.FromMilliseconds(_decreaseMillisecondes))
                // .Interval(TimeSpan.FromMilliseconds(_decreaseMillisecondes))
                .Where(_ => _gameStateProvider.Current.Value == GameState.Playing)
                .Where(_ => _blockManager.CurrentBlock.Value != null)
                .Subscribe(_ =>
                {
                    _blockManager.CurrentBlockMover.Value.MoveDown();
                    UpdateGrid(_blockManager.CurrentBlock.Value.transform);
                }).AddTo(this);

            _blockManager.CurrentBlockMover
                .Where(_ => _gameStateProvider.Current.Value == GameState.Playing)
                .Where(x => x != null)
                .Subscribe(x =>
                {
                    if (!x.IsGridPosition())
                    {
                        ClearBoard();
                        _gameStateProvider.Current.Value = GameState.Result;
                    }
                    
                    DeleteRows(0);
                }).AddTo(this);

            _blockManager.OnChangedPosition
                .Where(_ => _gameStateProvider.Current.Value == GameState.Playing)
                .Where(_ => _blockManager.CurrentBlock.Value != null)
                .Subscribe(_ =>
                {
                    UpdateGrid(_blockManager.CurrentBlock.Value.transform);
                }).AddTo(this);
            
            _blockManager.OnChangedRotation
                .Where(_ => _gameStateProvider.Current.Value == GameState.Playing)
                .Where(_ => _blockManager.CurrentBlock.Value != null)
                .Subscribe(_ =>
                {
                    UpdateGrid(_blockManager.CurrentBlock.Value.transform);
                }).AddTo(this);
            
        }
        
        public void DeleteRows(int k)
        {
            for (int y = k; y < _grid.RowLength; ++y)
            {
                if (IsRowFull(y))
                {
                    DeleteRow(y);
                    DecreaseRowsAbove(y + 1);
                    --y;
                    
                    _scoreManager.AddScore();
                }
            }     
        
            foreach (Transform t in _blockHolder)
                if (t.childCount <= 1)
                {
                    Destroy(t.gameObject);
                }
        }
        
        public void DeleteRow(int y)
        {
            for (int x = 0; x < _grid.ColumnLength; ++x)
            {
                Destroy(_grid.Columns[x].Rows[y].gameObject);
                _grid.Columns[x].Rows[y] = null;
            }
        }
        
        public void DecreaseRowsAbove(int y)
        {
            for (int i = y; i < 20; ++i)
                DecreaseRow(i);
        }
        
        public void DecreaseRow(int y)
        {
            for (var x = 0; x < _grid.ColumnLength; x++)
            {
                if (_grid.Columns[x].Rows[y] != null)
                {
                    _grid.Columns[x].Rows[y - 1] = _grid.Columns[x].Rows[y];
                    _grid.Columns[x].Rows[y] = null;

                    _grid.Columns[x].Rows[y - 1].position += Vector3.down;
                }
            }
        }

        public bool IsRowFull(int y)
        {
            for (int x = 0; x < _grid.ColumnLength; ++x)
                if (_grid.Columns[x].Rows[y] == null)
                    return false;
            return true;
        }

        private void UpdateGrid(Transform t)
        {
            for (int y = 0; y < _grid.RowLength; y++)
            {
                for (int x = 0; x < _grid.ColumnLength; x++)
                {
                    if (_grid.Columns[x].Rows[y] == null)
                    {
                        continue;
                    }

                    if (_grid.Columns[x].Rows[y].parent == t)
                    {
                        _grid.Columns[x].Rows[y] = null;
                    }
                }
            }
            
            foreach (Transform child in t)
            {
                if (child.gameObject.tag.Equals("Block"))
                {
                    Vector2 location = new Vector2(
                        Mathf.Round(child.position.x),
                        Mathf.Round(child.position.y)
                    );
                    
                    _grid.Columns[(int)location.x].Rows[(int)location.y] = child;
                }
            }
        }

        private void IsOverflow(Transform t)
        {
            foreach (Transform child in t)
            {
                if (child.gameObject.tag.Equals("Block"))
                {
                    Vector2 location = new Vector2(
                        Mathf.Round(child.position.x),
                        Mathf.Round(child.position.y)
                    );
                    
                    _grid.Columns[(int)location.x].Rows[(int)location.y] = child;
                }
            }
        }
        
        public void ClearBoard()
        {
            for (int y = 0; y < _grid.RowLength; y++)
            {
                for (int x = 0; x < _grid.ColumnLength; x++)
                {
                    if (_grid.Columns[x].Rows[y] != null)
                    {
                        Destroy(_grid.Columns[x].Rows[y].gameObject);
                        _grid.Columns[x].Rows[y] = null;
                    }
                }
            }

            foreach (Transform t in _blockHolder)
                Destroy(t.gameObject);
        }
    }
}