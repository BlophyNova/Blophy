using Scenes.Loading;
using Scenes.PublicScripts;
namespace Scenes.SelectMusic
{
    public class SelectMusicStart : PublicButton
    {
        private void Start()
        {
            thisButton.onClick.AddListener(() => LoadingController.Instance.SetLoadSceneByName("Gameplay").StartLoad());
        }
    }
}
