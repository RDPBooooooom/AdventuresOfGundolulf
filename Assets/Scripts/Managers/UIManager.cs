using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private Canvas _mainCanvasPrefab;

    #endregion

    #region Properties

    public Canvas MainCanvas { get; private set; }

    public CastScrollUI CastScrollUI { get; private set; }

    public bool DisablePausePanel { get; set; } = false;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        MainCanvas = Instantiate(_mainCanvasPrefab);
        CastScrollUI = MainCanvas.GetComponentInChildren<CastScrollUI>(true);
    }

    #endregion
}
