using System.Collections;
using Player;
using TMPro;
using UnityEngine;
using DG.Tweening;

namespace UI{
    public class UIGameOver : MonoBehaviour{
        private const string BestScoreText = "BestScore : ";
        private const string ScoreText = "Score : ";
        private const string MoneyText = "Money : ";
        private const string PriceText = "Price : ";
        private const string BrakeText = "BrakeStrength : ";
        private const string PowerText = "Power : ";
        private const string SteerText = "Steer : ";
        private const string MaxSpeedText = "MaxSpeed : ";


        [SerializeField] private GameObject GameOverWindow;

        [SerializeField] private TextMeshProUGUI bestScoreTextMPro;
        [SerializeField] private TextMeshProUGUI scoreTextMPro;
        [SerializeField] private TextMeshProUGUI moneyTextMPro;

        [SerializeField] private TextMeshProUGUI currentPowerTextMPro;
        [SerializeField] private TextMeshProUGUI currentMaxSpeedTextMPro;
        [SerializeField] private TextMeshProUGUI currentBrakeTextMPro;
        [SerializeField] private TextMeshProUGUI currentSteerTextMPro;

        [SerializeField] private TextMeshProUGUI currentPricePowerMPro;
        [SerializeField] private TextMeshProUGUI currentPriceBrakeMPro;
        [SerializeField] private TextMeshProUGUI currentPriceMaxSpeedMPro;
        [SerializeField] private TextMeshProUGUI currentPriceSteerMPro;

        private Score _score;
        private BootStrapper _bootStrapper;
        private Money _money;
        private PlayerMovement _playerMovement;
        private ShopInfo _shopInfo;
        private CarСharacteristics _carСharacteristics;


        public void Construct(Score score, PlayerMovement playerMovement, BootStrapper bootStrapper, Money money,
            ShopInfo shopInfo, CarСharacteristics carСharacteristics){
            _bootStrapper = bootStrapper;
            _score = score;
            _playerMovement = playerMovement;
            _playerMovement.OnGameOver += OpenGameOverWindow;
            _money = money;
            _shopInfo = shopInfo;
            _carСharacteristics = carСharacteristics;
        }

        private void OnDestroy(){
            _playerMovement.OnGameOver -= OpenGameOverWindow;
        }

        public void OpenGameOverWindow(){
            GameOverWindow.SetActive(true);
            RefreshUI();
        }


        private void RefreshUI(){
            UpdateMaxSpeedUI();
            UpdateBrakeUI();
            UpdateSteerUI();
            UpdatePowerUI();
            UpdateScoresUI();

            var newMoney = _money.AmountOfMoney + _score.GetScore / 15;
            DOVirtual.Int(_money.AmountOfMoney, newMoney, 4f,
                currentMoney => moneyTextMPro.text = MoneyText + currentMoney);
            _money.AmountOfMoney = newMoney;
        }


        public void BuyPower(){
            if (_money.AmountOfMoney > _shopInfo.pricePower){
                _carСharacteristics.power += _shopInfo.addPower;
                _money.AmountOfMoney -= _shopInfo.pricePower;
                _shopInfo.addPower = (int) (_shopInfo.multiplier * _shopInfo.addPower);
                _shopInfo.pricePower = (int) (_shopInfo.multiplier * _shopInfo.pricePower);
                UpdatePowerUI();
            }
        }

        public void BuyBrakeStrength(){
            if (_money.AmountOfMoney > _shopInfo.priceBrake){
                _carСharacteristics.brakeStrength += _shopInfo.addBrakeStrength;
                _money.AmountOfMoney -= _shopInfo.priceBrake;
                _shopInfo.addBrakeStrength = (int) (_shopInfo.multiplier * _shopInfo.addBrakeStrength);
                _shopInfo.priceBrake = (int) (_shopInfo.multiplier * _shopInfo.priceBrake);
                UpdateBrakeUI();
            }
        }

        public void BuyMaxSpeed(){
            if (_money.AmountOfMoney > _shopInfo.priceSpeed){
                _carСharacteristics.maxSpeedInMiles += _shopInfo.addSpeed;
                _money.AmountOfMoney -= _shopInfo.priceSpeed;
                _shopInfo.addSpeed = (int) (_shopInfo.multiplier * _shopInfo.addSpeed);
                _shopInfo.priceSpeed = (int) (_shopInfo.multiplier * _shopInfo.priceSpeed);
                UpdateMaxSpeedUI();
            }
        }

        public void BuySteer(){
            if (_money.AmountOfMoney > _shopInfo.priceSteer){
                _carСharacteristics.steer += _shopInfo.addSteer;
                _money.AmountOfMoney -= _shopInfo.priceSteer;
                _shopInfo.addSteer = _shopInfo.multiplier * _shopInfo.addSteer;
                _shopInfo.priceSteer = (int) (_shopInfo.multiplier * _shopInfo.priceSteer);
                UpdateSteerUI();
            }
        }


        public void RestartGame(){
            _bootStrapper.RestartGame();
        }

        public void QuitGame(){
            _bootStrapper.QuitGame();
        }


        private void UpdateScoresUI(){
            scoreTextMPro.text = ScoreText + _score.GetScore;
            if (_score.GetScore > _score.BestScore){
                _score.BestScore = _score.GetScore;
            }

            bestScoreTextMPro.text = BestScoreText + _score.BestScore;
        }

        private void UpdateMaxSpeedUI(){
            currentMaxSpeedTextMPro.text = MaxSpeedText + (int) (1.6f * _playerMovement.MaxSpeed);
            currentPriceMaxSpeedMPro.text = PriceText + _shopInfo.priceSpeed;
            moneyTextMPro.text = MoneyText + _money.AmountOfMoney;
        }

        private void UpdatePowerUI(){
            currentPowerTextMPro.text = PowerText + _playerMovement.Power;
            currentPricePowerMPro.text = PriceText + _shopInfo.pricePower;
            moneyTextMPro.text = MoneyText + _money.AmountOfMoney;
        }

        private void UpdateBrakeUI(){
            currentBrakeTextMPro.text = BrakeText + _playerMovement.BrakeStrength;
            currentPriceBrakeMPro.text = PriceText + _shopInfo.priceBrake;
            moneyTextMPro.text = MoneyText + _money.AmountOfMoney;
        }

        private void UpdateSteerUI(){
            currentSteerTextMPro.text = SteerText + $"{_playerMovement.Steer:F2}";
            currentPriceSteerMPro.text = PriceText + _shopInfo.priceSteer;
            moneyTextMPro.text = MoneyText + _money.AmountOfMoney;
        }
    }
}