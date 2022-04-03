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

        [Header("Items")] [SerializeField] private Image ItemDisplay;

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

        #endregion

        #region UI Methods

        public void PauseUnPause()
        {
            if (!GameManager.Instance.UIManager.DisablePausePanel)
            {
                if (!PausePanel.activeSelf)
                {
                    PausePanel.SetActive(true);
                    IngamePanel.SetActive(false);

                    _player.Input.Disable();
                    Time.timeScale = 0;
                }
                else
                {
                    PausePanel.SetActive(false);
                    IngamePanel.SetActive(true);

                    _player.Input.Enable();
                    Time.timeScale = 1;
                }
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

        #region UpdateDisplays
        public void UpdateHealthbar()
        {
            HealthDisplayBar.fillAmount = _player.Health / 100;
        }

        public void UpdateGold()
        {
            GoldAmount.text = _player.Gold.ToString();
        }

        public void UpdateActiveItem(Sprite item)
        {
            ItemDisplay.sprite = item;
        }

        public void UpdateAttackDisplay()
        {
            _attackValue.text = "ATT: " + _player.Attack.ToString();
        }

        public void UpdateIntelligenceDisplay()
        {
            _intelligenceValue.text = "INT: " + _player.Intelligence.ToString();
        }

        public void UpdateRangeDisplay()
        {
            _rangeValue.text = "RA: " + _player.Range.ToString();
        }
        public void UpdateHasteDisplay()
        {
            _hasteValue.text = "HA: " + _player.Haste.ToString();
        }
        public void UpdateSpeedDisplay()
        {
            _speedValue.text = "SPE: " + _player.Speed.ToString();
        }


        #endregion

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