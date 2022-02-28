using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class InGameUI : MonoBehaviour
    {
        #region Declaring Variables
        public static InGameUI Instance;
        public Image HealthDisplayBar;
        public Text GoldAmount;
        public Image Item;
        public int Gold;
        #endregion
        // Start is called before the first frame update
        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
