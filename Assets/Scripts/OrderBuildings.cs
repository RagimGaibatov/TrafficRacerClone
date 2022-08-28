using System;
using UnityEngine;

public class OrderBuildings : MonoBehaviour{
    private PlayerController _playerController;
    private Vector3 defaultPos;

    public void Construct(PlayerController playerController){
        _playerController = playerController;
    }

    private void Awake(){
        defaultPos = transform.position;
    }

    private void Update(){
        transform.Translate(_playerController.SpeedInMiles * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other){
        if (other.GetComponent<PlayerController>()){
            transform.position = defaultPos;
        }
    }
}