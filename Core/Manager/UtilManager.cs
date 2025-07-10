using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextRPG.Core.Manager
{
    internal class UtilManager : Singleton<UtilManager>
    {
        private string myName = "";
        public Player player;
        public struct ItemState
        {
            public string name;
            public int dmg;
            public int def;
            public bool isArmor;
            public int pay;
            public string itemInfo;
            public int itemId;
        };

        List<ItemState> itemList; // 생각안하고 리스트로 만들었지만 맵으로 바꾸기 귀찮아서 그냥 냅둠
        public void StartGame()
        {
            player = new Player(myName);
            LoadItemState();
        }
        public List<ItemState> GetItemList()
        {
            return itemList;
        }
        public void LoadItemState()
        {
            itemList = new List<ItemState>();
            List<string>[] ilist = LoadItemList();

            for (int i = 0; i < ilist.Length; i++)
            {
                ItemState itemState = new ItemState();
                itemState.name = ilist[i][0];
                itemState.isArmor = bool.Parse(ilist[i][2]);
                if (itemState.isArmor == true)
                {
                    itemState.def = int.Parse(ilist[i][1]);
                }
                else
                {
                    itemState.dmg = int.Parse(ilist[i][1]);
                }
                itemState.pay = int.Parse(ilist[i][3]);
                itemState.itemInfo = ilist[i][4];
                itemState.itemId = int.Parse(ilist[i][5]);
                itemList.Add(itemState);
            }
        }
        public ItemState? FindItem(string name)
        {
            foreach (ItemState item in itemList)
            {
                if (item.name == name)
                {
                    return item;
                }
            }
            return null;
        }
        public void Test() // 말그대로 테스트함수
        {
            ItemState itemState = new ItemState();
            itemState.name = "도란의 검";
            itemState.dmg = 10;
            itemState.isArmor = false;
            itemState.pay = 450;
            itemState.itemInfo = "도란이 오다 주운 검";
            itemState.itemId = 0;

            ItemState itemState2 = new ItemState();
            itemState2.name = "도란의 방패";
            itemState2.def = 10;
            itemState2.isArmor = true;
            itemState2.pay = 450;
            itemState2.itemInfo = "도란이 나무를 쳤는데 만들어진 방패";
            itemState2.itemId = 1;

            itemList.Add(itemState);
            itemList.Add(itemState2);

            player.GetPlayerState().possessionItemIndexs.Sort(
                (n1, n2) => { return n1 > n2 ? 1 : -1; }
                );
        }

        public bool BuyItem(int index, int gold)
        {
            bool isBuy = player.LoseGold(gold);
            if (isBuy == true)
            {
                player.GetPlayerState().possessionItemIndexs.Add(index);
                player.GetPlayerState().possessionItemIndexs.Sort(
                    (n1, n2) => { return n1 > n2 ? 1 : -1; }
                    );
            }
            return isBuy;
        }
        public void SellItem(int index, int gold)
        {
            float _g = gold * 0.85f;
            player.GetGold((int)_g); // 소수점 그냥 날려버림 
            player.GetPlayerState().possessionItemIndexs.Remove(index);
        }
        public void FileInput(List<String> strs, string name)
        {
            string _path = name + @".txt";
            if (File.Exists(_path))
            {
                File.WriteAllText(_path, StrsMerge(strs));
            }
            else
            {
                StreamWriter textWrite = File.CreateText(_path);
                File.WriteAllText(_path, StrsMerge(strs));
                textWrite.Dispose();
            }
        }

        public List<string>[]? LoadItemList()
        {
            string _path = @"itemList.txt";
            List<string>[] strings;
            try
            {
                List<String> content = File.ReadLines(_path).ToList();

                strings = new List<string>[content.Count];

                for (int i = 0; i < content.Count; i++)
                {
                    strings[i] = new List<String>();
                }
                int index = 0;
                content.ForEach(s =>
                {
                    s.Split(new char[] { ',' }).ToList().ForEach(e =>
                    {
                        strings[index].Add(e);
                    });
                    index++;
                });
                Console.Write("");
                return strings;
            }
            catch (Exception e)
            {

            }
            return null;
        }

        public String StrsMerge(List<String> strs) // 문자열 배열 합치기
        {
            String result = "";
            int a = 0;
            strs.ForEach(str =>
            {
                result += str;
                a++;
                if (a < strs.Count)
                {
                    result += ",";
                }
            });
            return result;
        }
        public List<String> GetPlayerFile(string name)
        {
            List<String> result = new List<String>();
            string _path = name + @".txt";

            try
            {
                String content = File.ReadAllText(_path);
                result = content.Split(new char[] { ',' }).ToList();
            }
            catch (Exception e)
            {

            }
            return result;
        }
        public bool IsPlayerFileCheck(string name) // 이름별로 파일 분류되게끔 있는지 확인
        {
            string _path = name + @".txt";
            if (File.Exists(_path))
            {
                return true;
            }
            else
            {
                StreamWriter textWrite = File.CreateText(_path);
                textWrite.Dispose();
                return false;
            }
        }

        public string GetMyName() { return myName; }
        public void SetMyName(string _name) { myName = _name; }

    }
}
