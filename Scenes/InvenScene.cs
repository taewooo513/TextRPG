using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Core.Manager;

namespace TextRPG.Scenes
{
    public class InvenScene : Scene
    {
        UtilManager util;
        int selectTempNum = 0;
        public void Init()
        {
            util = UtilManager.GetInstance();

        }

        public void Release()
        {
        }

        public void Update()
        {
            InventoryCheck();
        }
        private void InventoryCheck()
        {
            Console.Clear();
            Console.WriteLine("인벤토리");

            Console.WriteLine("[아이템 목록]");

            for (int i = 1; i <= util.player.GetPlayerState().possessionItemIndexs.Count; i++)
            {
                UtilManager.ItemState itemState = util.GetItemList()[util.player.GetPlayerState().possessionItemIndexs[i-1]];
                if (itemState.isArmor == false)
                {
                    Console.WriteLine(" - {0} {1}        | 공격력 {2}      | {3}"
                        , i, util.player.GetPlayerState().eWep == itemState.name ?
                        "[E]" + itemState.name : itemState.name, itemState.dmg, itemState.itemInfo);
                }
                else
                {
                    Console.WriteLine(" - {0} {1}        | 방어력 {2}      | {3}"
                        , i, util.player.GetPlayerState().eArm == itemState.name ?
                        "[E]" + itemState.name : itemState.name, itemState.def, itemState.itemInfo);
                }
            }
            Console.WriteLine();
            Console.WriteLine("1. 장착관리");
            Console.WriteLine("2. 나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            while (true)
            {
                int.TryParse(Console.ReadLine(), out selectTempNum);
                if (selectTempNum == 1)
                {
                    Equipped();
                    break;
                }
                else if (selectTempNum == 2)
                {
                    SceneManager.GetInstance().ChangeScene("GameScene");
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요");
                }
            }
        }

        private void Equipped()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("인벤토리 - 장착 관리");

                Console.WriteLine("[아이템 목록]");

                for (int i = 1; i <= util.player.GetPlayerState().possessionItemIndexs.Count; i++)
                {
                    UtilManager.ItemState itemState = util.GetItemList()[util.player.GetPlayerState().possessionItemIndexs[i - 1]];
                    if (itemState.isArmor == false)
                    {
                        Console.WriteLine(" - {0} {1}        | 공격력 {2}      | {3}"
                            , i, util.player.GetPlayerState().eWep == itemState.name ?
                            "[E]" + itemState.name : itemState.name, itemState.dmg, itemState.itemInfo);
                    }
                    else
                    {
                        Console.WriteLine(" - {0} {1}        | 방어력 {2}      | {3}"
                            , i, util.player.GetPlayerState().eArm == itemState.name ?
                            "[E]" + itemState.name : itemState.name, itemState.def, itemState.itemInfo);
                    }
                }

                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                int.TryParse(Console.ReadLine(), out selectTempNum);
                if (util.GetItemList().Count >= selectTempNum && 0 < selectTempNum)
                {
                    if (util.GetItemList()[selectTempNum - 1].isArmor == false)
                    {
                        util.player.SetEWepone(util.GetItemList()[selectTempNum - 1].name);
                    }
                    else
                    {
                        util.player.SetEArmor(util.GetItemList()[selectTempNum - 1].name);
                    }
                }
                else if (selectTempNum == 0)
                {
                    InventoryCheck();
                    break;
                }
            }
        }
    }
}
