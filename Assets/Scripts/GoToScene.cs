using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Outloud.Common
{
    public class GoToScene : MonoBehaviour
    {
        public string scene;
        void EnterScene()
        {
            SceneManager.LoadScene(scene);
        }

        public void OnClick()
        {
            FadeInController controller = FindObjectOfType<FadeInController>();
            if (controller != null)
            {
                controller.StartFade(EnterScene);
            }
            else
            {
                EnterScene();
            }
        }
    }
}
