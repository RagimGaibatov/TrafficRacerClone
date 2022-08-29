using System;
using System.Collections;
using NPC;
using UnityEngine;

namespace Player{
    public class PlayerMovement : MonoBehaviour{
        public void Construct(Score score, CarСharacteristics carСharacteristics){
            _score = score;
            _carСharacteristics = carСharacteristics;
        }

        private CarСharacteristics _carСharacteristics;
        private PlayerAnimator _playerAnimator;

        private Score _score;
        private int _newScore;

        private float _speedInMiles;
        public int Power => _carСharacteristics.power;
        public int MaxSpeed => _carСharacteristics.maxSpeedInMiles;
        public int BrakeStrength => _carСharacteristics.brakeStrength;
        public float Steer => _carСharacteristics.steer;

        public float SpeedInMiles => _speedInMiles;
        public float RelativelySpeed => _speedInMiles / _carСharacteristics.maxSpeedInMiles;


        private int _indexOfRoad;
        private Vector3 _newPosition;
        [SerializeField] private int _defaultIndex = 3;
        [SerializeField] private float _distanceToMove = 5;
        [SerializeField] private int _minIndexOfRoad = 0;
        [SerializeField] private int _maxIndexOfRoad = 3;


        private float _bonusScore = 1;
        [SerializeField] private float _maxTimeToGetBonusScore = 8;
        private float _timeFromLastEvasion;

        public bool IsGameOver;
        public event Action OnGameOver;
        private float stopSpeed = 0;


        private void Awake(){
            _timeFromLastEvasion = _maxTimeToGetBonusScore;
            _playerAnimator = GetComponent<PlayerAnimator>();
            _indexOfRoad = _defaultIndex;
            _newPosition = transform.position;

            int initSpeed = 50;
            _speedInMiles = initSpeed;
        }

        private void Update(){
            SpeedControl();
            HorizontalMovement();
            AddScoreFromSpeed();
        }


        private void SpeedControl(){
            if (Input.GetKey(KeyCode.DownArrow)){
                Brake();
            }

            else if (_speedInMiles < _carСharacteristics.maxSpeedInMiles){
                Acceleration();
            }

            _speedInMiles = Mathf.Clamp(_speedInMiles, _carСharacteristics.minSpeedInMiles,
                _carСharacteristics.maxSpeedInMiles);
        }

        private void HorizontalMovement(){
            if (Input.GetKeyDown(KeyCode.LeftArrow)){
                _indexOfRoad--;

                _playerAnimator.MoveLeft();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)){
                _indexOfRoad++;
                _playerAnimator.MoveRight();
            }


            _indexOfRoad = Mathf.Clamp(_indexOfRoad, _minIndexOfRoad, _maxIndexOfRoad);
            _newPosition = new Vector3(_indexOfRoad * _distanceToMove, transform.position.y, transform.position.z);

            transform.position =
                Vector3.Lerp(transform.position, _newPosition, Time.deltaTime * _carСharacteristics.steer);
        }

        private void AddScoreFromSpeed(){
            if (_speedInMiles < 100) return;

            _newScore = (int) (_speedInMiles * 0.01f);
            _score.AddScore(_newScore, 1);
        }

        private void Brake(){
            _speedInMiles -= _carСharacteristics.brakeStrength / _speedInMiles * Time.deltaTime;
        }

        private void Acceleration(){
            _speedInMiles += (_carСharacteristics.power - stopSpeed) / _speedInMiles * Time.deltaTime;
        }


        private void OnTriggerEnter(Collider other){
            if (other.GetComponent<NPCCar>()){
                CrashCar();
            }
            else if (other.GetComponent<EvasionScoreArea>()){
                AddEvasionScore();
            }
        }

        private void AddEvasionScore(){
            if (_timeFromLastEvasion > Time.time - _maxTimeToGetBonusScore){
                _bonusScore += 0.1f;
            }
            else{
                _bonusScore = 1;
            }

            _timeFromLastEvasion = Time.time;

            _score.AddScore((int) _speedInMiles, _bonusScore);
        }


        private void CrashCar(){
            if (IsGameOver) return;
            if (!(_speedInMiles > 15)) return;

            StartCoroutine(GameOver());
        }

        IEnumerator GameOver(){
            GetComponent<Collider>().enabled = false;
            IsGameOver = true;
            _playerAnimator.SwitchOffAnimator();
            _carСharacteristics.ResetMinSpeedToNull();

            _speedInMiles *= 0.5f;
            float engineBraking = _carСharacteristics.power * 5;
            stopSpeed = engineBraking;

            while (_speedInMiles > 0){
                yield return null;
            }

            enabled = false;
            OnGameOver?.Invoke();
        }
    }
}