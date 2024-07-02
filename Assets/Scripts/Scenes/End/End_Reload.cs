using Scenes.Loading;
using Scenes.PublicScripts;
namespace Scenes.End
{
    public class EndReload : PublicButton
    {
        private void Start()
        {
            thisButton.onClick.AddListener(() => LoadingController.Instance.SetLoadSceneByName("Gameplay").StartLoad());
        }
    }
}
