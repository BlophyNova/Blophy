using Scenes.Loading;
using Scenes.PublicScripts;
using UnityEngine.SceneManagement;
namespace Scenes.Title
{
    public class TouchToStart : PublicButton
    {
        private void Start()
        {
            thisButton.onClick.AddListener(() => SceneManager.LoadSceneAsync("GamePlay",LoadSceneMode.Single));
        }
    }
}
