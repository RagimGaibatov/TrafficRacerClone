using UnityEngine;
using Random = UnityEngine.Random;

namespace Player{
    public class CarSoundManager : MonoBehaviour{
        [SerializeField] AudioSource audioBrake;
        [SerializeField] AudioSource audioMotor;
        [SerializeField] private AudioClip[] brakeSounds;
        [SerializeField] private AudioClip accelerationClip;

        private float volume;

        PlayerMovement _playerMovement;

        public void Construct(PlayerMovement playerMovement){
            _playerMovement = playerMovement;
        }

        void Update(){
            volume = _playerMovement.RelativelySpeed * 0.5f;
            audioMotor.volume = volume;
            if (Input.GetKey(KeyCode.DownArrow)){
                BrakeSound();
            }

            else if (!audioMotor.isPlaying){
                MotorSound();
            }
        }

        void MotorSound(){
            audioMotor.PlayOneShot(accelerationClip);
        }


        void BrakeSound(){
            audioMotor.volume = volume / 1.5f;

            if (!audioBrake.isPlaying){
                audioBrake.volume = volume;
                audioBrake.PlayOneShot(brakeSounds[Random.Range(0, brakeSounds.Length)]);
            }
        }
    }
}