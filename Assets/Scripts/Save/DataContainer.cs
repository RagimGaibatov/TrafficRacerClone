namespace Save{
    [System.Serializable]
    public class DataContainer{
        public int power = 335;
        public int maxSpeedInMiles = 90;
        public int minSpeedInMiles = 12;
        public int brakeStrength = 1400;
        public float steer = 0.85f;


        public int pricePower = 120;
        public int priceSpeed = 90;
        public int priceBrake = 45;
        public int priceSteer = 60;

        public int addPower = 3;
        public int addBrakeStrength = 90;
        public int addSpeed = 3;
        public float addSteer = 0.055f;

        public int money = 150;
        public int bestScore = 0;

        public float multiplier = 1.25f;
    }
}