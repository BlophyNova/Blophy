using Scenes.DontDestoryOnLoad;
using Scenes.PublicScripts;
using TMPro;
namespace Scenes.Settings
{
    public class SettingsIsAutoplay : PublicButton
    {
        public TextMeshProUGUI thisText;
        // Start is called before the first frame update
        private void Start()
        {
            thisButton.onClick.AddListener(() =>
            {
                if (GlobalData.Instance.isAutoplay)
                {
                    GlobalData.Instance.isAutoplay = false;
                    thisText.text = "自动播放: 关";
                }
                else
                {
                    GlobalData.Instance.isAutoplay = true;
                    thisText.text = "自动播放: 开";
                }
            });
        }
    }
}
