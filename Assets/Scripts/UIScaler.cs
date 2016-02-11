using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Game
{
    public class UIScaler : MonoBehaviour
    {
        public Image mapEgde;
        public Image topBorder;
        public Image bottomBorder;
        public Image leftBorder;
        public Image rightBorder;
        private float margin = 150f;


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //MazeBorder
            float sizeMazeBorder = (Mathf.Min(Screen.height, Screen.width))*0.785f+20f;
            RectTransform edgeRect = mapEgde.transform as RectTransform;
            edgeRect.sizeDelta = new Vector2(sizeMazeBorder, sizeMazeBorder);
            edgeRect.position = new Vector3(Screen.width/2, Screen.height/2, 0f);

            //UI background
            RectTransform border = topBorder.transform as RectTransform;
            border.sizeDelta = new Vector2(Screen.width, (Screen.height-sizeMazeBorder)/2+1);
            border.position = new Vector3(Screen.width / 2, Screen.height-(Screen.height - sizeMazeBorder) / 4, 0f);

            border = bottomBorder.transform as RectTransform;
            border.sizeDelta = new Vector2(Screen.width, (Screen.height - sizeMazeBorder) / 2 + 1);
            border.position = new Vector3(Screen.width / 2, (Screen.height - sizeMazeBorder) / 4, 0f);

            border = leftBorder.transform as RectTransform;
            border.sizeDelta = new Vector2((Screen.width - sizeMazeBorder) / 2 + 1, Screen.height);
            border.position = new Vector3(Screen.width-(Screen.width - sizeMazeBorder) / 4, Screen.height / 2, 0f);

            border = rightBorder.transform as RectTransform;
            border.sizeDelta = new Vector2((Screen.width - sizeMazeBorder) / 2 + 1, Screen.height);
            border.position = new Vector3((Screen.width - sizeMazeBorder) / 4, Screen.height / 2, 0f);

        }
    }
}

