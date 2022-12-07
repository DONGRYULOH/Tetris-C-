using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    // 블럭이 어디서부터 시작될지 나타내는 좌표
    int x = 1;
    int y = 0;

    // 블록이 벽을 만나서 움직이고 못하고 쌓임 여부 
    Boolean blockWallMoveCheck = false;

    // 블럭을 이동하고 나서 테트리스 스크린에 이동된 블록을 그려주기 위해 필요하다 
    TetrisScreen tetrisScreen;

    // 블럭을 이동하고 나서 테트리스 스크린에 쌓을 블럭(TODO)
    TetrisScreen tetrisStackArray;

    // 모든블록에 대한 정보(종류, 회전방향)
    BlockData blockData;

    // 현재블록에 대한 정보(모양, 종류, 회전방향)
    string[][] currentBlockShape = null;
    BlockType currentBlockType;
    BlockRotate currentBlockRotate;

    public Block(TetrisScreen tetrisScreen, BlockData blockData)
    {
        // 블록 객체를 생성할때 무조건 생성자로 테트리스 스크린 정보를 가져와야 한다. 
        // 테트리스 스크린 정보가 있어야지 블록이 이동했을 때 스크린에다가 이동된 블록을 표시해 줄 수 있다.    
        this.tetrisScreen = tetrisScreen;

        // 블럭을 이동하고 나서 테트리스 스크린에 쌓을 블럭(TODO)
        this.tetrisStackArray = tetrisScreen;

        // 모든 블록에 대한 정보
        blockData.DataInit();
        this.blockData = blockData;

        // 랜덤 블록 생성
        randomBlockTypeMake();
    }

    // 보드에 블록을 그린다.
    public void draw(ConsoleKey inputKey) {

        // 블록 이동시 벽이 있는지 확인 
        bool moveCheck = blockMoveWallCheck(inputKey);
        if (moveCheck == false) return;

        // 1. 블록을 그릴 보드정보가 필요 
        // 보드에 어느 좌표에 블록을 그릴건지
        // 블록이 처음 떨어지는 위치는? 

        if (inputKey == ConsoleKey.DownArrow) y++;
        else if (inputKey == ConsoleKey.LeftArrow) x--;
        else if (inputKey == ConsoleKey.RightArrow) x++;


        // 블록정보(종류, 회전방향)을 가져와서 블록의 모양을 찾는다. 
        currentBlockShape = blockData.AllBlockData[(int)currentBlockType][(int)currentBlockRotate];

        for (int blockY = 0; blockY < blockData.widthMaxLength; blockY++) { 
            for(int blockX=0; blockX < blockData.heightMaxLength; blockX++)
            {
                if (currentBlockShape[blockY][blockX] == "■") {
                    tetrisScreen.TetristArray[y + blockY][x + blockX] = (int)TetrisBlock.MOVEBLOCK;
                }                
            }
        }  
    }

    // 랜덤 블록 생성
    public void randomBlockTypeMake() {
        Random randomBlock = new Random();

        // 랜덤 블록 종류, 회전방향
        int randomBlockNumberType = randomBlock.Next((int)BlockType.BT_I, (int)BlockType.BT_T); // 0부터 6까지         
        currentBlockType = (BlockType)randomBlockNumberType;
        int randomBlockNumberRotate = randomBlock.Next((int)BlockRotate.RA, (int)BlockRotate.RD); // 0부터 3까지         
        currentBlockRotate = (BlockRotate)randomBlockNumberRotate;

        // 블록 초기화(위치값, 블럭고정여부)
        x = 1;
        y = 0;
        blockWallMoveCheck = false;
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
                draw(inputKey);
                break;
            case ConsoleKey.DownArrow: // 아래로 1 이동                
                draw(inputKey);
                break;
            case ConsoleKey.LeftArrow: // 왼쪽으로 1이동                
                draw(inputKey);
                break;
            case ConsoleKey.RightArrow: // 오른쪽으로 1이동                                 
                draw(inputKey);
                break;
            default:
                break;
        }
    }

    // 블럭 이동시 벽인지 확인한다.
    public bool blockMoveWallCheck(ConsoleKey inputKey) {
        int tempY = this.y;
        int tempX = this.x;
        switch (inputKey)
        {
            case ConsoleKey.UpArrow: // 블록회전
                break;
            case ConsoleKey.DownArrow: // 아래로 1 이동
                tempY++;
                break;
            case ConsoleKey.LeftArrow: // 왼쪽으로 1이동
                tempX--;
                break;
            case ConsoleKey.RightArrow: // 오른쪽으로 1이동 
                tempX++;
                break;
            default:
                break;
        }

        // 테트리스 보드의 왼쪽 벽 
        int leftWall = tetrisScreen.TetristArray[tetrisScreen.TetristArray.Count - 1].Count - tetrisScreen.TetristArray[tetrisScreen.TetristArray.Count - 1].Count;

        // 테트리스 보드의 오른쪽 벽 
        int rightWall = tetrisScreen.TetristArray[tetrisScreen.TetristArray.Count - 1].Count - 1;
        
        // 테트리스 보드의 맨아래쪽 벽 
        int bottomtWall = tetrisScreen.TetristArray.Count - 1;

        currentBlockShape = blockData.AllBlockData[(int)currentBlockType][(int)currentBlockRotate]; // 현재 움직이고 있는 블록을 가져옴
        for (int blockY = 0; blockY < blockData.widthMaxLength; blockY++) {
            for (int blockX = 0; blockX < blockData.heightMaxLength; blockX++) {
                if (currentBlockShape[blockY][blockX] == "■") {

                    // 아래, 왼쪽, 오른쪽으로 이동했을 때 벽이 있는 경우 이동 불가능
                    if (inputKey == ConsoleKey.DownArrow)
                    {
                        if (tempY + blockY == bottomtWall)
                        {
                            for (int a = 0; a < blockData.widthMaxLength; a++)
                            {
                                for (int b = 0; b < blockData.heightMaxLength; b++)
                                {
                                    if (currentBlockShape[a][b] == "■")
                                    {
                                        tetrisScreen.TetristArray[y + a][x + b] = (int)TetrisBlock.MOVEBLOCK;
                                    }
                                }
                            }

                            return false; // 이동불가능
                        }
                    }
                    else if (inputKey == ConsoleKey.LeftArrow) {
                        if (tempX - blockX == leftWall)
                        {
                            // 벽이 있는 경우 블록 이동이 불가능 하므로 이동하기전의 위치에 블록을 그려준다.
                            for (int a = 0; a < blockData.widthMaxLength; a++)
                            {
                                for (int b = 0; b < blockData.heightMaxLength; b++)
                                {
                                    if (currentBlockShape[a][b] == "■")
                                    {
                                        tetrisScreen.TetristArray[y + a][x + b] = (int)TetrisBlock.MOVEBLOCK;
                                    }
                                }
                            }

                            return false; // 이동불가능
                        }
                    }
                    else if (inputKey == ConsoleKey.RightArrow)
                    {
                        if (tempX + blockX == rightWall)
                        {
                            for (int a = 0; a < blockData.widthMaxLength; a++)
                            {
                                for (int b = 0; b < blockData.heightMaxLength; b++)
                                {
                                    if (currentBlockShape[a][b] == "■")
                                    {
                                        tetrisScreen.TetristArray[y + a][x + b] = (int)TetrisBlock.MOVEBLOCK;
                                    }
                                }
                            }

                            return false; // 이동불가능
                        }
                    }
                    
                }
            }
        }

        return true;
    }    

    // 블럭이 이동한다.    
    public void moveBlock()
    {        
        // 플레이어가 방향키를 눌렀을때만 블럭을 이동시킨다.
        if (Console.KeyAvailable == true)
        {
            ConsoleKey inputKey = Console.ReadKey().Key;

            // 1. 블럭 이동시 벽이 있는지 확인한다.
            // blockMoveWallCheck(inputKey);
            

            // 2. 블럭 이동이 가능한 경우 테트리스 맵을 초기화 후 방향키에 해당되는 곳으로 블럭 이동                                                                                 
            tetrisScreen.blockMoveRender(); // 테트리스 맵을 초기화  
            keyInput(inputKey); // 방향키에 해당되는 곳으로 블럭 이동


            // 현재 움직이고 있는 블록이 테트리스 맵을 벗어나는지 확인
            //            blockGetOutMap(inputKey);

            // 블록이 테트리스 보드를 넘어가는 경우 지우면 안됨
            /*          if (TetrisScreen.removeCheck == 0)
                      {
                          tetrisScreen.blockMoveRender(); // 테트리스 맵을 초기화                 
                      }*/

            // 테트리스 맵을 넘어가는 경우 블록이동 X            
            /*if (TetrisScreen.removeCheck == 1) {
                // 테트리스 맵을 넘어가는 여부체크를 0으로 만든다 Why? 블럭이 벽에 부딪히는 경우에 
                // 부딪히는 벽의 방향 반대쪽으로 이동하는 경우는 움직이는게 가능해야되므로 
                TetrisScreen.removeCheck = 0;
            } else {
                keyInput(inputKey);
            }*/

        }
    }

}

