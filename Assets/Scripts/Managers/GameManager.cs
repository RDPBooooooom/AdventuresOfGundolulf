using Items;
using Levels;
using PlayerScripts;
using Scrolls;
using UnityEngine;
using Utils;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Fields

        [SerializeField] private LevelManager _levelManagerPrefab;
        [SerializeField] private EnemyManager _enemyManagerPrefab;
        [SerializeField] private UIManager _uIManagerPrefab;
        [SerializeField] private ItemManager _itemManagerPrefab;
        [SerializeField] private AudioManager _audioManagerPrefab;
        [SerializeField] private MonoBehaviourDummy _monoBehaviourDummyPrefab;
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Camera _playerCamPrefab;

        #endregion

        #region Properties

        public static GameManager Instance { get; private set; }

        public LevelManager LevelManager { get; private set; }

        public EnemyManager EnemyManager { get; private set; }

        public UIManager UIManager { get; private set; }

        public ItemManager ItemManager { get; private set; }

        public DeckManager DeckManager { get; private set; }

        public AudioManager AudioManager { get; private set; }

        public Player Player { get; private set; }

        public MonoBehaviourDummy MonoBehaviourDummy { get; private set; }

        BorderPlacement BorderPlacement;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("Instantiated a second GameManager. This is not allowed!");
                Destroy(this);
            }

            Instance = this;

            Vector3 roomPosition = Vector3.zero;
            
            if(_levelManagerPrefab != null)
            {
                LevelManager = Instantiate(_levelManagerPrefab, transform);
                LevelManager.GenerateLevel();    
                roomPosition = LevelManager.CurrentRoom.transform.position;
                
                LevelManager.PlayerCam = Instantiate(_playerCamPrefab);
                LevelManager.PlayerCam.transform.position += roomPosition;
            } else {
                Instantiate(_playerCamPrefab).transform.position = roomPosition + Vector3.up * 10;
            }

            Player = Instantiate(_playerPrefab, roomPosition, Quaternion.identity);
        }

        private void Start()
        {
            EnemyManager = Instantiate(_enemyManagerPrefab, transform);

            AudioManager = Instantiate(_audioManagerPrefab, transform);

            UIManager = Instantiate(_uIManagerPrefab, transform);

            ItemManager = Instantiate(_itemManagerPrefab, transform);

            MonoBehaviourDummy = Instantiate(_monoBehaviourDummyPrefab, transform);

            BorderPlacement = new BorderPlacement();

            BorderPlacement.Initialize();

            if (LevelManager != null)
            {
                DeckManager = new DeckManager();
                DeckManager.LoadDecks();
                LevelManager.Rooms.ForEach(room => room.EnterRoom += DeckManager.OnRoomEnter); 
            }
        }

        #endregion
    }
}