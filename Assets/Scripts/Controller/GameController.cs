using System.Collections;
using System.Collections.Generic;
using Manager;
using Scenes.DontDestoryOnLoad;
using UnityEngine;
using UtilityCode.Singleton;
namespace Controller
{
    public class GameController : MonoBehaviourSingleton<GameController>
    {
        public bool isLoading;
        public delegate void OnRefreshBox(int index);
        public event OnRefreshBox onRefreshBox;
        public void RefreshBox(int index) => onRefreshBox(index);

        public List<BoxController> boxControllers = new();
        private IEnumerator Start()
        {
            Register();
            isLoading = false;
            for (int i = 0; i < AssetManager.Instance.chartData.boxes.Count; i++)
            {
                BoxController boxController = InstBox(i);
                boxControllers.Add(boxController);
            }
            yield return new WaitForSeconds(3);//等8秒
            StateManager.Instance.IsStart = true;//设置状态IsStart为True
            StateManager.Instance.IsPause = true;
        }

        private static BoxController InstBox(int i)
        {
            BoxController boxController = Instantiate(AssetManager.Instance.boxController, AssetManager.Instance.box)
                .SetSortSeed(i * ValueManager.Instance.noteRendererOrder)//这里的3是每一层分为三小层，第一层是方框渲染层，第二和三层是音符渲染层，有些音符占用两个渲染层，例如Hold，FullFlick
                .Init(AssetManager.Instance.chartData.boxes[i]);
            return boxController;
        }

        void Register()
        {
            onRefreshBox += i =>
            {
                if (boxControllers.Count > i)
                {
                    BoxController tempBox = boxControllers[i];
                    Destroy(tempBox.gameObject);
                    boxControllers[i] = InstBox(i);
                }
                else
                {
                    boxControllers.Add(InstBox(i));
                }
            };
        }
    }
}
