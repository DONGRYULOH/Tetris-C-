using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



// 블록의 회전방향(4개)
enum BlockRotate
{
    RA = 0,
    RB = 1,
    RC = 2,
    RD = 3
}

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

partial class Block
{
    // 블럭이 어디에 생성될지 나타내는 좌표
    int x = 0;
    int y = 0;

    // 블럭을 이동하고 나서 테트리스 스크린에 이동된 블록을 그려주기 위해 필요하다 
    TetrisScreen tetrisScreen;

    // 모든블록에 대한 정보(종류, 회전방향)
    BlockData blockData;

    // 현재블록에 대한 정보(모양, 종류, 회전방향)
    string[][] currentBlock = null;
    BlockType currentBlockType = BlockType.BT_Z;
    BlockRotate currentBlockRotate = BlockRotate.RA;

    public Block(TetrisScreen tetrisScreen, BlockData blockData)
    {
        // 블록 객체를 생성할때 무조건 생성자로 테트리스 스크린 정보를 가져와야 한다. 
        // 테트리스 스크린 정보가 있어야지 블록이 이동했을 때 스크린에다가 이동된 블록을 표시해 줄 수 있다.    
        this.tetrisScreen = tetrisScreen;

        // 모든 블록에 대한 정보
        blockData.DataInit();
        this.blockData = blockData;

        // 랜덤 블록 생성
        randomBlockTypeMake();
    }

    // 랜덤 블록 생성
    public void randomBlockTypeMake() {
        Random randomBlock = new Random();
        int randomBlockNumber = randomBlock.Next((int)BlockType.BT_I, (int)BlockType.BT_T); // 0부터 6까지         
        currentBlockType = (BlockType)randomBlockNumber;        
    }

    // 블록정보(종류, 회전방향)을 가져와서 블록의 모양을 찾고 화살표 방향키만큼 이동
    public void keyInputMoveBlock() {

        // 블록정보(종류, 회전방향)을 가져와서 블록의 모양을 찾는다. 
        currentBlock = blockData.AllBlockData[(int)currentBlockType][(int)currentBlockRotate];
        
        int tetrisMapX_maxLength = tetrisScreen.TetristArray[tetrisScreen.TetristArray.Count - 1].Count;
        int tetrisMapY_maxLength = tetrisScreen.TetristArray.Count;           

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (currentBlock[i][j] == "■")
                {                    
                   tetrisScreen.setBlock(i + y, j + x, TetrisBlock.MOVEBLOCK);                    
                }
            }
        }
    }

    // 키보드의 방향키를 눌렀을 때 블럭이 이동할 방향
    public void keyInput(ConsoleKey inputKey)
    {        
        switch (inputKey)
        {
            case ConsoleKey.UpArrow: // 블록회전
                // 2가지 정보가 필요함 
                // 1. 어떤 블록을 회전시킬건지 
                // 2. 그 블록을 어느 방향으로 회전시킬건지                 
                currentBlockRotate++;
                if ((int)currentBlockRotate == 4) currentBlockRotate = 0;
                keyInputMoveBlock();
                break;
            case ConsoleKey.DownArrow:
                y++; // 블록의 좌표가 y축(아래로)으로 1만큼 이동
                // 현재 움직이고 있는 블록으로 아래로 1만큼 이동 후 테트리스 화면에 그려준다. 
                keyInputMoveBlock();
                break;
            case ConsoleKey.LeftArrow:
                x--;
                // 현재 움직이고 있는 블록으로 왼쪽으로 -1만큼 이동 후 테트리스 화면에 그려준다. 
                keyInputMoveBlock();
                break;
            case ConsoleKey.RightArrow:
                x++;
                // 현재 움직이고 있는 블록으로 오른쪽으로 1만큼 이동 후 테트리스 화면에 그려준다. 
                keyInputMoveBlock();
                break;
            default:
                break;
        }
    }

    // 현재 움직이고 있는 블록이 테트리스 맵을 벗어나는지 확인
    public void blockGetOutMap(ConsoleKey inputKey)
    {        
        // 블록정보(종류, 회전방향)을 가져와서 블록의 모양을 찾는다. 
        currentBlock = blockData.AllBlockData[(int)currentBlockType][(int)currentBlockRotate];
        int tetrisMapX_maxLength = tetrisScreen.TetristArray[tetrisScreen.TetristArray.Count - 1].Count;
        int tetrisMapY_maxLength = tetrisScreen.TetristArray.Count;
        int tempX = this.x;
        int tempY = this.y;

        switch (inputKey)
        {
            case ConsoleKey.UpArrow:                
                // 블록의 모양의 좌표에서 테트리스 보드에서 움직이고 있는 블록의 좌표를 더한다.
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (currentBlock[i][j] == "■")
                        {
                            if (j + tempX >= tetrisMapX_maxLength || i + tempY >= tetrisMapY_maxLength)
                            {
                                TetrisScreen.removeCheck = 1;
                                return;
                            }
                        }
                    }
                }
                break;
            case ConsoleKey.DownArrow:
                tempY++;
                // 블록의 모양의 좌표에서 테트리스 보드에서 움직이고 있는 블록의 좌표를 더한다.
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (currentBlock[i][j] == "■")
                        {
                            if (j + tempX >= tetrisMapX_maxLength || i + tempY >= tetrisMapY_maxLength)
                            {
                                TetrisScreen.removeCheck = 1;
                                return;
                            }
                        }
                    }
                }
                break;
            case ConsoleKey.LeftArrow:
                tempX--;
                // 블록의 모양의 좌표에서 테트리스 보드에서 움직이고 있는 블록의 좌표를 더한다.
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (currentBlock[i][j] == "■")
                        {
                            if (j + tempX <= -1 || i + tempY <= -1)
                            {
                                TetrisScreen.removeCheck = 1;
                                return;
                            }
                        }
                    }
                }
                break;
            case ConsoleKey.RightArrow:
                tempX++;
                // 블록의 모양의 좌표에서 테트리스 보드에서 움직이고 있는 블록의 좌표를 더한다.
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (currentBlock[i][j] == "■")
                        {
                            if (j + tempX >= tetrisMapX_maxLength || i + tempY >= tetrisMapY_maxLength)
                            {
                                TetrisScreen.removeCheck = 1;
                                return;
                            }
                        }
                    }
                }
                break;
            default:
                break;
        }
    }

    // 블럭이 이동한다.    
    public void moveBlock()
    {
        // 플레이어가 방향키를 눌렀을때만 블럭을 이동시킨다.
        if (Console.KeyAvailable == true)
        {
            ConsoleKey inputKey = Console.ReadKey().Key;
            
            // 현재 움직이고 있는 블록이 테트리스 맵을 벗어나는지 확인
            blockGetOutMap(inputKey);

            // 블록이 테트리스 보드를 넘어가는 경우 지우면 안됨
            if (TetrisScreen.removeCheck == 0)
            {
                tetrisScreen.blockMoveRender(); // 테트리스 맵을 초기화                 
            }

            // 테트리스 맵을 넘어가는 경우 블록이동 X
            if (TetrisScreen.removeCheck == 1) {
                // 테트리스 맵을 넘어가는 여부체크를 0으로 만든다 Why? 블럭이 벽에 부딪히는 경우에 
                // 부딪히는 벽의 방향 반대쪽으로 이동하는 경우는 움직이는게 가능해야되므로 
                TetrisScreen.removeCheck = 0;
            } else {
                keyInput(inputKey);
            }
            
        }
    }

}

