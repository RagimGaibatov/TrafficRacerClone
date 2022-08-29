using Save;
using UnityEngine;


public class Money : MonoBehaviour, ISaveable{
    private int _amountOfMoney;


    public int AmountOfMoney{
        get => _amountOfMoney;
        set => _amountOfMoney = value;
    }

    private DataContainer _dataContainer;


    public void Construct(DataContainer dataContainer){
        _dataContainer = dataContainer;
    }

    public void LoadData(){
        _amountOfMoney = _dataContainer.money;
    }

    public void SaveData(){
        _dataContainer.money = _amountOfMoney;
    }
}