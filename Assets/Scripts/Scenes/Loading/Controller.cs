using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UtilityCode.Singleton;
using Random = System.Random;
namespace Scenes.Loading
{
    public class Controller : MonoBehaviourSingleton<Controller>
    {
        public Image white;
        private Action action;
        public float animationDuration = 1f; // 引入的控制动画显示时间的变量
        public TextMeshProUGUI tip;
        public void StartLoad()
        {
            StartCoroutine(IncreaseAlphaAndSetTips());
        }
        private IEnumerator IncreaseAlphaAndSetTips()
        {
            white.gameObject.SetActive(true);
            tip.gameObject.SetActive(false);
            Color color = Color.white;
            Color tipColor = Color.black;
            color.a = 0;
            tipColor.a = 0;
            float elapsedTime = 0;
            TextAsset textAssets = Resources.Load<TextAsset>("Tips/tips");
            string text = textAssets.text;
            string[] tips = text.Split('\n');
            Random rnd = new Random();
            int index = rnd.Next(0, tips.Length);
            string tipText = tips[index];
            tip.text = "|Tip: " + tipText;
            tip.gameObject.SetActive(true);
            while (elapsedTime < animationDuration)
            {
                color.a = Mathf.Clamp01(elapsedTime / animationDuration);
                white.color = color;
                if( elapsedTime > animationDuration * 0.3 )
                {
                    tip.gameObject.SetActive(true);
                    tipColor.a = color.a;
                    tip.color = tipColor;
                }
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            color.a = 1;
            white.color = color;
            action?.Invoke();
        }
        private void CompleteLoad()
        {
            StartCoroutine(DecreaseAlpha());
        }
        private IEnumerator DecreaseAlpha()
        {
            Color color = Color.white;
            Color tipColor = Color.black;
            float elapsedTime = 0;

            while (elapsedTime < animationDuration)
            {
                color.a = Mathf.Clamp01(1 - (elapsedTime / animationDuration));
                white.color = color;
                if( elapsedTime <= animationDuration * 0.7 )
                {
                    tipColor.a = color.a;
                    tip.color = tipColor;
                }
                else { tip.gameObject.SetActive(false); }
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            color.a = 0;
            white.color = color;
            white.gameObject.SetActive(false);
        }
        public Controller SetLoadSceneByName(string targetSceneName)
        {
            action = () =>
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene()).completed += a => 
                    SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Additive).completed += a =>
                    {
                        SceneManager.SetActiveScene(SceneManager.GetSceneByName(targetSceneName));
                        CompleteLoad();
                    };
            };
            return this;
        }
    }
}
