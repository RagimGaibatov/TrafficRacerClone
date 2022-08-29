using Player;
using UnityEngine;

public class OrderBuildings : MonoBehaviour{
    private PlayerMovement _playerMovement;
    private Vector3 defaultPos;

    public void Construct(PlayerMovement playerMovement){
        _playerMovement = playerMovement;
    }

    private void Awake(){
        defaultPos = transform.position;
    }

    private void Update(){
        transform.Translate(_playerMovement.SpeedInMiles * Time.deltaTime * 0.5f, 0, 0);
    }

    private void OnTriggerEnter(Collider other){
        if (other.GetComponent<PlayerMovement>()){
            transform.position = defaultPos;
        }
    }
}