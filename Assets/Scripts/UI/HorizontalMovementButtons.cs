using Player;
using UnityEngine;

namespace UI{
    public class HorizontalMovementButtons : MonoBehaviour{
        private PlayerMovement _playerMovement;


        public void Construct(PlayerMovement playerMovement){
            _playerMovement = playerMovement;
        }

        public void MoveRight(){
            _playerMovement.MoveRight();
        }

        public void MoveLeft(){
            _playerMovement.MoveLeft();
        }
    }
}