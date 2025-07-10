using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Core.Manager;

namespace TextRPG.Scenes
{
    public class StateScene : Scene
    {
        UtilManager util;
        public void Init()
        {
            util = UtilManager.GetInstance();

        }

        public void Release()
        {
        }

        public void Update()
        {
            StateCheck();
        }
        private void StateCheck()
        {

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("＊＊＊＊＊상태창＊＊＊＊＊");
            Console.WriteLine();
            Console.WriteLine("Lv. {0}", util.player.GetPlayerState().lv);
            Console.WriteLine("직업. {0}", util.player.GetPlayerState().job);
            Console.WriteLine("체력. {0}", util.player.GetPlayerState().hp);
            Console.WriteLine("공격력. {0} {1}", util.player.GetPlayerState().dmg,
                util.player.GetPlayerState().eWep != "" ?
                " (+" + util.FindItem(util.player.GetPlayerState().eWep).Value.dmg + ")" : ""
                );
            Console.WriteLine("방어력. {0} {1}", util.player.GetPlayerState().def,
                util.player.GetPlayerState().eArm != "" ?
                " (+" + util.FindItem(util.player.GetPlayerState().eArm).Value.def + ")" : ""
                );
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            while (true)
            {
                if (Console.ReadLine() == "0")
                {
                    SceneManager.GetInstance().ChangeScene("GameScene");
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");

                    Console.WriteLine("＊＊＊＊＊상태창＊＊＊＊＊");

                    Console.WriteLine("Lv. {0}", util.player.GetPlayerState().lv);
                    Console.WriteLine("직업. {0}", util.player.GetPlayerState().job);
                    Console.WriteLine("체력. {0}", util.player.GetPlayerState().hp);
                    Console.WriteLine("공격력. {0}", util.player.GetPlayerState().dmg);
                    Console.WriteLine("방어력. {0}", util.player.GetPlayerState().def);

                    Console.WriteLine();
                    Console.WriteLine("0. 나가기");
                }
            }
        }
    }
}
