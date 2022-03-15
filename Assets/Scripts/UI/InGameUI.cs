using Managers;
using PlayerScripts;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class InGameUI : MonoBehaviour
    {
        #region Declaring Variables

        public static InGameUI Instance;
        private Player _player;
        private PlayerInput _input;

        [SerializeField] private string _mainMenuSceneName;
        [SerializeField] private string _currentSceneName;
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private GameObject _ingamePanel;

        [Header("Displays")]
        [SerializeField] private Image HealthDisplayBar;
        [SerializeField] private Text GoldAmount;

        [Header("Stats")]
        [SerializeField] private Text _attackValue;
        [SerializeField] private Text _intelligenceValue;
        [SerializeField] private Text _rangeValue;
        [SerializeField] private Text _hasteValue;
        [SerializeField] private Text _speedValue;

        [Header("Items")]
        [SerializeField] private Image Item;

        #endregion

        #region Unity Methods

        // Start is called before the first frame update
        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
            
            _input = new PlayerInput();
            SubscribeToEvents();
        }

        private void Start()
        {
            _player = GameManager.Instance.Player;
            _player.UpdateHealthEvent += UpdateHealthbar;
            _player.UpdateGoldEvent += UpdateGold;
            _input.UI.Enable();

            _attackValue.text = "ATT: " + _player.Attack.ToString();
            _intelligenceValue.text = "INT: " + _player.Intelligence.ToString();
            _rangeValue.text = "RA: " + _player.Range.ToString();
            _hasteValue.text = "HA: " + _player.Haste.ToString();
            _speedValue.text = "SPE: " + _player.Speed.ToString();
        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion

        #region UI Methods

        public void PauseUnPause()
        {
            if(!_pauseMenu.activeSelf)
            {
                _pauseMenu.SetActive(true);
                _ingamePanel.SetActive(false);
                Time.timeScale = 0;
            }
            else if(_pauseMenu.activeSelf)
            {
                _pauseMenu.SetActive(false);
                _ingamePanel.SetActive(true);
                Time.timeScale = 1;
            }
        }
        public void Restart()
        {
            AudioListener.pause = false;
            Time.timeScale = 1;
            SceneManager.LoadScene(_currentSceneName);

        }

        public void MainMenu()
        {
            SceneManager.LoadScene(_mainMenuSceneName);
        }

        public void UpdateHealthbar()
        {
            HealthDisplayBar.fillAmount = _player.Health / 100;
        }

        public void UpdateGold()
        {
            GoldAmount.text = _player.Gold.ToString();
        }

        #endregion

        #region Input

        private void PerformPauseIngame(InputAction.CallbackContext context)
        {
            Debug.Log("Pause Input performed");
            PauseUnPause();
        }

        #region Events

        private void SubscribeToEvents()
        {
            _input.UI.PauseIngame.performed += PerformPauseIngame;
        }

        #endregion

        #endregion
    }
}
