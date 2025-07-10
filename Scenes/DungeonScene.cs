using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TextRPG.Core.Manager;
// 내일 잔텍스처 처리만하고 제출 
namespace TextRPG.Scenes
{
    public class DungeonScene : Scene
    {
        int selectTempNum = 0;
        int dungeonLevel = 0;
        public void Init()
        {
        }

        public void Release()
        {
        }

        public void Update()
        {
            Console.Clear();
            Console.WriteLine("던전입장");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine("현재 체력 : {0}", UtilManager.GetInstance().player.GetPlayerState().hp);
            Console.WriteLine();
            Console.WriteLine("1. 벤들시티     | 방어력 5 이상 권장");
            Console.WriteLine("2. 빌지워터     | 방어력 11 이상 권장");
            Console.WriteLine("3.  자  운      | 방어력 17 이상 권장");
            Console.WriteLine("4. 프렐요드     | 방어력 24 이상 권장");
            Console.WriteLine("5. 데마시아     | 방어력 30 이상 권장");
            Console.WriteLine("6.  녹서스      | 방어력 40 이상 권장");
            Console.WriteLine();
            Console.WriteLine("0. 돌아가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            while (true)
            {
                int.TryParse(Console.ReadLine(), out selectTempNum);


                if (selectTempNum > 0 && selectTempNum <= 6)//던전 맞게 눌렀는지 쳌
                {
                    if (UtilManager.GetInstance().player.GetPlayerState().hp > 0)
                    {
                        dungeonLevel = selectTempNum;
                        DungeonIn(dungeonLevel);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("체력이 부족합니다.");
                    }
                }
                else if (selectTempNum == 0)
                {
                    SceneManager.GetInstance().ChangeScene("GameScene");
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }

        public void DungeonIn(int _dungeonIndex)
        {
            string _name = "";
            int getGole = 0;
            int needDef = 0;

            switch (_dungeonIndex)
            {
                case 1:
                    needDef = 5;
                    getGole = 1000;
                    _name = "벤들시티";
                    break;
                case 2:
                    needDef = 11;
                    getGole = 1400;
                    _name = "빌지워터";
                    break;
                case 3:
                    needDef = 17;
                    getGole = 1800;
                    _name = "자운";
                    break;
                case 4:
                    needDef = 24;
                    getGole = 2200;
                    _name = "프렐요드";
                    break;
                case 5:
                    needDef = 30;
                    getGole = 2500;
                    _name = "데마시아";
                    break;
                case 6:
                    needDef = 40;
                    getGole = 3000;
                    _name = "녹서스";
                    break;
            }
            bool isClear = true;
            getGole += getGole + (int)((float)getGole * ((UtilManager.GetInstance().player.GetPlayerState().dmg * 2) * 0.01f));
            if (UtilManager.GetInstance().player.GetPlayerState().def < needDef)
            {
                Random rd = new Random();
                if (rd.Next(0, 5) < 2)
                {
                    isClear = false;
                }
            }
            Console.Clear();
            int beforGold = UtilManager.GetInstance().player.GetPlayerState().gold;
            Console.WriteLine("던전입장");
            int beforHp = UtilManager.GetInstance().player.GetPlayerState().hp;
            if (isClear == true)
            {
                int val = UtilManager.GetInstance().player.GetPlayerState().def - needDef;
                int res = 0;
                Random rd = new Random();
                res = rd.Next(20 - val, 35 - val);
                Console.WriteLine("{0}을 클리어 하였습니다.", _name);
                UtilManager.GetInstance().player.GetGold(getGole);
                UtilManager.GetInstance().player.SetHp((int)(UtilManager.GetInstance().player.GetPlayerState().hp - res));
                UtilManager.GetInstance().player.LevUp();
            }
            if (isClear == false)
            {
                Console.WriteLine("{0}을 클리어 실패 하였습니다.", _name);
                UtilManager.GetInstance().player.SetHp((int)(UtilManager.GetInstance().player.GetPlayerState().hp * 0.5f));
            }

            Console.WriteLine();
            Console.WriteLine("[탐험 결과]");
            Console.WriteLine("체력 {0}-> {1}", beforHp, UtilManager.GetInstance().player.GetPlayerState().hp);
            Console.WriteLine("Gold {0}G -> {1}", beforGold, UtilManager.GetInstance().player.GetPlayerState().gold);
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.WriteLine();

            while (true)
            {
                int.TryParse(Console.ReadLine(), out selectTempNum);

                if (selectTempNum != 0)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
                else
                {
                    break;
                }
            }
        }
    }
}
