using Scenes.Loading;
using Scenes.PublicScripts;
namespace Scenes.SelectChapter
{
    public class SelectChapterStart : PublicButton
    {
        // Start is called before the first frame update
        private void Start()
        {
            thisButton.onClick.AddListener(() => LoadingController.Instance.SetLoadSceneByName("SelectMusic").StartLoad());
        }
    }
}
