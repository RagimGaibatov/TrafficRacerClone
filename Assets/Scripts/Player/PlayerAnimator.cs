using UnityEngine;

namespace Player{
    public class PlayerAnimator : MonoBehaviour{
        private static readonly int Left = Animator.StringToHash("Left");
        private static readonly int Right = Animator.StringToHash("Right");
        private Animator _animator;

        private void Awake(){
            _animator = GetComponent<Animator>();
        }

        public void MoveLeft(){
            _animator.SetTrigger(Left);
        }

        public void MoveRight(){
            _animator.SetTrigger(Right);
        }

        public void SwitchOffAnimator(){
            _animator.enabled = false;
        }
    }
}