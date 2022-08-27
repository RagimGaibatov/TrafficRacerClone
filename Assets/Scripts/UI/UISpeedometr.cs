using TMPro;
using UnityEngine;

namespace UI{
    public class UISpeedometr : MonoBehaviour{
        public void Construct(PlayerController playerController){
            _playerController = playerController;
        }

        [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
        private PlayerController _playerController;

        [SerializeField] private Color minSpeedColor;
        [SerializeField] private Color maxSpeedColor;


        private int speedInKilometrs;

        private float relativelySpeed;
        float H, S, V;

        void Update(){
            relativelySpeed = _playerController.RelativelySpeed;
            speedInKilometrs = (int) (_playerController.SpeedInMiles * 1.6);
            _textMeshProUGUI.text = speedInKilometrs + " KM/H";
            _textMeshProUGUI.color = Color.Lerp(minSpeedColor, maxSpeedColor, relativelySpeed);
            Color.RGBToHSV(_textMeshProUGUI.color, out H, out S, out V);
            _textMeshProUGUI.color = Color.HSVToRGB(H, 1, 1);
        }
    }
}