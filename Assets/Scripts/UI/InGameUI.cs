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
        #region Fields

        private Player _player;
        private PlayerInput _input;

        [SerializeField] private string _mainMenuSceneName;
        [SerializeField] private string _currentSceneName;

        [Header("Displays")] [SerializeField] private Image HealthDisplayBar;
        [SerializeField] private Text GoldAmount;

        [Header("Stats")] [SerializeField] private Text _attackValue;
        [SerializeField] private Text _intelligenceValue;
        [SerializeField] private Text _rangeValue;
        [SerializeField] private Text _hasteValue;
        [SerializeField] private Text _speedValue;

        [Header("Items")] [SerializeField] private Image Item;

        #endregion

        #region Properties

        [SerializeField] public GameObject IngamePanel;
        [SerializeField] public GameObject PausePanel;
        [SerializeField] public GameObject DeathPanel;

        #endregion

        #region Unity Methods

        // Start is called before the first frame update
        void Awake()
        {
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
            if (!PausePanel.activeSelf)
            {
                PausePanel.SetActive(true);
                IngamePanel.SetActive(false);
                Time.timeScale = 0;
            }
            else if (PausePanel.activeSelf)
            {
                PausePanel.SetActive(false);
                IngamePanel.SetActive(true);
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
            Time.timeScale = 1;
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