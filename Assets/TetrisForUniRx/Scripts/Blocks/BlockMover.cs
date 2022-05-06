using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace TetrisForUniRx.Scripts.Blocks
{
    public class BlockMover : MonoBehaviour
    {
        private Transform _rotationPivot;
        
        [Inject] private TetrisGrid _grid;

        private readonly ReactiveProperty<bool> _isActive = new BoolReactiveProperty(true);

        public IReadOnlyReactiveProperty<bool> IsActive => _isActive;
        
        private void Start()
        {
            _rotationPivot = transform.Find("Pivot");
        }

        public void RotateClockWise(bool isClockWise = true)
        {
            
            float rotationDegree = isClockWise ? 90.0f : -90.0f;
            
            transform.RotateAround(_rotationPivot.position, Vector3.forward, rotationDegree);
            
            if (!IsGridPosition())
            {
                transform.RotateAround(_rotationPivot.position, Vector3.forward, -rotationDegree);
            }

        }
        
        public void MoveHorizontal(Vector2 direction)
        {
            float movementDistance = direction.Equals(Vector2.right) ? 1.0f : -1.0f;
            
            transform.position += new Vector3(movementDistance, 0, 0);
            
            if (!IsGridPosition())
            {
                transform.position += new Vector3(-movementDistance, 0, 0);
            }
        }
        
        public void MoveDown()
        {
            transform.position += Vector3.down;

            if (!IsGridPosition())
            {
                transform.position += Vector3.up;
                
                GetComponent<BlockMover>().enabled = false;
                _isActive.Value = false;
            };
        }
        
        private bool IsInsideBorder(Vector2 position)
        {
            return (int)position.x >= 0 &&
                   (int)position.x < _grid.ColumnLength &&
                   (int)position.y >= 0;
        }
        
        public bool IsGridPosition()
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.tag.Equals("Block"))
                {
                    Vector2 position = new Vector2(
                        Mathf.Round(child.position.x),
                        Mathf.Round(child.position.y)
                    );

                    if (!IsInsideBorder(position))
                    {
                        return false;
                    }

                    if (_grid.Columns[(int)position.x].Rows[(int)position.y] != null &&
                        _grid.Columns[(int)position.x].Rows[(int)position.y].parent != transform)
                        return false;
                }
            }
            
            return true;
        }
    }
}