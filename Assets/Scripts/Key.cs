using UnityEngine;
using System.Collections;

namespace Game
{
    public class Key : MonoBehaviour
    {

        public void TouchPlayer()
        {
            GameHandler.instance.keyTaken = true;
            gameObject.SetActive(false);
                
        }
    }
}