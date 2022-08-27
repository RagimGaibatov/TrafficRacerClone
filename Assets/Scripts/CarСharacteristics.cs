using Save;

public class CarСharacteristics : ISaveable{
    public int power;
    public int maxSpeedInMiles;
    public int minSpeedInMiles;
    public int brakeStrength;
    public float steer;

    private DataContainer _dataContainer;


    public CarСharacteristics(DataContainer dataContainer){
        _dataContainer = dataContainer;
    }

    public void LoadData(){
        power = _dataContainer.power;
        maxSpeedInMiles = _dataContainer.maxSpeedInMiles;
        minSpeedInMiles = _dataContainer.minSpeedInMiles;
        brakeStrength = _dataContainer.brakeStrength;
        steer = _dataContainer.steer;
    }

    public void SaveData(){
        _dataContainer.power = power;
        _dataContainer.maxSpeedInMiles = maxSpeedInMiles;
        _dataContainer.minSpeedInMiles = minSpeedInMiles;
        _dataContainer.brakeStrength = brakeStrength;
        _dataContainer.steer = steer;
    }
}