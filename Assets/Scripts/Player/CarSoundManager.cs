using UnityEngine;
using Random = UnityEngine.Random;

namespace Player{
    public class CarSoundManager : MonoBehaviour{
        [SerializeField] AudioSource audioBrake;
        [SerializeField] AudioSource audioMotor;
        [SerializeField] private AudioClip[] brakeSounds;
        [SerializeField] private AudioClip accelerationClip;

        private float _volume;

        PlayerMovement _playerMovement;

        public void Construct(PlayerMovement playerMovement){
            _playerMovement = playerMovement;
        }

        private void Update(){
            _volume = _playerMovement.RelativelySpeed * 0.5f;
            audioMotor.volume = _volume;

            if (!audioMotor.isPlaying){
                MotorSound();
            }
        }

        private void MotorSound(){
            audioMotor.PlayOneShot(accelerationClip);
        }


        public void BrakeSound(){
            audioMotor.volume = _volume / 1.5f;

            if (audioBrake.isPlaying) return;
            audioBrake.volume = _volume;
            audioBrake.PlayOneShot(brakeSounds[Random.Range(0, brakeSounds.Length)]);
        }
    }
}