using Player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI{
    public class BrakeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler{
        private PlayerMovement _playerMovement;
        private CarHeadlights _carHeadlights;
        private CarSoundManager _carSoundManager;


        public void Construct(PlayerMovement playerMovement, CarSoundManager carSoundManager){
            _playerMovement = playerMovement;
            _carHeadlights = _playerMovement.GetComponent<CarHeadlights>();
            _carSoundManager = carSoundManager;
        }


        public void OnPointerDown(PointerEventData eventData){
            _playerMovement.PlayParticleWheel();
            _playerMovement.BrakeOn();
            _carHeadlights.BrakeLight();
            _carSoundManager.BrakeSound();
        }

        public void OnPointerUp(PointerEventData eventData){
            _playerMovement.BrakeOff();
            _carHeadlights.DefaultLight();
        }
    }
}