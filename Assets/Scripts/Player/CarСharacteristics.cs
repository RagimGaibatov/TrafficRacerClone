using Save;

namespace Player{
    public class CarСharacteristics : ISaveable{
        public int power;
        public int maxSpeedInMiles;
        public int minSpeedInMiles;
        public int brakeStrength;
        public float steer;

        private DataContainer _dataContainer;


        public CarСharacteristics(DataContainer dataContainer){
            _dataContainer = dataContainer;
            minSpeedInMiles = _dataContainer.minSpeedInMiles;
        }

        public void ResetMinSpeedToNull(){
            minSpeedInMiles = 0;
        }

        public void LoadData(){
            power = _dataContainer.power;
            maxSpeedInMiles = _dataContainer.maxSpeedInMiles;
            brakeStrength = _dataContainer.brakeStrength;
            steer = _dataContainer.steer;
        }

        public void SaveData(){
            _dataContainer.power = power;
            _dataContainer.maxSpeedInMiles = maxSpeedInMiles;
            _dataContainer.brakeStrength = brakeStrength;
            _dataContainer.steer = steer;
        }
    }
}