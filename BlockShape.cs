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
        List<int[,]> blockTypeDatas;

        // 모든 블록의 모양이 들어갈 공간의 최대 가로길이와 최대 세로길이 
        public int maxWidthLen;
        public int maxHeightLen;

        public BlockShape()
        {
            maxWidthLen = 4;
            maxHeightLen = 4;
            MakeBlockType();
        }

        public List<int[,]> BlockTypeDatas
        {
            get { return blockTypeDatas; }
        }

        public void MakeBlockType()
        {
            // I형 블록 
            int[,] BLOCK_I = new int[4, 4]{
                {1,0,0,0},
                {1,0,0,0},
                {1,0,0,0},
                {1,0,0,0}
             };
            // J형 블록 
            int[,] BLOCK_J = new int[4, 4]{
                {0,1,0,0},
                {0,1,0,0},
                {1,1,0,0},
                {0,0,0,0}
            };
            // L형 블록 
            int[,] BLOCK_L = new int[4, 4]{
                {1,0,0,0},
                {1,0,0,0},
                {1,1,0,0},
                {0,0,0,0}
            };
            // O형 블록 
            int[,] BLOCK_O = new int[4, 4]{
                {1,1,0,0},
                {1,1,0,0},
                {0,0,0,0},
                {0,0,0,0}
            };
            // S형 블록 
            int[,] BLOCK_S = new int[4, 4]{
                {0,1,1,0},
                {1,1,0,0},
                {0,0,0,0},
                {0,0,0,0}
            };
            // Z형 블록 
            int[,] BLOCK_Z = new int[4, 4]{
                {1,1,0,0},
                {0,1,1,0},
                {0,0,0,0},
                {0,0,0,0}
            };
            // T형 블록 
            int[,] BLOCK_T = new int[4, 4]{
                {0,1,0,0},
                {1,1,1,0},
                {0,0,0,0},
                {0,0,0,0}
            };

            blockTypeDatas = new List<int[,]>();
            // 블록종류의 개수
            int blockTypeCnt = System.Enum.GetValues(typeof(BlockType)).Length;

            for (int i = 0; i < blockTypeCnt; i++)
            {
                string blockType = System.Enum.GetValues(typeof(BlockType)).GetValue(i).ToString(); // 블록 모양의 타입
                switch (blockType)
                {
                    case "BT_I":
                        blockTypeDatas.Add(BLOCK_I);
                        break;
                    case "BT_J":
                        blockTypeDatas.Add(BLOCK_J);
                        break;
                    case "BT_L":
                        blockTypeDatas.Add(BLOCK_L);
                        break;
                    case "BT_O":
                        blockTypeDatas.Add(BLOCK_O);
                        break;
                    case "BT_S":
                        blockTypeDatas.Add(BLOCK_S);
                        break;
                    case "BT_Z":
                        blockTypeDatas.Add(BLOCK_Z);
                        break;
                    case "BT_T":
                        blockTypeDatas.Add(BLOCK_T);
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
