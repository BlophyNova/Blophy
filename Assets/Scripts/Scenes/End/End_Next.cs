using Scenes.Loading;
using Scenes.PublicScripts;
namespace Scenes.End
{
    public class EndNext : PublicButton
    {
        private void Start()
        {
            thisButton.onClick.AddListener(() => LoadingController.Instance.SetLoadSceneByName("SelectMusic").StartLoad());
        }
    }
}
