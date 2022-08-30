using Player;
using UnityEngine;

namespace NPC{
    public class NPCCar : MonoBehaviour{
        [SerializeField] private float speed;
        private float _defaultSpeed;

        private PlayerMovement _playerMovement;

        private CarsSpawner _carsSpawner;

        private Transform _transformPlayer;
        RaycastHit _hit;

        private void Start(){
            _defaultSpeed = speed;

            _playerMovement = FindObjectOfType<PlayerMovement>();
            _transformPlayer = _playerMovement.transform;
            _carsSpawner = FindObjectOfType<CarsSpawner>();
        }

        private void Update(){
            Vector3 forwardVector = transform.forward;
            if (Physics.Raycast(transform.position, forwardVector, out _hit, 10)){
                if (_hit.collider.GetComponent<NPCCar>()){
                    speed /= _defaultSpeed / 3;
                }
            }
            else{
                speed = _defaultSpeed;
            }

            float dot = Vector3.Dot(transform.forward, _transformPlayer.forward);
            if (dot > 0){
                transform.Translate((Vector3.forward * (speed - _playerMovement.SpeedInMiles)) * Time.deltaTime);
            }
            else{
                transform.Translate((Vector3.forward * (speed + _playerMovement.SpeedInMiles)) * Time.deltaTime);
            }
        }

        private void OnTriggerEnter(Collider other){
            if (other.GetComponent<RemoverNpc>()){
                _carsSpawner.HideCar(this);
            }
        }
    }
}