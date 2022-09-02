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

        private float _stopSpeed = 0;
        private bool _isBraking;

        [SerializeField] private ParticleSystem _leftParticleSystemWheel;
        [SerializeField] private ParticleSystem _rightParticleSystemWheel;


        public void Construct(CarСharacteristics carСharacteristics){
            _carСharacteristics = carСharacteristics;
        }

        private void Awake(){
            _playerAnimator = GetComponent<PlayerAnimator>();
            _indexOfRoad = _defaultIndex;
            _newPosition = transform.position;

            var initSpeed = 50;
            _speedInMiles = initSpeed;
        }

        private void Update(){
            SpeedControl();
            HorizontalMovement();
        }

        public void PlayParticleWheel(){
            var left = _leftParticleSystemWheel.main;
            var right = _rightParticleSystemWheel.main;
            left.startSize = RelativelySpeed;
            right.startSize = RelativelySpeed;
            _leftParticleSystemWheel.Play();
            _rightParticleSystemWheel.Play();
        }


        private void SpeedControl(){
            if (_isBraking){
                Brake();
            }
            else if (SpeedInMiles < _carСharacteristics.maxSpeedInMiles){
                Acceleration();
            }


            _speedInMiles = Mathf.Clamp(_speedInMiles, _carСharacteristics.minSpeedInMiles,
                _carСharacteristics.maxSpeedInMiles);
        }

        private void HorizontalMovement(){
            _indexOfRoad = Mathf.Clamp(_indexOfRoad, _minIndexOfRoad, _maxIndexOfRoad);
            _newPosition = new Vector3(_indexOfRoad * _distanceToMove, transform.position.y, transform.position.z);

            transform.position =
                Vector3.Lerp(transform.position, _newPosition, Time.deltaTime * _carСharacteristics.steer);
        }

        public void MoveRight(){
            _indexOfRoad++;
            _playerAnimator.MoveRight();
        }

        public void MoveLeft(){
            _indexOfRoad--;
            _playerAnimator.MoveLeft();
        }

        private void Brake(){
            _speedInMiles -= _carСharacteristics.brakeStrength / _speedInMiles * Time.deltaTime;
        }

        private void Acceleration(){
            _speedInMiles += (_carСharacteristics.power - _stopSpeed) / _speedInMiles * Time.deltaTime;
        }

        public void BrakeOn(){
            _isBraking = true;
        }

        public void BrakeOff(){
            _isBraking = false;
        }

        public void AddStopSpeed(){
            float engineBraking = _carСharacteristics.power * 5;
            _speedInMiles *= 0.5f;
            _stopSpeed = engineBraking;
        }

        public void TurnOffMovement(){
            enabled = false;
        }
    }
}