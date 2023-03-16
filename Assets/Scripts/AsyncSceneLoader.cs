using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Outloud.Common
{
    public class AsyncSceneLoader : MonoBehaviour
    {
        public string scene;
        public Image progressBar;
        public GameObject loadingScreen;

        void EnterScene()
        {
            StartCoroutine(LoadingScreen());
            
        }

        IEnumerator LoadingScreen()
        {
            loadingScreen.SetActive(true);
            var operation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);

            while (!operation.isDone)
            {
                progressBar.fillAmount = operation.progress;
                yield return null;
            }
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
