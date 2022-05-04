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

        [SerializeField] private string _mainMenuSceneName;
        [SerializeField] private string _currentSceneName;

        [Header("Displays")] 
        [SerializeField] private Image _healthDisplayBar;
        [SerializeField] private Text _goldAmount;

        [Header("Stats")] 
        [SerializeField] private Text _attackValue;
        [SerializeField] private Text _intelligenceValue;
        [SerializeField] private Text _rangeValue;
        [SerializeField] private Text _hasteValue;
        [SerializeField] private Text _speedValue;

        [Header("Items")] 
        [SerializeField] private Image _itemDisplay;

        private Player _player;
        private PlayerInput _input;
        private SkinnedMeshRenderer _playerMaterial;
        private Material _normalMaterial;
        private Material _invincibleMaterial;

        #endregion

        #region Properties

        [SerializeField] public GameObject IngamePanel;
        [SerializeField] public GameObject PausePanel;
        [SerializeField] public GameObject DeathPanel;
        [SerializeField] public GameObject WinPanel;

        [SerializeField] public Text _textInteractable;
        [SerializeField] public Image ItemBackgroundImage;
        [SerializeField] public Slider audioSlider;

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
            _invincibleMaterial = Resources.Load<Material>("Material/Invincible");
            _playerMaterial = _player.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
            _normalMaterial = _playerMaterial.material;
            
            _player.OnUpdateHealthEvent += UpdateHealthbar;
            _player.OnUpdateAttackEvent += UpdateAttackDisplay;
            _player.OnUpdateIntelligenceEvent += UpdateIntelligenceDisplay;
            _player.OnUpdateRangeEvent += UpdateRangeDisplay;
            _player.OnUpdateHasteEvent += UpdateHasteDisplay;
            _player.OnUpdateSpeedEvent += UpdateSpeedDisplay;
            _player.OnUpdateGoldEvent += UpdateGold;
            _player.OnUpdateActiveItemEvent += UpdateActiveItem;
            _player.OnUpdateInvincibilityEvent += UpdateInvincibilityStatus;
            
            _input.UI.Enable();

            UpdateAttackDisplay();
            UpdateIntelligenceDisplay();
            UpdateRangeDisplay();
            UpdateHasteDisplay();
            UpdateSpeedDisplay();
            UpdateActiveItem();
            audioSlider.onValueChanged.AddListener(GameManager.Instance.AudioManager.SetSoundsValue);
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
            _healthDisplayBar.fillAmount = _player.Health / 100;
        }

        public void UpdateGold()
        {
            _goldAmount.text = _player.Gold.ToString();
        }

        public void UpdateActiveItem()
        {
            _itemDisplay.sprite = _player.ActiveItem?.UIImage;
            if (_itemDisplay.sprite != null)
                _itemDisplay.gameObject.SetActive(true);
            else
                _itemDisplay.gameObject.SetActive(false);
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

        void UpdateInvincibilityStatus()
        {
            if (_player.Invincible)
                _playerMaterial.material = _invincibleMaterial;
            else
                _playerMaterial.material = _normalMaterial;
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