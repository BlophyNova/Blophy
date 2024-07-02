using Scenes.Loading;
using Scenes.PublicScripts;
namespace Scenes.Gameplay
{
    public class GameplayRetry : PublicButton
    {
        // Start is called before the first frame update
        private void Start()
        {
            thisButton.onClick.AddListener(() => LoadingController.Instance.SetLoadSceneByName("Gameplay").StartLoad());
        }
    }
}
