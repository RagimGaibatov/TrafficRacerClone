using UnityEngine;

namespace Player{
    public class PlayerMovement : MonoBehaviour{
        private int _indexOfRoad;
        private Vector3 _newPosition;
        [SerializeField] private int _defaultIndex = 3;
        [SerializeField] private float _distanceToMove = 5;
        [SerializeField] private int _minIndexOfRoad = 0;
        [SerializeField] private int _maxIndexOfRoad = 3;

        private CarСharacteristics _carСharacteristics;
        private PlayerAnimator _playerAnimator;

        private float _speedInMiles;
        public int Power => _carСharacteristics.power;
        public int MaxSpeed => _carСharacteristics.maxSpeedInMiles;
        public int BrakeStrength => _carСharacteristics.brakeStrength;
        public float Steer => _carСharacteristics.steer;
        public float SpeedInMiles => _speedInMiles;
        public float RelativelySpeed => _speedInMiles / _carСharacteristics.maxSpeedInMiles;

        private float stopSpeed = 0;

        public void Construct(CarСharacteristics carСharacteristics){
            _carСharacteristics = carСharacteristics;
        }

        private void Awake(){
            _playerAnimator = GetComponent<PlayerAnimator>();
            _indexOfRoad = _defaultIndex;
            _newPosition = transform.position;

            int initSpeed = 50;
            _speedInMiles = initSpeed;
        }

        private void Update(){
            SpeedControl();
            HorizontalMovement();
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

        private void Brake(){
            _speedInMiles -= _carСharacteristics.brakeStrength / _speedInMiles * Time.deltaTime;
        }

        private void Acceleration(){
            _speedInMiles += (_carСharacteristics.power - stopSpeed) / _speedInMiles * Time.deltaTime;
        }


        public void AddStopSpeed(){
            float engineBraking = _carСharacteristics.power * 5;
            _speedInMiles *= 0.5f;
            stopSpeed = engineBraking;
        }

        public void TurnOffMovemnt(){
            enabled = false;
        }
    }
}