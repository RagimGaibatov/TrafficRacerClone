using NPC;
using Player;
using UnityEngine;

public class NPCCar : MonoBehaviour{
    [SerializeField] private float speed;
    private float defaultSpeed;

    private PlayerMovement _playerMovement;

    private CarsSpawner _carsSpawner;

    private Transform _transformPlayer;
    RaycastHit hit;

    private void Start(){
        defaultSpeed = speed;

        _playerMovement = FindObjectOfType<PlayerMovement>();
        _transformPlayer = _playerMovement.transform;
        _carsSpawner = FindObjectOfType<CarsSpawner>();
    }

    private void Update(){
        Vector3 forwardVector = transform.forward;
        if (Physics.Raycast(transform.position, forwardVector, out hit, 10)){
            if (hit.collider.GetComponent<NPCCar>()){
                speed /= defaultSpeed / 3;
            }
        }
        else{
            speed = defaultSpeed;
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