using System;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    private static readonly int Left = Animator.StringToHash("Left");
    private static readonly int Right = Animator.StringToHash("Right");
    private Animator _animator;
    
    private Score _score;
    private int newScore;
    private CarСharacteristics _carСharacteristics;

    public int Power => _carСharacteristics.power;
    public int MaxSpeed => _carСharacteristics.maxSpeedInMiles;
    public int BrakeStrength => _carСharacteristics.brakeStrength;
    public float Steer => _carСharacteristics.steer;

    public event Action OnGameOver;


    public void Construct(Score score, CarСharacteristics carСharacteristics){
        _score = score;
        _carСharacteristics = carСharacteristics;
    }

    [Header("Indexes And DistanceToMove")] private int index;
    private Vector3 newPosition;
    [SerializeField] private int defaultIndex = 3;
    [SerializeField] private float distanceToMove = 5;
    [SerializeField] private int minIndex = 0;
    [SerializeField] private int maxIndex = 3;

    private float speedInMiles;

    public float SpeedInMiles => speedInMiles;
    public float RelativelySpeed => speedInMiles / _carСharacteristics.maxSpeedInMiles;

    private float bonusScore = 1;
    [SerializeField] private float maxTimeToGetBonusScore = 5;
    private float timeFromLastEvasion;

    public bool IsGameOver;


    private void Awake(){
        timeFromLastEvasion = maxTimeToGetBonusScore;
        _animator = GetComponent<Animator>();
        index = defaultIndex;
        newPosition = transform.position;
        speedInMiles = 50;
    }

    private void Update(){
        if (IsGameOver){
            speedInMiles = Mathf.Lerp(speedInMiles, 0, Time.deltaTime);
            return;
        }

        SpeedControl();
        HorizontalMovement();
        AddScoreFromSpeed();
    }

    void AddScoreFromSpeed(){
        if (speedInMiles > 100){
            newScore = (int) (speedInMiles * 0.01f);
            _score.AddScore(newScore, 1);
        }
    }

    void SpeedControl(){
        if (Input.GetKey(KeyCode.DownArrow)){
            Brake();
        }

        else if (speedInMiles < _carСharacteristics.maxSpeedInMiles){
            Acceleration();
        }


        speedInMiles = Mathf.Clamp(speedInMiles, _carСharacteristics.minSpeedInMiles,
            _carСharacteristics.maxSpeedInMiles);
    }

    void HorizontalMovement(){
        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            index--;
            _animator.SetTrigger(Left);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)){
            index++;
            _animator.SetTrigger(Right);
        }


        index = Mathf.Clamp(index, minIndex, maxIndex);
        newPosition = new Vector3(index * distanceToMove, transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * _carСharacteristics.steer);
    }

    void Brake(){
        speedInMiles -= _carСharacteristics.brakeStrength / speedInMiles * Time.deltaTime;
    }

    void Acceleration(){
        speedInMiles += _carСharacteristics.power / speedInMiles * Time.deltaTime;
    }


    private void OnTriggerEnter(Collider other){
        if (other.GetComponent<NPCCar>()){
            CrashCar();
        }
        else if (other.CompareTag("ScoreArea")){
            if (timeFromLastEvasion > Time.time - maxTimeToGetBonusScore){
                bonusScore += 0.1f;
            }
            else{
                bonusScore = 1;
            }

            timeFromLastEvasion = Time.time;

            _score.AddScore((int) speedInMiles, bonusScore);
        }
    }


    void CrashCar(){
        if (!IsGameOver){
            if (speedInMiles > 15){
                _animator.enabled = false;
                GetComponent<Collider>().enabled = false;
                IsGameOver = true;
                OnGameOver?.Invoke();
            }
        }
    }
}