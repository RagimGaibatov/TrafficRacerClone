using System.Collections;
using TMPro;
using UnityEngine;

namespace UI{
    public class UIGameOver : MonoBehaviour{
        private PlayerController _playerController;

        [SerializeField] private TextMeshProUGUI bestScoreText;
        [SerializeField] private TextMeshProUGUI scoreText;
        private int UIScoreNumber;

        [SerializeField] private GameObject GameOverWindow;

        [SerializeField] private TextMeshProUGUI moneyText;

        [SerializeField] private TextMeshProUGUI currentPowerText;
        [SerializeField] private TextMeshProUGUI currentMaxSpeedText;
        [SerializeField] private TextMeshProUGUI currentBrakeText;
        [SerializeField] private TextMeshProUGUI currentSteerText;

        [SerializeField] private TextMeshProUGUI currentPricePower;
        [SerializeField] private TextMeshProUGUI currentPriceBrake;
        [SerializeField] private TextMeshProUGUI currentPriceMaxSpeed;
        [SerializeField] private TextMeshProUGUI currentPriceSteer;

        private Score _score;
        private BootsTrapper _bootsTrapper;
        private Money _money;

        private ShopInfo _shopInfo;
        private CarСharacteristics _carСharacteristics;

        public void Construct(Score score, PlayerController playerController, BootsTrapper bootsTrapper, Money money,
            ShopInfo shopInfo, CarСharacteristics carСharacteristics){
            _bootsTrapper = bootsTrapper;
            _score = score;
            _playerController = playerController;
            _playerController.OnGameOver += OpenGameOverWindow;
            _money = money;
            _shopInfo = shopInfo;
            _carСharacteristics = carСharacteristics;
        }

        private void OnDestroy(){
            _playerController.OnGameOver -= OpenGameOverWindow;
        }

        public void OpenGameOverWindow(){
            GameOverWindow.SetActive(true);
            StartCoroutine(RefreshAllUI());
        }

        IEnumerator RefreshAllUI(){
            UpdateUICharacteristicsText();
            moneyText.text = "Money : " + _money.MoneyCount;

            //update UIScoreText
            while (UIScoreNumber <= _score.GetScore){
                yield return null;
                UIScoreNumber += 100;
                scoreText.text = "Score : " + UIScoreNumber;
            }

            UIScoreNumber = _score.GetScore;
            scoreText.text = "Score : " + UIScoreNumber;

            if (_score.GetScore > _score.BestScore){
                _score.BestScore = _score.GetScore;
            }

            bestScoreText.text = "Best Score : " + _score.BestScore;

            //update UIMoneyText
            while (UIScoreNumber >= 0){
                UIScoreNumber -= 100;
                _money.MoneyCount += 10;
                yield return null;
                moneyText.text = $"Money : {_money.MoneyCount}";
            }

            moneyText.text = $"Money : {_money.MoneyCount}";
        }

        void UpdateUICharacteristicsText(){
            moneyText.text = $"Money : {_money.MoneyCount}";
            currentPowerText.text = $"Power : {_playerController.Power}";
            currentBrakeText.text = $"BrakeStrength : {_playerController.BrakeStrength}";
            currentMaxSpeedText.text = $"MaxSpeed : {(int) (1.6f * _playerController.MaxSpeed)}";
            currentSteerText.text = $"Steer : {string.Format("{0:F2}", _playerController.Steer)}";
            currentPricePower.text = $"Price : {_shopInfo.pricePower}";
            currentPriceBrake.text = $"Price : {_shopInfo.priceBrake}";
            currentPriceMaxSpeed.text = $"Price : {_shopInfo.priceSpeed}";
            currentPriceSteer.text = $"Price : {_shopInfo.priceSteer}";
        }

        public void BuyPower(){
            if (_money.MoneyCount > _shopInfo.pricePower){
                _carСharacteristics.power += _shopInfo.addPower;
                _money.MoneyCount -= _shopInfo.pricePower;
                _shopInfo.addPower = (int) (_shopInfo.multiplier * _shopInfo.addPower);
                _shopInfo.pricePower = (int) (_shopInfo.multiplier * _shopInfo.pricePower);
                UpdateUICharacteristicsText();
            }
        }

        public void BuyBrakeStrength(){
            if (_money.MoneyCount > _shopInfo.priceBrake){
                _carСharacteristics.brakeStrength += _shopInfo.addBrakeStrength;
                _money.MoneyCount -= _shopInfo.priceBrake;
                _shopInfo.addBrakeStrength = (int) (_shopInfo.multiplier * _shopInfo.addBrakeStrength);
                _shopInfo.priceBrake = (int) (_shopInfo.multiplier * _shopInfo.priceBrake);
                UpdateUICharacteristicsText();
            }
        }

        public void BuyMaxSpeed(){
            if (_money.MoneyCount > _shopInfo.priceSpeed){
                _carСharacteristics.maxSpeedInMiles += _shopInfo.addSpeed;
                _money.MoneyCount -= _shopInfo.priceSpeed;
                _shopInfo.addSpeed = (int) (_shopInfo.multiplier * _shopInfo.addSpeed);
                _shopInfo.priceSpeed = (int) (_shopInfo.multiplier * _shopInfo.priceSpeed);
                UpdateUICharacteristicsText();
            }
        }

        public void BuySteer(){
            if (_money.MoneyCount > _shopInfo.priceSteer){
                _carСharacteristics.steer += _shopInfo.addSteer;
                _money.MoneyCount -= _shopInfo.priceSteer;
                _shopInfo.addSteer = _shopInfo.multiplier * _shopInfo.addSteer;
                _shopInfo.priceSteer = (int) (_shopInfo.multiplier * _shopInfo.priceSteer);
                UpdateUICharacteristicsText();
            }
        }


        public void RestartGame(){
            _bootsTrapper.RestartGame();
        }

        public void QuitGame(){
            _bootsTrapper.QuitGame();
        }
    }
}