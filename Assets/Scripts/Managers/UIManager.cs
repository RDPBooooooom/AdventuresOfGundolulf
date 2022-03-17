using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private Canvas mainCanvasPrefab;

    #endregion

    #region Properties

    public Canvas MainCanvas { get; private set; }
    public CastScrollUI CastScrollUI { get; private set; }

    #endregion

    #region Unity Methods

    private void Awake()
    {
        MainCanvas = Instantiate(mainCanvasPrefab);
        CastScrollUI = MainCanvas.GetComponentInChildren<CastScrollUI>(true);
    }

    #endregion
}
