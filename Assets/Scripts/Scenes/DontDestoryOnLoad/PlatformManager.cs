using UnityEngine;
using UtilityCode.Singleton;
namespace Scenes.DontDestoryOnLoad
{
    public class PlatformManager : MonoBehaviourSingleton<PlatformManager>
    {
        public string currentPlatformArchiveDataPath;
        public string editorArchiveDataPath;
        public string androidArchiveDataPath;
        public string iPhoneArchiveDataPath;
        private void Start()
        {
            if (Application.isEditor)
            {
                currentPlatformArchiveDataPath = editorArchiveDataPath;
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
                currentPlatformArchiveDataPath = androidArchiveDataPath;
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                currentPlatformArchiveDataPath = iPhoneArchiveDataPath;
            }
        }
    }
}
