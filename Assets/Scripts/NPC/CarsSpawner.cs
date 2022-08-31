using System.Collections.Generic;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;


namespace NPC{
    public class CarsSpawner : MonoBehaviour{
        [SerializeField] private Transform[] spawns;
        [SerializeField] private NPCCar[] carPrefabs;

        private PlayerMovement _playerMovement;
        Queue<NPCCar> cars = new();

        private readonly int _sizeOfListCars = 30;
        private float _timeToSpawnCar;
        private float _time;


        public void Construct(PlayerMovement playerMovement){
            _playerMovement = playerMovement;
        }

        private void Start(){
            InitialSpawnCars();
        }

        private void Update(){
            _time += Time.deltaTime;
            if (_time >= _timeToSpawnCar){
                var randomSpawnIndex = Random.Range(0, spawns.Length);
                var iterations = spawns.Length - randomSpawnIndex;
                for (var i = Random.Range(0, iterations); i < iterations; i++){
                    SpawnCar(spawns[i]);
                }

                _time = 0;
                _timeToSpawnCar = Random.Range(0.6f, 3f);
            }
        }


        private void SpawnCar(Transform carTransform){
            var car = cars.Dequeue();
            car.gameObject.SetActive(true);
            var newTransform = car.transform;
            newTransform.position = carTransform.position;
            newTransform.forward = carTransform.forward;
        }

        private void InitialSpawnCars(){
            for (int i = 0; i < _sizeOfListCars; i++){
                NPCCar car;
                if (i % 3 == 0){
                    car = Instantiate(carPrefabs[2]);
                }
                else if (i % 4 == 0){
                    car = Instantiate(carPrefabs[1]);
                }
                else{
                    car = Instantiate(carPrefabs[0]);
                }

                car.Construct(this, _playerMovement);
                HideCar(car);
            }
        }

        public void HideCar(NPCCar car){
            cars.Enqueue(car);
            car.gameObject.SetActive(false);
        }
    }
}