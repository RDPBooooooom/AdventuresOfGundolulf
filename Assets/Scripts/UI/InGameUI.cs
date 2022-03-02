using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UserInterface
{
    public class InGameUI : MonoBehaviour
    {
        #region Declaring Variables
        public static InGameUI Instance;
        PlayerScripts.Player player;
        [SerializeField] string mainMenuSceneName;
        [SerializeField] string currentSceneName;
        [SerializeField] GameObject pauseMenu;
        [SerializeField] GameObject ingamePanel;

        [Header("Displays")]
        public Image HealthDisplayBar;
        public Text GoldAmount;
        public Image Item;
        public int Gold;

        public Text AttackValue;
        public Text IntelligenceValue;
        public Text RangeValue;
        public Text HasteValue;
        public Text SpeedValue;

        #endregion
        // Start is called before the first frame update
        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        private void Start()
        {
            player = FindObjectOfType<PlayerScripts.Player>();

            AttackValue.text = "ATT: " + player.Attack.ToString();
            IntelligenceValue.text = "INT: " + player.Intelligence.ToString();
            RangeValue.text = "RA: " + player.Range.ToString();
            HasteValue.text = "HA: " + player.Haste.ToString();
            SpeedValue.text = "SPE: " + player.Speed.ToString();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PauseUnPause()
        {
            if(!pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(true);
                ingamePanel.SetActive(false);
                Time.timeScale = 0;
            }
            else if(pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
                ingamePanel.SetActive(true);
                Time.timeScale = 1;
            }
        }
        public void Restart()
        {
            AudioListener.pause = false;
            Time.timeScale = 1;
            SceneManager.LoadScene(currentSceneName);

        }

        public void MainMenu()
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }
}
