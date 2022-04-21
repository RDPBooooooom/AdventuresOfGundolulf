using UnityEngine;
using UnityEngine.SceneManagement;

namespace UserInterface
{
    public class MainMenu : MonoBehaviour
    {
        #region Declaring variables

        [SerializeField] private string _startSceneName;

        #endregion

        #region Unity Methods

        private void Start()
        {
            AudioListener.pause = false;
        }

        #endregion

        #region UI Methods

        public void StartButton()
        {
            SceneManager.LoadScene(_startSceneName);
        }

        public void Quit()
        {
            Application.Quit();
        }

        #endregion
    }
}
