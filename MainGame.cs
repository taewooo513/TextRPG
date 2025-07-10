using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Core.Manager;

//플레이어 정보 입출력완료
// 아이템정보 불러오고
// 상점 페이지 인벤 수정 던전 레벨 구현
// 휴식기능

namespace TextRPG
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Init();
            while (true)
            {
                SceneManager.GetInstance().Update();
            }
        }
        static void Init()
        {
            SceneManager.GetInstance().AddScene("TitleScene", new Scenes.TitleScene());
            SceneManager.GetInstance().AddScene("GameScene", new Scenes.GameScene());
            SceneManager.GetInstance().AddScene("StoreScene", new Scenes.StoreScene());
            SceneManager.GetInstance().AddScene("InvenScene", new Scenes.InvenScene());
            SceneManager.GetInstance().AddScene("StateScene", new Scenes.StateScene());
            SceneManager.GetInstance().AddScene("DungeonScene", new Scenes.DungeonScene());

            SceneManager.GetInstance().ChangeScene("TitleScene");
        }

    }
}