using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    // Class that all items will inherit from
    #region Declaring Variables
    protected enum Category
    {
        Melee,
        Magic,
        Teleport,
        Active,
        Stats,
    }
    protected Category category;
    public int value { get; protected set; }

    public Sprite uiImage;
    #endregion
}
