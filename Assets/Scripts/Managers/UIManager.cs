using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private Canvas mainCanvasPrefab;

    #endregion

    #region Properties

    public Canvas MainCanvas { get; private set; }

    #endregion

    #region Unity Methods

    private void Awake()
    {
        MainCanvas = Instantiate(mainCanvasPrefab);
    }

    #endregion
}
