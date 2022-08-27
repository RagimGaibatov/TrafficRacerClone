using UnityEngine;

public class Road : MonoBehaviour{
    [SerializeField] private Material roadMaterial;
    [SerializeField] private Material grassMaterial;

    private PlayerController _playerController;

    public void Constuct(PlayerController playerController){
        _playerController = playerController;
    }

    void LateUpdate(){
        roadMaterial.mainTextureOffset += new Vector2(0, -_playerController.SpeedInMiles / 10 * Time.deltaTime);
        grassMaterial.mainTextureOffset += new Vector2(0, -_playerController.SpeedInMiles / 20 * Time.deltaTime);
    }
}