using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class CarsSpawner : MonoBehaviour{
    [SerializeField] private Transform[] spawns;
    [SerializeField] private NPCCar[] carPrefabs;


    Queue<NPCCar> cars = new Queue<NPCCar>();

    private int sizeOfListCars = 30;

    private float timeToSpawn = 10;
    private float time;

    void Start(){
        InitialSpawnCars();
    }

    private void Update(){
        SpawnCars();
    }

    void SpawnCars(){
        time += Time.deltaTime;
        if (time >= timeToSpawn){
            int random = Random.Range(0, spawns.Length);
            SpawnCar(spawns[random]);

            timeToSpawn = random;

            time = 0;
        }
    }


    void SpawnCar(Transform spawnTransform){
        NPCCar car = cars.Dequeue();
        car.gameObject.SetActive(true);
        car.transform.position = spawnTransform.position;
        car.transform.forward = spawnTransform.forward;
    }

    void InitialSpawnCars(){
        for (int i = 0; i < sizeOfListCars; i++){
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

            HideCar(car);
        }
    }

    public void HideCar(NPCCar car){
        cars.Enqueue(car);
        car.gameObject.SetActive(false);
    }
}