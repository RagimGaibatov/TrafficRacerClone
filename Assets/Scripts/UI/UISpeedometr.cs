using Player;
using TMPro;
using UnityEngine;

namespace UI{
    public class UISpeedometr : MonoBehaviour{
        public void Construct(PlayerMovement playerMovement){
            _playerMovement = playerMovement;
        }

        [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
        private PlayerMovement _playerMovement;

        [SerializeField] private Color minSpeedColor;
        [SerializeField] private Color maxSpeedColor;


        private int _speedInKilometrs;

        private float _relativelySpeed;
        float H, S, V;

        void Update(){
            _relativelySpeed = _playerMovement.RelativelySpeed;
            _speedInKilometrs = (int) (_playerMovement.SpeedInMiles * 1.6);
            _textMeshProUGUI.text = _speedInKilometrs + " KM/H";
            _textMeshProUGUI.color = Color.Lerp(minSpeedColor, maxSpeedColor, _relativelySpeed);
            Color.RGBToHSV(_textMeshProUGUI.color, out H, out S, out V);
            _textMeshProUGUI.color = Color.HSVToRGB(H, 1, 1);
        }
    }
}