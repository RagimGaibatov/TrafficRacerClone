using Player;
using UnityEngine;

namespace Surroundings{
    public class MoverBuildings : MonoBehaviour{
        private PlayerMovement _playerMovement;
        private Vector3 _defaultPos;


        public void Construct(PlayerMovement playerMovement){
            _playerMovement = playerMovement;
            _defaultPos = transform.position;
        }

        private void Update(){
            transform.Translate(0, 0, -_playerMovement.SpeedInMiles * Time.deltaTime * 0.5f);
        }

        private void OnTriggerEnter(Collider other){
            if (other.GetComponent<PlayerMovement>()){
                transform.position = _defaultPos;
            }
        }
    }
}