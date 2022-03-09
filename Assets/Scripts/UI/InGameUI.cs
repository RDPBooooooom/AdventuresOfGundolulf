using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UserInterface
{
    public class InGameUI : MonoBehaviour
    {
        #region Declaring Variables

        public static InGameUI Instance;
        PlayerScripts.Player player;
        private PlayerInput _input;

        [SerializeField] private string _mainMenuSceneName;
        [SerializeField] private string _currentSceneName;
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private GameObject _ingamePanel;

        [Header("Displays")]
        public Image HealthDisplayBar;
        public Text GoldAmount;

        [Header("Stats")]
        [SerializeField] private Text _attackValue;
        [SerializeField] private Text _intelligenceValue;
        [SerializeField] private Text _rangeValue;
        [SerializeField] private Text _hasteValue;
        [SerializeField] private Text _speedValue;

        [Header("Items")]
        public Image Item;

        #endregion

        #region Unity Methods

        // Start is called before the first frame update
        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);

            player = FindObjectOfType<PlayerScripts.Player>();

            _input = new PlayerInput();
            SubscribeToEvents();
        }

        private void Start()
        {
            _input.UI.Enable();

            _attackValue.text = "ATT: " + player.Attack.ToString();
            _intelligenceValue.text = "INT: " + player.Intelligence.ToString();
            _rangeValue.text = "RA: " + player.Range.ToString();
            _hasteValue.text = "HA: " + player.Haste.ToString();
            _speedValue.text = "SPE: " + player.Speed.ToString();
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
