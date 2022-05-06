using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace TetrisForUniRx.Scripts.Inputs.InputImpls
{
    public class KeyInputEventProvider : MonoBehaviour, IInputEventProvider
    {
        private readonly ReactiveProperty<bool> _onSpawn = new BoolReactiveProperty();
        private readonly ReactiveProperty<bool> _onRotate = new BoolReactiveProperty();
        private readonly ReactiveProperty<bool> _onMoveDown = new BoolReactiveProperty();
        private readonly ReactiveProperty<bool> _onMoveLeft = new BoolReactiveProperty();
        private readonly ReactiveProperty<bool> _onMoveRight = new BoolReactiveProperty();
        
        public IReadOnlyReactiveProperty<bool> OnSpawn => _onSpawn;
        public IReadOnlyReactiveProperty<bool> OnRotate => _onRotate;
        public IReadOnlyReactiveProperty<bool> OnMoveDown => _onMoveDown;
        public IReadOnlyReactiveProperty<bool> OnMoveLeft => _onMoveLeft;
        public IReadOnlyReactiveProperty<bool> OnMoveRight => _onMoveRight;

        private void Start()
        {
            this.UpdateAsObservable()
                .Select(_ => Input.GetKey(KeyCode.Space))
                .DistinctUntilChanged()
                .Subscribe(x =>
                {
                    // Debug.LogFormat("Space: {0}", x);
                    _onSpawn.Value = x;
                });
            
            this.UpdateAsObservable()
                .Select(_ => Input.GetKey(KeyCode.DownArrow))
                .DistinctUntilChanged()
                .Subscribe(x =>
                {
                    // Debug.LogFormat("DownArrow: {0}", x);
                    _onMoveDown.Value = x;
                });
            
            this.UpdateAsObservable()
                .Select(_ => Input.GetKey(KeyCode.UpArrow))
                .DistinctUntilChanged()
                .Subscribe(x =>
                {
                    // Debug.LogFormat("UpArrow: {0}", x);
                    _onRotate.Value = x;
                });
            
            this.UpdateAsObservable()
                .Select(_ => Input.GetKey(KeyCode.LeftArrow))
                .DistinctUntilChanged()
                .Subscribe(x =>
                {
                    // Debug.LogFormat("LeftArrow: {0}", x);
                    _onMoveLeft.Value = x;
                });
            
            this.UpdateAsObservable()
                .Select(_ => Input.GetKey(KeyCode.RightArrow))
                .DistinctUntilChanged()
                .Subscribe(x =>
                {
                    // Debug.LogFormat("RightArrow: {0}", x);
                    _onMoveRight.Value = x;
                });
        }
    }
}