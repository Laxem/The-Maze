using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Game
{
    public class UIMenuScaler : MonoBehaviour {

        public Image background;
        public Image Title;
        public Text title;
        public Text explaning;
        public Canvas difficulty;
        public Button begin;

        // Use this for initialization
        void Start ()
        {
            SetUI();
        }

        void Update()
        {
            SetUI();

            if (Input.GetButtonDown("Enter") || Input.GetButtonDown("Return"))
            {
                begin.onClick.Invoke();

            }
            if (Input.GetButtonDown("Echap"))
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainCameraHandler>().ExitGame();
            }
        }
        
        void SetUI()
        {
            //background
            UISetdisplay(background, Screen.width / 2, Screen.height / 2, Screen.width, Screen.height);
            //title
            UISetdisplay(Title, Screen.width / 2, Screen.height * 0.77f, Screen.width / 2, Screen.height / 4);
            //explaning
            UISetdisplay(explaning, Screen.width / 2, Screen.height * 3 / 16, Screen.width, Screen.height / 8);
            //difficulty
            UISetdisplay(difficulty, Screen.width / 2, Screen.height / 2, Screen.width, Screen.height);
            //button
            UISetdisplay(begin, Screen.width / 2, Screen.height / 8, Screen.width / 8, Screen.height / 16);
        }

        void UISetdisplay(MaskableGraphic obj, float posX, float posY, float sizeX, float sizeY)
        {
            RectTransform rectTransform = obj.transform as RectTransform;
            rectTransform.sizeDelta = new Vector2(sizeX, sizeY);
            rectTransform.position = new Vector3(posX, posY, 0f);
        }

        void UISetdisplay(Selectable obj, float posX, float posY, float sizeX, float sizeY)
        {
            RectTransform rectTransform = obj.transform as RectTransform;
            rectTransform.sizeDelta = new Vector2(sizeX, sizeY);
            rectTransform.position = new Vector3(posX, posY, 0f);
        }
        void UISetdisplay(Canvas obj, float posX, float posY, float sizeX, float sizeY)
        {
            RectTransform rectTransform = obj.transform as RectTransform;
            rectTransform.sizeDelta = new Vector2(sizeX, sizeY);
            rectTransform.position = new Vector3(posX, posY, 0f);
        }
    }
}