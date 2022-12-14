using Player;
using UnityEngine;

namespace Surroundings{
    public class Road : MonoBehaviour{
        [SerializeField] private Material roadMaterial;
        [SerializeField] private Material groundMaterial;


        private PlayerMovement _playerMovement;

        public void Construct(PlayerMovement playerMovement){
            _playerMovement = playerMovement;
        }

        void LateUpdate(){
            roadMaterial.mainTextureOffset += new Vector2(0, -_playerMovement.SpeedInMiles / 10 * Time.deltaTime);
            groundMaterial.mainTextureOffset += new Vector2(0, -_playerMovement.SpeedInMiles / 10 * Time.deltaTime);
        }
    }
}