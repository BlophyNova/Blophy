using Scenes.Loading;
using Scenes.PublicScripts;
namespace Scenes.SelectMusic
{
    public class SelectMusicReturn : PublicButton
    {
        // Start is called before the first frame update
        private void Start()
        {
            thisButton.onClick.AddListener(() => LoadingController.Instance.SetLoadSceneByName("SelectChapter").StartLoad());
        }
    }
}
