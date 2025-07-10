using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Core.Manager;
namespace TextRPG.Scenes
{
    internal class TitleScene : Scene
    {
        public void Init()
        {
        }
        public void Update()
        {
            SettingNickName();
        }
        public void SettingNickName()
        {
            while (true)
            {
                Console.WriteLine("***이름별로 정보를 저장합니다***");
                Console.WriteLine("원하시는 이름을 설정해주세요.");
                Console.WriteLine();
                string _name = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("입력하신 이름은 {0}입니다.", _name);
                Console.WriteLine();
                Console.WriteLine("1. 저장");
                Console.WriteLine("2. 취소");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                while (true)
                {
                    int number;
                    int.TryParse(Console.ReadLine(), out number);
                    if (number == 1)
                    {
                        UtilManager.GetInstance().SetMyName(_name);
                        break;
                    }
                    else if (number == 2)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("1. 저장");
                        Console.WriteLine("2. 취소");
                        Console.WriteLine();
                        Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요");
                    }
                }
                if (UtilManager.GetInstance().GetMyName() != "")
                {

                    SceneManager.GetInstance().ChangeScene("GameScene");
                    UtilManager.GetInstance().StartGame();
                    break;
                }
            }

        }

        public void Release()
        {
            Console.Clear();
        }
    }
}
