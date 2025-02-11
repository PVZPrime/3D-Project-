using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace player
{
    public class XpScript : MonoBehaviour
    {
        public int XP;
        public int MaxXP = 100;
        public bool Level1;
        public bool Level2;
        public bool Level3;
        public bool Level4;
        public bool Level5;
        public bool Level6;
        void Start()
        {

        }
        void Update()
        {
            if (XP >= MaxXP && !Level1)
            {
                Level1 = true;
                XP -= MaxXP;
            } if (XP >= MaxXP && Level1 && !Level2)
            {
                Level2 = true;
                XP -= MaxXP;
            }if (XP >= MaxXP && Level2 && !Level3)
            {
                Level3 = true;
                XP -= MaxXP;
            }if(XP >= MaxXP && Level3 && !Level4)
            {
                Level4 = true;
                XP -= MaxXP;
            }if(XP>= MaxXP && Level4 && !Level5)
            {
                Level5 = true;
                XP -= MaxXP;
            }if(XP >=MaxXP && Level5 && !Level6)
            {
                Level6 = true;
                XP -= MaxXP;
            }else if(Level6)
            {
                XP = 100;
            }
        }
        public void GiveXP(int XPNum)
        {
            XP += XPNum;
        }
    }
}
