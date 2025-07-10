using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TextRPG.Core.Manager;
using static TextRPG.Core.Manager.UtilManager;

namespace TextRPG
{
    public class Player
    {
        public struct State
        {
            public State()
            {
                eWep = "";
                eArm = "";
            }
            public string name;
            public int job;
            public int lv;
            public int exp;
            public float dmg;
            public int def;
            public int hp;
            public int gold;
            public string eWep;
            public string eArm;
            public List<int> possessionItemIndexs; // 소지중인 아이템
        };

        State playerState;
        public Player(string _name)
        {
            playerState = new State();
            playerState.name = _name;
            playerState.possessionItemIndexs = new List<int>();
            LoadPlayerState();
        }

        private void LoadPlayerState()
        {
            if (UtilManager.GetInstance().IsPlayerFileCheck(playerState.name) == true)
            {
                List<String> strings = UtilManager.GetInstance().GetPlayerFile(playerState.name);
                playerState.lv = int.Parse(strings[0]);
                playerState.hp = int.Parse(strings[1]);
                playerState.job = int.Parse(strings[2]);
                playerState.dmg = float.Parse(strings[3]);
                playerState.def = int.Parse(strings[4]);
                playerState.exp = int.Parse(strings[5]);
                playerState.gold = int.Parse(strings[6]);
                playerState.eWep = strings[7];
                playerState.eArm = strings[8];

                for (int i = 9; i < strings.Count; i++)
                {
                    playerState.possessionItemIndexs.Add(int.Parse(strings[i]));
                }
            }
            else
            {
                playerState.lv = 1;
                playerState.hp = 100;
                playerState.dmg = 10;
                playerState.def = 10;
                playerState.exp = 0;
                playerState.gold = 500; // 초기설정
                playerState.possessionItemIndexs.Add(0);
                playerState.possessionItemIndexs.Add(1);

                SaveState();
            }
        }
        public void SaveState()
        {
            List<String> strings = new List<String>();
            strings.Add(playerState.lv.ToString());
            strings.Add(playerState.hp.ToString());
            strings.Add(playerState.job.ToString());
            strings.Add(playerState.dmg.ToString());
            strings.Add(playerState.def.ToString());
            strings.Add(playerState.exp.ToString());
            strings.Add(playerState.gold.ToString());
            strings.Add(playerState.eWep.ToString());
            strings.Add(playerState.eArm.ToString());

            playerState.possessionItemIndexs.ForEach(e => { strings.Add(e.ToString()); });

            UtilManager.GetInstance().FileInput(strings, playerState.name);
        }
        public State GetPlayerState()
        {
            return playerState;
        }
        public void SetEWepone(string wep)
        {
            if (playerState.eWep == wep)
            {
                playerState.eWep = "";
            }
            else
            {
                playerState.eWep = wep;
            }
        }
        public void SetEArmor(string arm)
        {
            if (playerState.eArm == arm)
            {
                playerState.eArm = "";
            }
            else
            {
                playerState.eArm = arm;
            }
        }

        public void GetGold(int gold)
        {
            playerState.gold += gold;
            SaveState();
        }

        public bool LoseGold(int gold)
        {
            if (playerState.gold - gold >= 0)
            {
                playerState.gold -= gold;
                SaveState();
                return true;
            }
            return false;
        }
        public void LevUp()
        {
            playerState.exp++;
            if (playerState.lv == playerState.exp)
            {
                playerState.exp = 0;
                playerState.lv++;
                playerState.def += 1;
                playerState.dmg += .5f;
            }
            SaveState();
        }
        public void SetHp(int _hp)
        {
            if (_hp > 0)
                playerState.hp = _hp;
            else
                playerState.hp = 0;
            SaveState();

        }
    }
}
