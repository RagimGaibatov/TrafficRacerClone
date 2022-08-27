using System;
using Save;
using UnityEngine;


public class Score : MonoBehaviour, ISaveable{
    private int _score;
    private int _bestScore;

    private DataContainer _dataContainer;


    public void Construct(DataContainer dataContainer){
        _dataContainer = dataContainer;
    }

    public int BestScore{
        get => _bestScore;
        set{
            if (value > 0) _bestScore = value;
            else{
                Debug.LogError("ERROR SCORE. Value is negative");
            }
        }
    }


    public int GetScore => _score;

    public delegate void AddedScore(int score);

    public event Action OnChangedScore;
    public event AddedScore OnAddedScore;


    public void AddScore(int score, float bonus){
        score = (int) (score * bonus);
        OnAddedScore?.Invoke(score);
        _score += score;
        OnChangedScore?.Invoke();
    }


    public void LoadData(){
        _bestScore = _dataContainer.bestScore;
    }

    public void SaveData(){
        _dataContainer.bestScore = _bestScore;
    }
}