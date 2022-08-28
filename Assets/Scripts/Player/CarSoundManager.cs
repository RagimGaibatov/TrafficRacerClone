using UnityEngine;
using Random = UnityEngine.Random;

public class CarSoundManager : MonoBehaviour{
    [SerializeField] AudioSource audioBrake;
    [SerializeField] AudioSource audioMotor;
    [SerializeField] private AudioClip[] brakeSounds;
    [SerializeField] private AudioClip accelerationClip;

    private float volume;

    PlayerController _playerController;

    public void Construct(PlayerController playerController){
        _playerController = playerController;
    }

    void Update(){
        volume = _playerController.RelativelySpeed * 0.5f;
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