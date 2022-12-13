using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


partial class BlockData
{

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
    private List<List<int[,]>> allBlockData = new List<List<int[,]>>();

    // 모든 블록의 모양이 들어갈 공간의 최대 가로길이와 최대 세로길이 
    public int widthMaxLength;
    public int heightMaxLength;

    public BlockData(int width, int height) {
        this.widthMaxLength = width;
        this.heightMaxLength = height;
    }

    public List<List<int[,]>> AllBlockData
    {
        get { return allBlockData; }
    }

    public void DataInit()
    {
        // <7개의 블록 종류에 해당되는 모든 회전모양을 2차원 배열로 만들어둔다.>
        // 반시계 방향으로 90도 회전한 모든 방향
        // 테트리스 블록은 총 7개로 고정되어 있기 때문에 동적으로 그려줄 필요는 없다.

        #region I
        // I형 블록 
        int[,] IA = new int[4,4]{            
            {1,0,0,0},                        
            {1,0,0,0},
            {1,0,0,0},
            {1,0,0,0}
        };

        int[,] IB = new int[4, 4]{
            {0,0,0,0},
            {1,1,1,1},            
            {0,0,0,0},
            {0,0,0,0}
        };

        int[,] IC = new int[4, 4]{
            {1,0,0,0},
            {1,0,0,0},
            {1,0,0,0},
            {1,0,0,0}
        };

        int[,] ID = new int[4, 4]{
            {0,0,0,0},
            {1,1,1,1},
            {0,0,0,0},
            {0,0,0,0}
        };
        #endregion


        #region J
        // J형 블록 
        int[,] JA = new int[4, 4]{
            {0,1,0,0},
            {0,1,0,0},
            {1,1,0,0},
            {0,0,0,0}
        };

        int[,] JB = new int[4, 4]{
            {1,1,1,0},
            {0,0,1,0},
            {0,0,0,0},
            {0,0,0,0}
        };

        int[,] JC = new int[4, 4]{
            {1,1,0,0},
            {1,0,0,0},
            {1,0,0,0},
            {0,0,0,0}
        };

        int[,] JD = new int[4, 4]{
            {1,0,0,0},
            {1,1,1,0},
            {0,0,0,0},
            {0,0,0,0}
        };
        #endregion

        #region L
        // L형 블록 
        int[,] LA = new int[4, 4]{
            {1,0,0,0},
            {1,0,0,0},
            {1,1,0,0},
            {0,0,0,0}
        };
        int[,] LB = new int[4, 4]{
            {0,0,1,0},
            {1,1,1,0},
            {0,0,0,0},
            {0,0,0,0}
        };
        int[,] LC = new int[4, 4]{
            {1,1,0,0},
            {0,1,0,0},
            {0,1,0,0},
            {0,0,0,0}
        };
        int[,] LD = new int[4, 4]{
            {1,1,1,0},
            {1,0,0,0},
            {0,0,0,0},
            {0,0,0,0}
        };
        #endregion

        #region O
        // O형 블록 
        int[,] OA = new int[4, 4]{
            {1,1,0,0},
            {1,1,0,0},
            {0,0,0,0},
            {0,0,0,0}
        };
        int[,] OB = new int[4, 4]{
            {1,1,0,0},
            {1,1,0,0},
            {0,0,0,0},
            {0,0,0,0}
        };
        int[,] OC = new int[4, 4]{
            {1,1,0,0},
            {1,1,0,0},
            {0,0,0,0},
            {0,0,0,0}
        };
        int[,] OD = new int[4, 4]{
            {1,1,0,0},
            {1,1,0,0},
            {0,0,0,0},
            {0,0,0,0}
        };
        #endregion

        #region S
        // S형 블록 
        int[,] SA = new int[4, 4]{
            {0,1,1,0},
            {1,1,0,0},
            {0,0,0,0},
            {0,0,0,0}
        };
        int[,] SB = new int[4, 4]{
            {1,0,0,0},
            {1,1,0,0},
            {0,1,0,0},
            {0,0,0,0}
        };
        int[,] SC = new int[4, 4]{
            {0,1,1,0},
            {1,1,0,0},
            {0,0,0,0},
            {0,0,0,0}
        };
        int[,] SD = new int[4, 4]{
            {1,0,0,0},
            {1,1,0,0},
            {0,1,0,0},
            {0,0,0,0}
        };
        #endregion

        #region Z
        // Z형 블록 
        int[,] ZA = new int[4, 4]{
            {1,1,0,0},
            {0,1,1,0},
            {0,0,0,0},
            {0,0,0,0}
        };
        int[,] ZB = new int[4, 4]{
            {1,0,0,0},
            {1,1,0,0},
            {0,1,0,0},
            {0,0,0,0}
        };
        int[,] ZC = new int[4, 4]{
            {1,0,0,0},
            {1,1,0,0},
            {0,1,0,0},
            {0,0,0,0}
        };
        int[,] ZD = new int[4, 4]{
            {1,0,0,0},
            {1,1,0,0},
            {0,1,0,0},
            {0,0,0,0}
        };
        #endregion

        #region T
        // T형 블록 
        int[,] TA = new int[4, 4]{
            {0,1,0,0},
            {1,1,1,0},
            {0,0,0,0},
            {0,0,0,0}
        };
        int[,] TB = new int[4, 4]{
            {0,1,0,0},
            {1,1,0,0},
            {0,1,0,0},
            {0,0,0,0}
        };
        int[,] TC = new int[4, 4]{
            {0,0,0,0},
            {1,1,1,0},
            {0,1,0,0},
            {0,0,0,0}
        };
        int[,] TD = new int[4, 4]{
            {0,1,0,0},
            {0,1,1,0},
            {0,1,0,0},
            {0,0,0,0}
        };
        #endregion

        // 모든 블록종류의 개수
        int allBlockTypeCnt = System.Enum.GetValues(typeof(BlockType)).Length;

        for (int i = 0; i < allBlockTypeCnt; i++)
        {
            // 블록의 종류에 해당되는 공간이 만들어진다.
            allBlockData.Add(new List<int[,]>());

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

