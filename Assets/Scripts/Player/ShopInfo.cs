using Save;

public class ShopInfo : ISaveable{
    public int pricePower;
    public int priceSpeed;
    public int priceBrake;
    public int priceSteer;


    public int addPower;
    public int addBrakeStrength;
    public int addSpeed;
    public float addSteer;
    public float multiplier;

    private DataContainer _dataContainer;

    public ShopInfo(DataContainer dataContainer){
        _dataContainer = dataContainer;
    }

    public void LoadData(){
        pricePower = _dataContainer.pricePower;
        priceSpeed = _dataContainer.priceSpeed;
        priceBrake = _dataContainer.priceBrake;
        priceSteer = _dataContainer.priceSteer;
        addPower = _dataContainer.addPower;
        addBrakeStrength = _dataContainer.addBrakeStrength;
        addSpeed = _dataContainer.addSpeed;
        addSteer = _dataContainer.addSteer;
        multiplier = _dataContainer.multiplier;
    }

    public void SaveData(){
        _dataContainer.pricePower = pricePower;
        _dataContainer.priceSpeed = priceSpeed;
        _dataContainer.priceBrake = priceBrake;
        _dataContainer.priceSteer = priceSteer;
        _dataContainer.addPower = addPower;
        _dataContainer.addBrakeStrength = addBrakeStrength;
        _dataContainer.addSpeed = addSpeed;
        _dataContainer.addSteer = addSteer;
        _dataContainer.multiplier = multiplier;
    }
}