using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

/*
     블록의 종류별 모양을 만들어주는 클래스
 */
namespace Tetris
{

    // 블록의 모양(7개)
    enum BlockType
    {

        // ■■■■
        BT_I = 0,

        // ■
        // ■■■
        BT_J = 1,

        //     ■
        // ■■■
        BT_L = 2,

        // ■■     
        // ■■
        BT_O = 3,

        //   ■■          
        // ■■
        BT_S = 4,

        // ■■          
        //   ■■
        BT_Z = 5,

        //   ■     
        // ■■■
        BT_T = 6
    }
 
    internal class BlockShape
    {
        private List<List<int[,]>> blockTypeDatas;

        // 모든 블록의 모양이 들어갈 공간의 최대 가로길이와 최대 세로길이 
        private const int MAX_WIDTH_LEN = 4;
        private const int MAX_HEIGHT_LEN = 4;

        public BlockShape()
        {            
            MakeBlockType();
        }

        public List<List<int[,]>> BlockTypeDatas
        {
            get { return blockTypeDatas; }
        }

        /*
            블록을 회전했을때의 좌표값은 어떻게 가져올건지? 
            -> 블록마다 모든 회전방향이 필요함
            -> 모든 블록에 대해서 회전방향별로 각각 모양을 만들어야 되는데 이렇게 하면 나중에 하나 수정할때 여러개를 건들어야 됨 
         */
        public void MakeBlockType()
        {
            // I형 블록 
            int[,] BLOCK_IA = new int[MAX_HEIGHT_LEN, MAX_WIDTH_LEN]{
                {2,0,0,0},
                {2,0,0,0},
                {2,0,0,0},
                {2,0,0,0}
             };
            int[,] BLOCK_IB = new int[MAX_HEIGHT_LEN, MAX_WIDTH_LEN]{
                {0,0,0,0},
                {2,2,2,2},
                {0,0,0,0},
                {0,0,0,0}
             };
            int[,] BLOCK_IC = new int[MAX_HEIGHT_LEN, MAX_WIDTH_LEN]{
                {2,0,0,0},
                {2,0,0,0},
                {2,0,0,0},
                {2,0,0,0}
             };
            int[,] BLOCK_ID = new int[MAX_HEIGHT_LEN, MAX_WIDTH_LEN]{
                {0,0,0,0},
                {2,2,2,2},
                {0,0,0,0},
                {0,0,0,0}
             };

            // J형 블록 
            int[,] BLOCK_JA = new int[MAX_HEIGHT_LEN, MAX_WIDTH_LEN]{
                {0,2,0,0},
                {0,2,0,0},
                {2,2,0,0},
                {0,0,0,0}
            };
            int[,] BLOCK_JB = new int[MAX_HEIGHT_LEN, MAX_WIDTH_LEN]{
                {0,2,0,0},
                {0,2,2,2},
                {0,0,0,0},
                {0,0,0,0}
            };
            int[,] BLOCK_JC = new int[MAX_HEIGHT_LEN, MAX_WIDTH_LEN]{
                {0,2,2,0},
                {0,2,0,0},
                {0,2,0,0},
                {0,0,0,0}
            };
            int[,] BLOCK_JD = new int[MAX_HEIGHT_LEN, MAX_WIDTH_LEN]{
                {0,2,2,2},
                {0,0,0,2},
                {0,0,0,0},
                {0,0,0,0}
            };

            // L형 블록 
            int[,] BLOCK_LA = new int[MAX_HEIGHT_LEN, MAX_WIDTH_LEN]{
                {2,0,0,0},
                {2,0,0,0},
                {2,2,0,0},
                {0,0,0,0}
            };
            int[,] BLOCK_LB = new int[MAX_HEIGHT_LEN, MAX_WIDTH_LEN]{
                {0,0,2,0},
                {2,2,2,0},
                {0,0,0,0},
                {0,0,0,0}
            };
            int[,] BLOCK_LC = new int[MAX_HEIGHT_LEN, MAX_WIDTH_LEN]{
                {2,2,0,0},
                {0,2,0,0},
                {0,2,0,0},
                {0,0,0,0}
            };
            int[,] BLOCK_LD = new int[MAX_HEIGHT_LEN, MAX_WIDTH_LEN]{
                {2,2,2,0},
                {2,0,0,0},
                {0,0,0,0},
                {0,0,0,0}
            };

            // O형 블록 
            int[,] BLOCK_OA = new int[MAX_HEIGHT_LEN, MAX_WIDTH_LEN]{
                {2,2,0,0},
                {2,2,0,0},
                {0,0,0,0},
                {0,0,0,0}
            };
            int[,] BLOCK_OB = new int[MAX_HEIGHT_LEN, MAX_WIDTH_LEN]{
                {2,2,0,0},
                {2,2,0,0},
                {0,0,0,0},
                {0,0,0,0}
            };
            int[,] BLOCK_OC = new int[MAX_HEIGHT_LEN, MAX_WIDTH_LEN]{
                {2,2,0,0},
                {2,2,0,0},
                {0,0,0,0},
                {0,0,0,0}
            };
            int[,] BLOCK_OD = new int[MAX_HEIGHT_LEN, MAX_WIDTH_LEN]{
                {2,2,0,0},
                {2,2,0,0},
                {0,0,0,0},
                {0,0,0,0}
            };

            // S형 블록 
            int[,] BLOCK_SA = new int[MAX_HEIGHT_LEN, MAX_WIDTH_LEN]{
                {0,2,2,0},
                {2,2,0,0},
                {0,0,0,0},
                {0,0,0,0}
            };
            int[,] BLOCK_SB = new int[MAX_HEIGHT_LEN, MAX_WIDTH_LEN]{
                {0,2,0,0},
                {0,2,2,0},
                {0,0,2,0},
                {0,0,0,0}
            };
            int[,] BLOCK_SC = new int[MAX_HEIGHT_LEN, MAX_WIDTH_LEN]{
                {0,2,2,0},
                {2,2,0,0},
                {0,0,0,0},
                {0,0,0,0}
            };
            int[,] BLOCK_SD = new int[MAX_HEIGHT_LEN, MAX_WIDTH_LEN]{
                {0,2,0,0},
                {0,2,2,0},
                {0,0,2,0},
                {0,0,0,0}
            };

            // Z형 블록 
            int[,] BLOCK_ZA = new int[MAX_HEIGHT_LEN, MAX_WIDTH_LEN]{
                {2,2,0,0},
                {0,2,2,0},
                {0,0,0,0},
                {0,0,0,0}
            };
            int[,] BLOCK_ZB = new int[MAX_HEIGHT_LEN, MAX_WIDTH_LEN]{
                {0,0,2,0},
                {0,2,2,0},
                {0,2,0,0},
                {0,0,0,0}
            };
            int[,] BLOCK_ZC = new int[MAX_HEIGHT_LEN, MAX_WIDTH_LEN]{
                {2,2,0,0},
                {0,2,2,0},
                {0,0,0,0},
                {0,0,0,0}
            };
            int[,] BLOCK_ZD = new int[MAX_HEIGHT_LEN, MAX_WIDTH_LEN]{
                {0,0,2,0},
                {0,2,2,0},
                {0,2,0,0},
                {0,0,0,0}
            };

            // T형 블록 
            int[,] BLOCK_TA = new int[MAX_HEIGHT_LEN, MAX_WIDTH_LEN]{
                {0,2,0,0},
                {2,2,2,0},
                {0,0,0,0},
                {0,0,0,0}
            };
            int[,] BLOCK_TB = new int[MAX_HEIGHT_LEN, MAX_WIDTH_LEN]{
                {0,2,0,0},
                {0,2,2,0},
                {0,2,0,0},
                {0,0,0,0}
            };
            int[,] BLOCK_TC = new int[MAX_HEIGHT_LEN, MAX_WIDTH_LEN]{
                {0,0,0,0},
                {2,2,2,0},
                {0,2,0,0},
                {0,0,0,0}
            };
            int[,] BLOCK_TD = new int[MAX_HEIGHT_LEN, MAX_WIDTH_LEN]{
                {0,2,0,0},
                {2,2,0,0},
                {0,2,0,0},
                {0,0,0,0}
            };

            blockTypeDatas = new List<List<int[,]>>();
            
            int blockTypeCnt = System.Enum.GetValues(typeof(BlockType)).Length; // 블록종류의 개수

            for (int i = 0; i < blockTypeCnt; i++)
            {
                blockTypeDatas.Add(new List<int[,]>()); // 7가지 블록 종류 생성                

                string blockType = System.Enum.GetValues(typeof(BlockType)).GetValue(i).ToString(); // 블록 모양의 타입
                switch (blockType)
                {
                    case "BT_I":
                        blockTypeDatas[i].Add(BLOCK_IA);
                        blockTypeDatas[i].Add(BLOCK_IB);
                        blockTypeDatas[i].Add(BLOCK_IC);
                        blockTypeDatas[i].Add(BLOCK_ID);
                        break;
                    case "BT_J":
                        blockTypeDatas[i].Add(BLOCK_JA);
                        blockTypeDatas[i].Add(BLOCK_JB);
                        blockTypeDatas[i].Add(BLOCK_JC);
                        blockTypeDatas[i].Add(BLOCK_JD);
                        break;
                    case "BT_L":
                        blockTypeDatas[i].Add(BLOCK_LA);
                        blockTypeDatas[i].Add(BLOCK_LB);
                        blockTypeDatas[i].Add(BLOCK_LC);
                        blockTypeDatas[i].Add(BLOCK_LD);
                        break;
                    case "BT_O":
                        blockTypeDatas[i].Add(BLOCK_OA);
                        blockTypeDatas[i].Add(BLOCK_OB);
                        blockTypeDatas[i].Add(BLOCK_OC);
                        blockTypeDatas[i].Add(BLOCK_OD);
                        break;
                    case "BT_S":
                        blockTypeDatas[i].Add(BLOCK_SA);
                        blockTypeDatas[i].Add(BLOCK_SB);
                        blockTypeDatas[i].Add(BLOCK_SC);
                        blockTypeDatas[i].Add(BLOCK_SD);
                        break;
                    case "BT_Z":
                        blockTypeDatas[i].Add(BLOCK_ZA);
                        blockTypeDatas[i].Add(BLOCK_ZB);
                        blockTypeDatas[i].Add(BLOCK_ZC);
                        blockTypeDatas[i].Add(BLOCK_ZD);
                        break;
                    case "BT_T":
                        blockTypeDatas[i].Add(BLOCK_TA);
                        blockTypeDatas[i].Add(BLOCK_TB);
                        blockTypeDatas[i].Add(BLOCK_TC);
                        blockTypeDatas[i].Add(BLOCK_TD);
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
