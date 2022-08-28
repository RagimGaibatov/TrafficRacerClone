using Save;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootStrapper : MonoBehaviour{
    [SerializeField] Road roadPrefab;
    [SerializeField] private Vector3 roadPosition;
    [SerializeField] PlayerController playerPrefab;
    [SerializeField] private Vector3 playerPosition;
    [SerializeField] CarSoundManager carSoundManagerPrefab;
    [SerializeField] Canvas canvasPrefab;
    [SerializeField] private Score ScorePrefab;
    [SerializeField] private Money moneyPrefab;

    private PlayerController _player;
    private CarSoundManager _carSoundManager;
    private Road _road;

    private Canvas _canvas;
    private UIScore _UIScore;
    UISpeedometr _UISpeedometr;
    private UIGameOver _uiGameOver;

    private Score _score;
    private Money _money;
    private CarСharacteristics _carСharacteristics;
    private ShopInfo _shopInfo;

    private DataContainer _dataContainer;
    private SaveLoadController _saveLoadController;
    private SaveablesObjects _saveablesObjects;

    private void Awake(){
        _saveLoadController = new SaveLoadController();
        _dataContainer = _saveLoadController.Load();

        _carСharacteristics = new CarСharacteristics(_dataContainer);
        _shopInfo = new ShopInfo(_dataContainer);

        CreateScore();
        CreatePlayer();
        CreateSoundManager();
        CreateMoney();
        CreateCanvas();
        CreateRoad();

        _saveablesObjects = new SaveablesObjects(_carСharacteristics, _shopInfo, _money, _score);
        _saveablesObjects.GetDataFromContainer();
    }

    void CreatePlayer(){
        _player = Instantiate(playerPrefab, playerPosition, Quaternion.identity);
        _player.Construct(_score, _carСharacteristics);
    }

    void CreateSoundManager(){
        _carSoundManager = Instantiate(carSoundManagerPrefab);
        _carSoundManager.Construct(_player);
    }

    void CreateCanvas(){
        _canvas = Instantiate(canvasPrefab);
        _UIScore = _canvas.GetComponent<UIScore>();
        _UIScore.Construct(_score);
        _UISpeedometr = _canvas.GetComponent<UISpeedometr>();
        _UISpeedometr.Construct(_player);
        _uiGameOver = _canvas.GetComponent<UIGameOver>();
        _uiGameOver.Construct(_score, _player, this, _money, _shopInfo, _carСharacteristics);
    }

    void CreateMoney(){
        _money = Instantiate(moneyPrefab);
        _money.Construct(_dataContainer);
    }

    void CreateRoad(){
        _road = Instantiate(roadPrefab, roadPosition, Quaternion.identity);
        _road.Constuct(_player);
    }

    void CreateScore(){
        _score = Instantiate(ScorePrefab);
        _score.Construct(_dataContainer);
    }

    public void RestartGame(){
        _saveablesObjects.WriteDataToContainer();
        _saveLoadController.Save(_dataContainer);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame(){
        _saveablesObjects.WriteDataToContainer();
        _saveLoadController.Save(_dataContainer);
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}