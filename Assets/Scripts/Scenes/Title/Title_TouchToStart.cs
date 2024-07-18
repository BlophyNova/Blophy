using Scenes.Loading;
using Scenes.PublicScripts;
using UnityEngine.SceneManagement;
namespace Scenes.Title
{
    public class TitleTouchToStart : PublicButton
    {
        private void Start()
        {
            SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Additive).completed += a =>
                LoadingController.Instance.SetLoadSceneByName("SelectChapter");

            thisButton.onClick.AddListener(() => LoadingController.Instance.StartLoad());
        }
    }
}
