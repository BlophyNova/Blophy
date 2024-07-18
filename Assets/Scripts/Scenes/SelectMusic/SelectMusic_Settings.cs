using Scenes.DontDestoryOnLoad;
using Scenes.Loading;
using Scenes.PublicScripts;
namespace Scenes.SelectMusic
{
    public class SelectMusicSettings : PublicButton
    {
        private void Start()
        {
            GlobalData.Instance.whereToEnterSettings = "SelectMusic";
            thisButton.onClick.AddListener(() => LoadingController.Instance.SetLoadSceneByName("Settings").StartLoad());
        }
    }
}
