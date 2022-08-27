using Save;
using UnityEngine;


public class Money : MonoBehaviour, ISaveable{
    private int money;


    public int MoneyCount{
        get => money;
        set => money = value;
    }

    private DataContainer _dataContainer;


    public void Construct(DataContainer dataContainer){
        _dataContainer = dataContainer;
    }

    public void LoadData(){
        money = _dataContainer.money;
    }

    public void SaveData(){
        _dataContainer.money = money;
    }
}