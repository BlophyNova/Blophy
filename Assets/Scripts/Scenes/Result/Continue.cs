using Scenes.PublicScripts;
namespace Scenes.Result
{
    public class Continue : PublicButton
    {
        private void Start()
        {
            thisButton.onClick.AddListener(() => Loading.Controller.Instance.SetLoadSceneByName("SelectMusic").StartLoad());
        }
    }
}
