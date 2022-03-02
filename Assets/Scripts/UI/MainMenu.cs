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

        private void Start()
        {
            AudioListener.pause = false;
        }

        public void StartButton()
        {
            SceneManager.LoadSceneAsync(startSceneName);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
