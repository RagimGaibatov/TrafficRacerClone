using UnityEngine;

public class CarHeadlights : MonoBehaviour{
    [SerializeField] private Light rearLamp1;
    [SerializeField] private Light rearLamp2;
    [SerializeField] private float intensityMultiplier;
    private float defaultIntensity;


    void Start(){
        defaultIntensity = rearLamp1.intensity;
    }

    void Update(){
        if (Input.GetKey(KeyCode.DownArrow)){
            BrakeLight();
        }

        if (Input.GetKeyUp(KeyCode.DownArrow)){
            DefaultLight();
        }
    }

    void BrakeLight(){
        rearLamp1.intensity = defaultIntensity * intensityMultiplier;
        rearLamp2.intensity = defaultIntensity * intensityMultiplier;
    }

    void DefaultLight(){
        rearLamp1.intensity = defaultIntensity;
        rearLamp2.intensity = defaultIntensity;
    }
}