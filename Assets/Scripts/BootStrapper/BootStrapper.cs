using NPC;
using Player;
using Save;
using Surroundings;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BootStrapper{
    public class BootStrapper : MonoBehaviour{
        [SerializeField] private Vector3 _playerPosition;
        [SerializeField] private Vector3 _roadPosition;
        [SerializeField] Road _roadPrefab;
        [SerializeField] PlayerMovement _playerPrefab;
        [SerializeField] CarSoundManager _carSoundManagerPrefab;
        [SerializeField] Canvas _canvasPrefab;
        [SerializeField] private Score _ScorePrefab;
        [SerializeField] private Money _moneyPrefab;

        private PlayerMovement _player;
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

            CreateGameObjects();

            _saveablesObjects = new SaveablesObjects(_carСharacteristics, _shopInfo, _money, _score);
            _saveablesObjects.GetDataFromContainer();
        }

        private void CreateGameObjects(){
            CreateScore();
            CreatePlayer();
            CreateSoundManager();
            CreateMoney();
            CreateCanvas();
            CreateRoad();
        }

        private void CreatePlayer(){
            _player = Instantiate(_playerPrefab, _playerPosition, Quaternion.identity);
            _player.Construct(_carСharacteristics);
            var _scoreManager = _player.GetComponent<ScoreManager>();
            _scoreManager.Construct(_score);
            var _gameOverPlayer = _player.GetComponent<GameOverPlayer>();
            _gameOverPlayer.Construct(_carСharacteristics);
        }

        private void CreateSoundManager(){
            _carSoundManager = Instantiate(_carSoundManagerPrefab);
            _carSoundManager.Construct(_player);
        }

        private void CreateCanvas(){
            _canvas = Instantiate(_canvasPrefab);
            _UIScore = _canvas.GetComponent<UIScore>();
            _UIScore.Construct(_score);
            _UISpeedometr = _canvas.GetComponent<UISpeedometr>();
            _UISpeedometr.Construct(_player);
            _uiGameOver = _canvas.GetComponent<UIGameOver>();
            _uiGameOver.Construct(_score, _player, this, _money, _shopInfo, _carСharacteristics,
                _player.GetComponent<GameOverPlayer>());
            var horizontalMovementButtons = _uiGameOver.GetComponent<HorizontalMovementButtons>();
            horizontalMovementButtons.Construct(_player);
            var brakeButton = _uiGameOver.GetComponentInChildren<BrakeButton>();
            brakeButton.Construct(_player, _carSoundManager);
        }

        private void CreateMoney(){
            _money = Instantiate(_moneyPrefab);
            _money.Construct(_dataContainer);
        }

        private void CreateRoad(){
            _road = Instantiate(_roadPrefab, _roadPosition, Quaternion.identity);
            _road.Construct(_player);
            _road.GetComponentInChildren<MoverBuildings>().Construct(_player);
            _road.GetComponentInChildren<CarsSpawner>().Construct(_player);
        }

        private void CreateScore(){
            _score = Instantiate(_ScorePrefab);
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
}