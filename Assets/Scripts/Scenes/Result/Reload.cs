using Scenes.PublicScripts;
namespace Scenes.Result
{
    public class Reload : PublicButton
    {
        private void Start()
        {
            thisButton.onClick.AddListener(() => Loading.Controller.Instance.SetLoadSceneByName("Gameplay").StartLoad());
        }
    }
}
