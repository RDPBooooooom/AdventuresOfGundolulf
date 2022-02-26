using System;
using Levels;
using PlayerScripts;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Fields

        [SerializeField] private LevelManager _levelManagerPrefab;
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Camera _playerCamPrefab;

        #endregion

        #region Properties

        public GameManager Instance { get; private set; }

        public LevelManager LevelManager { get; private set; }

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
        }

        private void Start()
        {
            LevelManager = Instantiate(_levelManagerPrefab, this.transform);
            LevelManager.GenerateLevel();

            Vector3 roomPosition = LevelManager.CurrentRoom.transform.position;

            Instantiate(_playerPrefab, roomPosition, Quaternion.identity);
            LevelManager.PlayerCam = Instantiate(_playerCamPrefab);
            LevelManager.PlayerCam.transform.position += roomPosition;
        }

        #endregion
    }
}