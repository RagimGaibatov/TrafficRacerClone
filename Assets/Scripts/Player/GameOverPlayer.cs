using System;
using System.Collections;
using NPC;
using UnityEngine;

namespace Player{
    public class GameOverPlayer : MonoBehaviour{
        private CarСharacteristics _carСharacteristics;
        private PlayerAnimator _playerAnimator;
        private PlayerMovement _playerMovement;

        private bool _isGameOver;
        public event Action OnGameOver;

        public void Construct(CarСharacteristics carСharacteristics){
            _carСharacteristics = carСharacteristics;
        }


        private void Awake(){
            _playerAnimator = GetComponent<PlayerAnimator>();
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void OnTriggerEnter(Collider other){
            if (other.GetComponent<NPCCar>()){
                CrashCar();
            }
        }

        private void CrashCar(){
            if (_isGameOver) return;
            if (!(_playerMovement.SpeedInMiles > 15)) return;

            StartCoroutine(GameOver());
        }

        IEnumerator GameOver(){
            GetComponent<Collider>().enabled = false;
            _isGameOver = true;
            _carСharacteristics.ResetMinSpeedToNull();
            _playerMovement.AddStopSpeed();

            while (_playerMovement.SpeedInMiles > 0){
                yield return null;
            }

            _playerAnimator.SwitchOffAnimator();
            _playerMovement.TurnOffMovement();
            OnGameOver?.Invoke();
        }
    }
}