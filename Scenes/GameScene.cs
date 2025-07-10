using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Core.Manager;
using static TextRPG.Core.Manager.UtilManager;

namespace TextRPG.Scenes
{
    public class GameScene : Scene
    {
        UtilManager util;
        int selectTempNum;
        public void Init()
        {
            util = UtilManager.GetInstance();
        }

        public void Release()
        {
            Console.Clear();
        }
        public void Update()
        {
            Console.Clear();
            Console.WriteLine("소환사의 협곡에 오신것을 환영합니다");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기\n2. 인벤토리\n3. 상점\n4. 던전입장\n5. 휴식하기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.WriteLine();
            bool isRe = true;
            while (isRe)
            {
                int.TryParse(Console.ReadLine(), out selectTempNum);
                switch (selectTempNum)
                {
                    case 1:
                        SceneManager.GetInstance().ChangeScene("StateScene");
                        isRe = false;
                        break;
                    case 2:
                        SceneManager.GetInstance().ChangeScene("InvenScene");
                        isRe = false;
                        break;
                    case 3:
                        SceneManager.GetInstance().ChangeScene("StoreScene");
                        isRe = false;
                        break;
                    case 4:
                        SceneManager.GetInstance().ChangeScene("DungeonScene");
                        isRe = false;
                        break;
                    case 5:
                        RestHp();
                        isRe = false;
                        break;
                    default:
                        Console.WriteLine("잘못된 입력");
                        break;
                }
            }
        }
        private void RestHp()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("휴식하기");
                Console.WriteLine();
                Console.WriteLine("현재 체력 : {0}", util.player.GetPlayerState().hp);
                Console.WriteLine("500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {0} G)", util.player.GetPlayerState().gold);
                Console.WriteLine();
                Console.WriteLine("1. 휴식하기");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                bool isOut = false;
                while (true)
                {
                    int.TryParse(Console.ReadLine(), out selectTempNum);
                    if (selectTempNum == 1)
                    {
                        util.player.LoseGold(500);
                        util.player.SetHp(100);
                        break;
                    }
                    else if (selectTempNum == 0)
                    {
                        isOut = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다 ");
                    }
                }
                if (isOut)
                    break;
            }
        }
        /*
         휴식하기
500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : 800 G)

1. 휴식하기
0. 나가기

원하시는 행동을 입력해주세요.
>>
         */

    }
}
// 메크로 없나 매니저 가독성 개구린데;;