using Scenes.DontDestoryOnLoad;
using Scenes.Loading;
using Scenes.PublicScripts;
namespace Scenes.SelectChapter
{
    public class SelectChapterSettings : PublicButton
    {
        // Start is called before the first frame update
        private void Start()
        {
            GlobalData.Instance.whereToEnterSettings = "SelectChapter";
            thisButton.onClick.AddListener(() => LoadingController.Instance.SetLoadSceneByName("Settings").StartLoad());
        }
    }
}
