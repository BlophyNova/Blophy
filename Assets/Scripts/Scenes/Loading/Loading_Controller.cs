using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading_Controller : MonoBehaviourSingleton<Loading_Controller>
{
    public Image white;
    public Action action;
    public void StartLoad()
    {
        StartCoroutine(Alpha0_1());
    }
    IEnumerator Alpha0_1()
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
        action?.Invoke();
    }
    public void CompleteLoad()
    {
        StartCoroutine(Alpha1_0());
    }
    IEnumerator Alpha1_0()
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
    public Loading_Controller SetLoadSceneByName(string targetSceneName)
    {
        action = () =>
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
