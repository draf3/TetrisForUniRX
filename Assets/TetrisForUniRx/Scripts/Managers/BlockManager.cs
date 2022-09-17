using System;
using TetrisForUniRx.Scripts.Blocks;
using TetrisForUniRx.Scripts.Games;
using TetrisForUniRx.Scripts.Inputs;
using TetrisForUniRx.Scripts.Inputs.InputImpls;
using UnityEngine;
using UniRx;
using Zenject;

namespace TetrisForUniRx.Scripts.Managers
{
    [RequireComponent(typeof(KeyInputEventProvider))]
    public class BlockManager : MonoBehaviour
    {
        [SerializeField] private BlockSpawner _blockSpawner;

        private readonly ReactiveProperty<GameObject> _currentBlock = new ReactiveProperty<GameObject>(null);
        private readonly ReactiveProperty<BlockMover> _currentBlockMover = new ReactiveProperty<BlockMover>(null);
        private readonly Subject<Unit> _onChangedBlockPosition = new Subject<Unit>();
        private readonly Subject<Unit> _onChangedBlockRotation = new Subject<Unit>();

        public IReadOnlyReactiveProperty<GameObject> CurrentBlock => _currentBlock;
        public IReadOnlyReactiveProperty<BlockMover> CurrentBlockMover => _currentBlockMover;
        public IObservable<Unit> OnChangedPosition => _onChangedBlockPosition;
        public IObservable<Unit> OnChangedRotation => _onChangedBlockRotation;

        [Inject] private GameStateProvider _gameStateProvider;

        private void Start()
        {
            var input = GetComponent<IInputEventProvider>();

            _gameStateProvider.Current
                .Where(x => x == GameState.Playing)
                .Subscribe(_ =>
                {
                    _currentBlock.Value = _blockSpawner.Spawn();
                    _currentBlockMover.Value = _currentBlock.Value.GetComponent<BlockMover>();
                    _onChangedBlockPosition.OnNext(Unit.Default);
                    _onChangedBlockRotation.OnNext(Unit.Default);
                });

            _currentBlockMover
                .Where(_ => _gameStateProvider.Current.Value == GameState.Playing)
                .Subscribe(mover =>
                {
                    mover.IsActive
                        .Where(x => !x)
                        .Subscribe(_ =>
                        {
                            _currentBlock.Value = _blockSpawner.Spawn();
                            _currentBlockMover.Value = _currentBlock.Value.GetComponent<BlockMover>();
                            _onChangedBlockPosition.OnNext(Unit.Default);
                            _onChangedBlockRotation.OnNext(Unit.Default);
                        })
                        .AddTo(this);
                })
                .AddTo(this);

            input.OnRotate
                .Where(_ => _gameStateProvider.Current.Value == GameState.Playing)
                .Where(x => x)
                .Subscribe(_ =>
                {
                    _currentBlockMover.Value.RotateClockWise();
                    _onChangedBlockRotation.OnNext(Unit.Default);
                })
                .AddTo(this);
            
            input.OnMoveLeft
                .Where(_ => _gameStateProvider.Current.Value == GameState.Playing)
                .Where(x => x)
                .Subscribe(_ =>
                {
                    _currentBlockMover.Value.MoveHorizontal(Vector2.left);
                    _onChangedBlockPosition.OnNext(Unit.Default);
                })
                .AddTo(this);
            
            input.OnMoveRight
                .Where(_ => _gameStateProvider.Current.Value == GameState.Playing)
                .Where(x => x)
                .Subscribe(_ =>
                {
                    _currentBlockMover.Value.MoveHorizontal(Vector2.right);
                    _onChangedBlockPosition.OnNext(Unit.Default);
                })
                .AddTo(this);
            
            input.OnMoveDown
                .Where(_ => _gameStateProvider.Current.Value == GameState.Playing)
                .Where(x => x)
                .Subscribe(_ =>
                {
                    _currentBlockMover.Value.MoveDown();
                    _onChangedBlockPosition.OnNext(Unit.Default);
                })
                .AddTo(this);
        }
    }
}