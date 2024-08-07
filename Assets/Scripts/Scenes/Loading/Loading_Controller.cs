using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UtilityCode.Singleton;
namespace Scenes.Loading
{
    public class LoadingController : MonoBehaviourSingleton<LoadingController>
    {
        public Image white;
        public Action Action;
        public void StartLoad()
        {
            StartCoroutine(Alpha0_1());
        }
        private IEnumerator Alpha0_1()
        {
            white.gameObject.SetActive(true);
            Color color = Color.white;
            color.a = 0;
            while (color.a < 1)
            {
                color.a += Time.deltaTime;
                white.color = color;
                yield return new WaitForEndOfFrame();
            }
            Action?.Invoke();
        }
        public void CompleteLoad()
        {
            StartCoroutine(Alpha1_0());
        }
        private IEnumerator Alpha1_0()
        {
            Color color = Color.white;
            while (color.a > 0)
            {
                color.a -= Time.deltaTime;
                white.color = color;
                yield return new WaitForEndOfFrame();
            }
            white.gameObject.SetActive(false);
        }
        public LoadingController SetLoadSceneByName(string targetSceneName)
        {
            Action = () =>
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene()).completed += (AsyncOperation a) => 
                    SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Additive).completed += (AsyncOperation a) =>
                    {
                        SceneManager.SetActiveScene(SceneManager.GetSceneByName(targetSceneName));
                        CompleteLoad();
                    };
            };
            return this;
        }
    }
}
