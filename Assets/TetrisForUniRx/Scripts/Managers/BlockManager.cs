using System;
using TetrisForUniRx.Scripts.Blocks;
using TetrisForUniRx.Scripts.Inputs;
using TetrisForUniRx.Scripts.Inputs.InputImpls;
using UnityEngine;
using UniRx;

namespace TetrisForUniRx.Scripts.Managers
{
    [RequireComponent(typeof(KeyInputEventProvider))]
    public class BlockManager : MonoBehaviour
    {
        [SerializeField] private BlockSpawner _blockSpawner;

        private readonly ReactiveProperty<GameObject> _currentBlock = new ReactiveProperty<GameObject>(null);
        private readonly ReactiveProperty<BlockMover> _currentBlockMover = new ReactiveProperty<BlockMover>(null);
        // private readonly ReactiveProperty<Transform> _currentBlockTransform = new ReactiveProperty<Transform>();
        private Transform _currentBlockTransform;
        private readonly ReactiveProperty<Vector3> _currentBlockPosition = new ReactiveProperty<Vector3>();
        private readonly ReactiveProperty<Quaternion> _currentBlockRotation = new ReactiveProperty<Quaternion>();
        
        public IReadOnlyReactiveProperty<GameObject> CurrentBlock => _currentBlock;
        public IReadOnlyReactiveProperty<BlockMover> CurrentBlockMover => _currentBlockMover;
        // public IReadOnlyReactiveProperty<Transform> CurrentBlockTransform => _currentBlockTransform;
        public Transform CurrentBlockTransform => _currentBlockTransform;
        public IReadOnlyReactiveProperty<Vector3> CurrentBlockPosition => _currentBlockPosition;
        public IReadOnlyReactiveProperty<Quaternion> CurrentBlockRotation => _currentBlockRotation;

        private void Start()
        {
            var input = GetComponent<IInputEventProvider>();

            _currentBlock.Value = _blockSpawner.Spawn();
            _currentBlockMover.Value = _currentBlock.Value.GetComponent<BlockMover>();
            _currentBlockTransform = _currentBlock.Value.transform;
            _currentBlockPosition.Value = _currentBlock.Value.transform.position;
            _currentBlockRotation.Value = _currentBlock.Value.transform.rotation;
                    
            _currentBlockMover
                .Subscribe(mover =>
                {
                    mover.IsActive
                        .Where(x => !x)
                        .Subscribe(_ =>
                        {
                            // Debug.LogFormat("Spawn block");
                            _currentBlock.Value = _blockSpawner.Spawn();
                            _currentBlockMover.Value = _currentBlock.Value.GetComponent<BlockMover>();
                            _currentBlockTransform = _currentBlock.Value.transform;
                            _currentBlockPosition.Value = _currentBlock.Value.transform.position;
                            _currentBlockRotation.Value = _currentBlock.Value.transform.rotation;
                        })
                        .AddTo(this);
                })
                .AddTo(this);
            
            // input.OnSpawn
            //     .Where(x => x)
            //     .Subscribe(_ =>
            //     {
            //         Debug.LogFormat("Spawn block");
            //         _currentBlock.Value = _blockSpawner.Spawn();
            //         _currentBlockMover.Value = _currentBlock.Value.GetComponent<BlockMover>();
            //         _currentBlockTransform.Value = _currentBlock.Value.transform;
            //     })
            //     .AddTo(this);
            
            input.OnRotate
                .Where(x => x)
                .Subscribe(_ =>
                {
                    // Debug.LogFormat("Rotate block");
                    _currentBlockMover.Value.RotateClockWise();
                    _currentBlockRotation.Value = _currentBlock.Value.transform.rotation;
                    

                })
                .AddTo(this);
            
            input.OnMoveLeft
                .Where(x => x)
                .Subscribe(_ =>
                {
                    // Debug.LogFormat("MoveLeft block");
                    _currentBlockMover.Value.MoveHorizontal(Vector2.left);
                    _currentBlockPosition.Value = _currentBlock.Value.transform.position;
                })
                .AddTo(this);
            
            input.OnMoveRight
                .Where(x => x)
                .Subscribe(_ =>
                {
                    // Debug.LogFormat("MoveRight block");
                    _currentBlockMover.Value.MoveHorizontal(Vector2.right);
                    _currentBlockPosition.Value = _currentBlock.Value.transform.position;
                })
                .AddTo(this);
            
            input.OnMoveDown
                .Where(x => x)
                .Subscribe(_ =>
                {
                    // Debug.LogFormat("MoveDown block");
                    _currentBlockMover.Value.MoveDown();
                    _currentBlockPosition.Value = _currentBlock.Value.transform.position;
                })
                .AddTo(this);
        }
    }
}