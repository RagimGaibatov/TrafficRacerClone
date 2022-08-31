using Player;
using UnityEngine;

namespace NPC{
    public class NPCCar : MonoBehaviour{
        [SerializeField] private float speed;

        private PlayerMovement _playerMovement;
        private CarsSpawner _carsSpawner;
        private Transform _transformPlayer;
        RaycastHit _hit;

        public float Speed => speed;
        private float _defaultSpeed;

        public void Construct(CarsSpawner carsSpawner, PlayerMovement playerMovement){
            _carsSpawner = carsSpawner;
            _playerMovement = playerMovement;
            _defaultSpeed = speed;
        }

        private void Start(){
            _transformPlayer = _playerMovement.transform;
        }

        private void Update(){
            var forwardVector = transform.forward;
            if (Physics.Raycast(transform.position, forwardVector, out _hit, 25)){
                if (_hit.collider.TryGetComponent<NPCCar>(out var frontNpc)){
                    speed = frontNpc.Speed * 0.8f;
                }
            }
            else{
                speed = _defaultSpeed;
            }


            var dot = Vector3.Dot(transform.forward, _transformPlayer.forward);
            if (dot > 0){
                transform.Translate((Vector3.forward * (speed - _playerMovement.SpeedInMiles)) * Time.deltaTime);
            }
            else{
                transform.Translate((Vector3.forward * (speed + _playerMovement.SpeedInMiles)) * Time.deltaTime);
            }
        }

        private void OnTriggerEnter(Collider other){
            if (!other.GetComponent<RemoverNpc>()) return;
            _carsSpawner.HideCar(this);
        }
    }
}