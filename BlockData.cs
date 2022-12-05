using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


partial class BlockData
{
    // 모든 블록의 관리하려고 한다.
    // 총 7개의 블록의 모든 방향을 관리할수 있는 형태는? 
    // allBlockData[블록의 종류][블록의 회전 방향]

    // 머릿속으로만 계산하는 것은 좋지 않음    
    /*
            블록의 정보를 담기 위해서 배열을 생성한다. 
            1. 블록 하나의 정보를 담을 수 있는 최소크기는 4*4 
            -> string[4][4] 
            2. 블록은 총 4개의 방향을 가진다. 
            -> string[4][4][4] 
            3. 블록의 종류는 총7개이다. 
            -> string[7][4][4][4] 
     */

    // 모든 블록을 관리하는 것을 배열 또는 리스트로 만든다.
    List<List<string[][]>> allBlockData = new List<List<string[][]>>();
    // string[ , , , ] allBlockData2 = new string[7,4,4,4];

    public List<List<string[][]>> AllBlockData
    {
        get { return allBlockData; }
    }

    public void DataInit()
    {
        // 블록 종류에 해당되는 회전모양
        #region I
        // I형 블록 
        string[][] IA = new string[][]
        {
            new string[] {"■","□","□","□" },
            new string[] {"■","□","□","□" },
            new string[] {"■","□","□","□" },
            new string[] {"■","□","□","□" },
        };

        string[][] IB = new string[][]
        {
            new string[] {"■","■","■","■" },
            new string[] {"□","□","□","□" },
            new string[] {"□","□","□","□" },
            new string[] {"□","□","□","□" }
        };

        string[][] IC = new string[][]
        {
            new string[] {"■","□","□","□" },
            new string[] {"■","□","□","□" },
            new string[] {"■","□","□","□" },
            new string[] {"■","□","□","□" },
        };

        string[][] ID = new string[][]
        {
            new string[] {"■","■","■","■" },
            new string[] {"□","□","□","□" },
            new string[] {"□","□","□","□" },
            new string[] {"□","□","□","□" }
        };
        #endregion

        #region J
        // J형 블록 
        string[][] JA = new string[][]
        {
            new string[] {"■","□","□","□" },
            new string[] {"■","■","■","□" },
            new string[] {"□","□","□","□" },
            new string[] {"□","□","□","□" },
        };

        string[][] JB = new string[][]
        {
            new string[] {"■","■","□","□" },
            new string[] {"■","□","□","□" },
            new string[] {"■","□","□","□" },
            new string[] {"□","□","□","□" },
        };

        string[][] JC = new string[][]
        {
            new string[] {"■","■","■","□" },
            new string[] {"□","□","■","□" },
            new string[] {"□","□","□","□" },
            new string[] {"□","□","□","□" },
        };

        string[][] JD = new string[][]
        {
            new string[] {"□","■","□","□" },
            new string[] {"□","■","□","□" },
            new string[] {"■","■","□","□" },
            new string[] {"□","□","□","□" },
        };
        #endregion

        #region L
        // L형 블록 
        string[][] LA = new string[][]
        {
            new string[] {"■","■","■","□"},
            new string[] {"■","□","□","□"},
            new string[] {"□","□","□","□"},
            new string[] {"□","□","□","□"},
        };

        string[][] LB = new string[][]
        {
            new string[] {"■","■","□","□" },
            new string[] {"□","■","□","□" },
            new string[] {"□","■","□","□" },
            new string[] {"□","□","□","□" },
        };

        string[][] LC = new string[][]
        {
            new string[] {"□","□","■","□" },
            new string[] {"■","■","■","□" },
            new string[] {"□","□","□","□" },
            new string[] {"□","□","□","□" },
        };

        string[][] LD = new string[][]
        {
            new string[] {"■","□","□","□" },
            new string[] {"■","□","□","□" },
            new string[] {"■","■","□","□" },
            new string[] {"□","□","□","□" },
        };
        #endregion

        #region O
        // O형 블록 
        string[][] OA = new string[][]
        {
            new string[] {"■","■","□","□" },
            new string[] {"■","■","□","□" },
            new string[] {"□","□","□","□" },
            new string[] {"□","□","□","□" },
        };

        string[][] OB = new string[][]
        {
            new string[] {"■","■","□","□" },
            new string[] {"■","■","□","□" },
            new string[] {"□","□","□","□" },
            new string[] {"□","□","□","□" },
        };

        string[][] OC = new string[][]
        {
            new string[] {"■","■","□","□" },
            new string[] {"■","■","□","□" },
            new string[] {"□","□","□","□" },
            new string[] {"□","□","□","□" },
        };

        string[][] OD = new string[][]
        {
            new string[] {"■","■","□","□" },
            new string[] {"■","■","□","□" },
            new string[] {"□","□","□","□" },
            new string[] {"□","□","□","□" },
        };
        #endregion

        #region S
        // S형 블록 
        string[][] SA = new string[][]
        {
            new string[] {"□","■","■","□" },
            new string[] {"■","■","□","□" },
            new string[] {"□","□","□","□" },
            new string[] {"□","□","□","□" },
        };

        string[][] SB = new string[][]
        {
            new string[] {"■","□","□","□" },
            new string[] {"■","■","□","□" },
            new string[] {"□","■","□","□" },
            new string[] {"□","□","□","□" },
        };

        string[][] SC = new string[][]
        {
            new string[] {"□","■","■","□" },
            new string[] {"■","■","□","□" },
            new string[] {"□","□","□","□" },
            new string[] {"□","□","□","□" },
        };

        string[][] SD = new string[][]
        {
            new string[] {"■","□","□","□" },
            new string[] {"■","■","□","□" },
            new string[] {"□","■","□","□" },
            new string[] {"□","□","□","□" },
        };
        #endregion

        #region Z
        // Z형 블록 
        string[][] ZA = new string[][]
        {
            new string[] {"■","■","□","□" },
            new string[] {"□","■","■","□" },
            new string[] {"□","□","□","□" },
            new string[] {"□","□","□","□" },
        };

        string[][] ZB = new string[][]
        {
            new string[] {"□","■","□","□" },
            new string[] {"■","■","□","□" },
            new string[] {"■","□","□","□" },
            new string[] {"□","□","□","□" },
        };

        string[][] ZC = new string[][]
        {
            new string[] {"■","■","□","□" },
            new string[] {"□","■","■","□" },
            new string[] {"□","□","□","□" },
            new string[] {"□","□","□","□" },
        };

        string[][] ZD = new string[][]
        {
            new string[] {"□","■","□","□" },
            new string[] {"■","■","□","□" },
            new string[] {"■","□","□","□" },
            new string[] {"□","□","□","□" },
        };
        #endregion

        #region T
        // T형 블록 
        string[][] TA = new string[][]
        {
            new string[] {"□","■","□","□" },
            new string[] {"■","■","■","□" },
            new string[] {"□","□","□","□" },
            new string[] {"□","□","□","□" },
        };

        string[][] TB = new string[][]
        {
            new string[] {"□","■","□","□" },
            new string[] {"□","■","■","□" },
            new string[] {"□","■","□","□" },
            new string[] {"□","□","□","□" },
        };

        string[][] TC = new string[][]
        {
            new string[] {"□","□","□","□" },
            new string[] {"■","■","■","□" },
            new string[] {"□","■","□","□" },
            new string[] {"□","□","□","□" },
        };

        string[][] TD = new string[][]
        {
            new string[] {"□","■","□","□" },
            new string[] {"■","■","□","□" },
            new string[] {"□","■","□","□" },
            new string[] {"□","□","□","□" },
        };
        #endregion

        // 모든 블록종류의 개수
        int allBlockTypeCnt = System.Enum.GetValues(typeof(BlockType)).Length;

        for (int i = 0; i < allBlockTypeCnt; i++)
        {
            // 블록의 종류에 해당되는 공간이 만들어진다.
            allBlockData.Add(new List<string[][]>());

            // 해당 블록 종류의 방향에 해당되는 공간이 만들어짐           
            string blockType = System.Enum.GetValues(typeof(BlockType)).GetValue(i).ToString();
            switch (blockType)
            {
                case "BT_I":
                    allBlockData[i].Add(IA); // 0번째
                    allBlockData[i].Add(IB); // 1번째 
                    allBlockData[i].Add(IC); // 2번째
                    allBlockData[i].Add(ID); // 3번째
                    break;
                case "BT_J":
                    allBlockData[i].Add(JA); // 0번째
                    allBlockData[i].Add(JB); // 1번째 
                    allBlockData[i].Add(JC); // 2번째
                    allBlockData[i].Add(JD); // 3번째
                    break;
                case "BT_L":
                    allBlockData[i].Add(LA); // 0번째
                    allBlockData[i].Add(LB); // 1번째 
                    allBlockData[i].Add(LC); // 2번째
                    allBlockData[i].Add(LD); // 3번째
                    break;
                case "BT_O":
                    allBlockData[i].Add(OA); // 0번째
                    allBlockData[i].Add(OB); // 1번째 
                    allBlockData[i].Add(OC); // 2번째
                    allBlockData[i].Add(OD); // 3번째
                    break;
                case "BT_S":
                    allBlockData[i].Add(SA); // 0번째
                    allBlockData[i].Add(SB); // 1번째 
                    allBlockData[i].Add(SC); // 2번째
                    allBlockData[i].Add(SD); // 3번째
                    break;
                case "BT_Z":
                    allBlockData[i].Add(ZA); // 0번째
                    allBlockData[i].Add(ZB); // 1번째 
                    allBlockData[i].Add(ZC); // 2번째
                    allBlockData[i].Add(ZD); // 3번째
                    break;
                case "BT_T":
                    allBlockData[i].Add(TA);
                    allBlockData[i].Add(TB);
                    allBlockData[i].Add(TC);
                    allBlockData[i].Add(TD);
                    break;
                default:
                    break;
            }
        }

    }
}

