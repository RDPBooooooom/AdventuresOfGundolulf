using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UserInterface
{
    public class MainMenu : MonoBehaviour
    {
        #region Declaring variables

        [SerializeField] string startSceneName;

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
            SceneManager.LoadScene(startSceneName);
        }

        public void Quit()
        {
            Application.Quit();
        }

        #endregion
    }
}
