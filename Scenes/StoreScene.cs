using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Core.Manager;

/*
 * 그다음 던전
 * 휴식기능 추가
 * 그다음 레벨 시스템
 * 다한 다음 사소한 텍스트 오류수정좀하자
 */

namespace TextRPG.Scenes
{
    public class StoreScene : Scene
    {
        UtilManager util;
        int selectTempNum;
        public void Init()
        {
            util = UtilManager.GetInstance();

        }

        public void Release()
        {
        }

        public void Update()
        {
            Store();
        }

        private void Store()
        {
            Console.Clear();

            Console.WriteLine("상점");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine(util.player.GetPlayerState().gold + "G");
            Console.WriteLine();
            int idx = 0;
            for (int i = 1; i <= util.GetItemList().Count; i++)
            {
                UtilManager.ItemState itemState = util.GetItemList()[i - 1];
                string payText;
                payText = itemState.pay.ToString();

                if (util.player.GetPlayerState().possessionItemIndexs.Count > idx)
                {
                    if (itemState.itemId == util.player.GetPlayerState().possessionItemIndexs[idx])
                    {
                        payText = "보유중인 아이템";
                        idx++;
                    }
                }

                if (itemState.isArmor == false)
                {
                    Console.WriteLine(" - {0}        | 공격력 {1}      | {2} | {3}"
                        , itemState.name, itemState.dmg, itemState.itemInfo, payText);
                }
                else
                {
                    Console.WriteLine(" - {0}        | 방어력 {1}      | {2} | {3}"
                         , itemState.name, itemState.def, itemState.itemInfo, payText);
                }
            }
            Console.WriteLine();
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");

            int.TryParse(Console.ReadLine(), out selectTempNum);

            if (selectTempNum == 1)
            {
                BuyItem();
            }
            else if (selectTempNum == 2)
            {
                SellItem();
            }
            else if (selectTempNum == 0)
            {
                SceneManager.GetInstance().ChangeScene("GameScene");
            }
        }
        public void BuyItem()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("상점 - 아이템 구매");
                Console.WriteLine();
                Console.WriteLine("[보유 골드]");
                Console.WriteLine(util.player.GetPlayerState().gold + "G");
                Console.WriteLine();
                int idx = 0;
                for (int i = 1; i <= util.GetItemList().Count; i++)
                {
                    UtilManager.ItemState itemState = util.GetItemList()[i - 1];
                    string payText;
                    payText = itemState.pay.ToString();

                    if (util.player.GetPlayerState().possessionItemIndexs.Count > idx)
                    {
                        if (itemState.itemId == util.player.GetPlayerState().possessionItemIndexs[idx])
                        {
                            payText = "보유중인 아이템";
                            idx++;
                        }
                    }

                    if (itemState.isArmor == false)
                    {
                        Console.WriteLine(" - {4}. {0}        | 공격력 {1}      | {2} | {3}"
                            , itemState.name, itemState.dmg, itemState.itemInfo, payText, i);
                    }
                    else
                    {
                        Console.WriteLine(" - {4}. {0}        | 방어력 {1}      | {2} | {3}"
                             , itemState.name, itemState.def, itemState.itemInfo, payText, i);
                    }
                }

                Console.WriteLine();

                Console.WriteLine("0. 나가기");
                bool isout = false;
                while (true)
                {
                    int.TryParse(Console.ReadLine(), out selectTempNum);
                    if (selectTempNum != 0 && selectTempNum - 1 < util.GetItemList().Count)
                    {
                        bool isCheck = false;
                        // 아이템 찾아서 중복인지 확인
                        for (int i = 0; i < util.player.GetPlayerState().possessionItemIndexs.Count; i++)
                        {
                            if (util.GetItemList()[selectTempNum - 1].itemId == util.player.GetPlayerState().possessionItemIndexs[i])
                            {
                                isCheck = true;
                                break;
                            }
                        }
                        if (!isCheck)
                        {
                            if (util.BuyItem(util.GetItemList()[selectTempNum - 1].itemId,
                               util.GetItemList()[selectTempNum - 1].pay))
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("구매 실패 ");
                            }
                        }
                    }

                    if (selectTempNum == 0)
                    {
                        isout = true;
                        break;
                    }
                }
                if (isout == true)
                {
                    break;
                }
            }
        }
        public void SellItem()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("상점 - 아이템 판매");
                Console.WriteLine();
                Console.WriteLine("[보유 골드]");
                Console.WriteLine(util.player.GetPlayerState().gold + "G");
                Console.WriteLine();
                for (int i = 1; i <= util.player.GetPlayerState().possessionItemIndexs.Count; i++)
                {
                    UtilManager.ItemState itemState = util.GetItemList()[util.player.GetPlayerState().possessionItemIndexs[i - 1]];
                    if (itemState.isArmor == false)
                    {
                        Console.WriteLine(" -{4} {0}        | 공격력 {1}      | {2} | {3}"
                            , itemState.name, itemState.dmg, itemState.itemInfo, itemState.pay, i);
                    }
                    else
                    {
                        Console.WriteLine(" -{4} {0}        | 방어력 {1}      | {2} | {3}"
                             , itemState.name, itemState.def, itemState.itemInfo, itemState.pay, i);
                    }
                }
                Console.WriteLine();
                Console.WriteLine("0. 나가기");

                int.TryParse(Console.ReadLine(), out selectTempNum);

                if (selectTempNum - 1 < util.player.GetPlayerState().possessionItemIndexs.Count && selectTempNum != 0)
                {
                    var a = util.GetItemList()[util.player.GetPlayerState().possessionItemIndexs[selectTempNum - 1]].name;
                    util.SellItem(util.player.GetPlayerState().possessionItemIndexs[selectTempNum - 1],
                        util.GetItemList()[util.player.GetPlayerState().possessionItemIndexs[selectTempNum - 1]].pay);
                    if (util.player.GetPlayerState().eWep == a)
                    {
                        util.player.SetEWepone("");
                    }
                    else if (util.player.GetPlayerState().eArm == a)
                    {
                        util.player.SetEArmor("");
                    }
                }

                if (selectTempNum == 0)
                {
                    break;
                }
            }
        }
    }
}
