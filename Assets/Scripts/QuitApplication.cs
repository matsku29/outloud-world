using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Outloud.Common
{
    public class QuitApplication : MonoBehaviour
    {
        public void Quit()
        {
            Application.Quit();
        }

        void Update()
        {
            // TODO: If no other view is over this one
            if (Application.platform == RuntimePlatform.Android)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Debug.Log("Exit application pressed!");
                    Quit();
                }
            }
        }
    }
}