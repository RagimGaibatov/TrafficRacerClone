using NPC;
using UnityEngine;

namespace Player{
    public class ScoreManager : MonoBehaviour{
        private PlayerMovement _playerMovement;
        private Score _score;
        private int _newScore;

        [SerializeField] private float _maxTimeToGetBonusScore = 8;
        private float _bonusScore = 1;
        private float _timeFromLastEvasion;

        public void Construct(Score score){
            _score = score;
        }

        private void Awake(){
            _timeFromLastEvasion = _maxTimeToGetBonusScore;
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update(){
            AddScoreFromSpeed();
        }

        private void OnTriggerEnter(Collider other){
            if (other.GetComponent<EvasionScoreArea>()){
                AddEvasionScore();
            }
        }

        private void AddScoreFromSpeed(){
            if (_playerMovement.SpeedInMiles < 100) return;

            _newScore = (int) (_playerMovement.SpeedInMiles * 0.01f);
            _score.AddScore(_newScore, 1);
        }

        private void AddEvasionScore(){
            if (_timeFromLastEvasion > Time.time - _maxTimeToGetBonusScore){
                _bonusScore += 0.1f;
            }
            else{
                _bonusScore = 1;
            }

            _timeFromLastEvasion = Time.time;

            _score.AddScore((int) _playerMovement.SpeedInMiles, _bonusScore);
        }
    }
}