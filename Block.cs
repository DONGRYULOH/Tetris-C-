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
    int x = 0;
    int y = 0;

    // 블럭을 이동하고 나서 테트리스 스크린에 이동된 블록을 그려주기 위해 필요하다 
    TetrisScreen tetrisScreen;

    // 모든블록에 대한 정보(종류, 회전방향)
    protected BlockData blockData;

    // 현재블록에 대한 정보(모양, 종류, 회전방향)
    int[,] currentBlockShape = null;
    BlockType currentBlockType;
    BlockRotate currentBlockRotate;

    // 저장된 블록을 담고 있는 테트리스 보드에 대한 정보
    TetrisDataSaveScreen tetrisDataSaveScreen; 

    public Block(TetrisScreen tetrisScreen, BlockData blockData, TetrisDataSaveScreen tetrisDataSaveScreen)
    {
        // 블록 객체를 생성할때 무조건 생성자로 테트리스 스크린 정보를 가져와야 한다. 
        // 테트리스 스크린 정보가 있어야지 블록이 이동했을 때 스크린에다가 이동된 블록을 표시해 줄 수 있다.    
        this.tetrisScreen = tetrisScreen;        

        // 모든 블록에 대한 정보
        blockData.DataInit();
        this.blockData = blockData;

        // 랜덤 블록 생성
        randomBlockTypeMake();

        // 저장된 블록을 담고 있는 테트리스 보드에 대한 정보
        this.tetrisDataSaveScreen = tetrisDataSaveScreen;
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
        else if (inputKey == ConsoleKey.UpArrow) {
            currentBlockRotate++;
            if ((int)currentBlockRotate == 4) currentBlockRotate = 0;
        }


        // 블록정보(종류, 회전방향)을 가져와서 블록의 모양을 찾는다. 
        currentBlockShape = blockData.AllBlockData[(int)currentBlockType][(int)currentBlockRotate];

        for (int blockY = 0; blockY < blockData.widthMaxLength; blockY++) { 
            for(int blockX=0; blockX < blockData.heightMaxLength; blockX++)
            {
                if (currentBlockShape[blockY,blockX] == 1) {
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
        this.x = 1;
        this.y = 0;            
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
                draw(inputKey);
                break;
            case ConsoleKey.DownArrow: // 아래로 1 이동
                // 아래로 이동시 벽이 있는지 체크(TODO)
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

    // 테트리스 보드에 이동한 블럭을 그려준다.
    public void moveBlockDraw(int[,] currentBlockShape) {
        for (int i = 0; i < blockData.widthMaxLength; i++)
        {
            for (int j = 0; j < blockData.heightMaxLength; j++)
            {
                if (currentBlockShape[i, j] == 1)
                {
                    tetrisScreen.TetristArray[y + i][x + j] = (int)TetrisBlock.MOVEBLOCK;
                }
            }
        }
    }

    // 블럭이 아래로 이동시 벽이 있는지 확인
    public bool downWallCheck(int tempX, int tempY, int bottomtWall)
    {
        // 블록이 더 이상 내려갈수 없으면(벽이 있으면) 자식 테트리스 보드에 블럭을 쌓는다. 
        
        // 블록이 한칸 아래로 내려갔을 때 벽이 있는지 확인        
        for (int a = 0; a < blockData.widthMaxLength; a++)
        {
            for (int b = 0; b < blockData.heightMaxLength; b++)
            {
                if (currentBlockShape[a, b] == 1)
                {
                    if (tempY + a + 1 == bottomtWall)
                    {
                        // 벽이 있으면 블록이 위치하고 있는 좌표값을 알맹이 보드에 저장
                        tetrisDataSaveScreen.blockSave(tempX, tempY, currentBlockShape);
                        tetrisDataSaveScreen.TetrisRender();
                        return false; // 이동 불가능
                    }
                }
            }
        }

        return true;
    }

    // 블럭 이동시 벽인지 확인한다.
    public bool blockMoveWallCheck(ConsoleKey inputKey) {
        int tempY = this.y;
        int tempX = this.x;

        // 테트리스 보드의 왼쪽 벽 (0)
        int leftWall = tetrisScreen.TetristArray[tetrisScreen.TetristArray.Count - 1].Count - tetrisScreen.TetristArray[tetrisScreen.TetristArray.Count - 1].Count;

        // 테트리스 보드의 오른쪽 벽 (x축 길이 - 1)
        int rightWall = tetrisScreen.TetristArray[tetrisScreen.TetristArray.Count - 1].Count - 1;

        // 테트리스 보드의 맨아래쪽 벽 (y축 길이 - 1)
        int bottomtWall = tetrisScreen.TetristArray.Count - 1;

        // 현재 움직이고 있는 블록을 가져옴
        currentBlockShape = blockData.AllBlockData[(int)currentBlockType][(int)currentBlockRotate];

        switch (inputKey)
        {
            case ConsoleKey.UpArrow: // 블록회전
                // 회전하면 벽과 부딪히는지 검사
                int tempRoate = (int)currentBlockRotate + 1; // 블록의 다음 회전 방향
                if (tempRoate == 4) tempRoate = 0;
                int[,] tempBlockRoate = blockData.AllBlockData[(int)currentBlockType][(int)tempRoate]; // 회전방향에 대한 블록 모양

                for (int a = 0; a < blockData.widthMaxLength; a++)
                {
                    for (int b = 0; b < blockData.heightMaxLength; b++)
                    {
                        if (tempBlockRoate[a, b] == 1)
                        {
                            if (tempY + a == bottomtWall || tempX + b == leftWall || tempX + b == rightWall)
                            {
                                moveBlockDraw(currentBlockShape);
                                return false; // 이동 불가능
                            }
                        }
                    }
                }
                break;
            case ConsoleKey.DownArrow: // 아래로 1 이동
                // 벽이 있는지 확인
                downWallCheck(tempX, tempY, bottomtWall);                      
                break;
            case ConsoleKey.LeftArrow: // 왼쪽으로 1이동
                // 1. 이동하기 전에 현재 블록이 위치하고 있는 좌표값을 가져온다.
                // 2. 현재 블록이 위치하고 있는 좌표값에서 이동한 방향만큼 더하거나 빼준다.
                // 3. 이동한 위치에 벽이 있는지 확인한다. 
                // 4. 벽이 있으면 이동하지 않는다. 
                // 5. 벽이 없으면 현재 블록이 위치하고 있는 좌표값에 이동한 방향만큼 더하거나 빼준다.
                for (int a = 0; a < blockData.widthMaxLength; a++)
                {
                    for (int b = 0; b < blockData.heightMaxLength; b++)
                    {
                        if (currentBlockShape[a, b] == 1)
                        {
                            if (tempX + b - 1 == leftWall) {
                                moveBlockDraw(currentBlockShape);
                                return false; // 이동 불가능
                            }
                        }
                    }
                }
                break;
            case ConsoleKey.RightArrow: // 오른쪽으로 1이동 
                for (int a = 0; a < blockData.widthMaxLength; a++)
                {
                    for (int b = 0; b < blockData.heightMaxLength; b++)
                    {
                        if (currentBlockShape[a, b] == 1)
                        {
                            if (tempX + b + 1 == rightWall)
                            {
                                moveBlockDraw(currentBlockShape);
                                return false; // 이동 불가능
                            }
                        }
                    }
                }                
                break;
            default:
                break;
        }

        return true;
    }    

    // 블럭이 이동한다.    
    public void moveBlock()
    {
        // 플레이어가 방향키를 눌렀을때 
        if (Console.KeyAvailable == true)
        {
            ConsoleKey inputKey = Console.ReadKey().Key;

            // 1. 블럭 이동시 벽이 있는지 확인한다.
            // 2. 블럭 이동이 가능한 경우 테트리스 맵을 초기화 후 방향키에 해당되는 곳으로 블럭 이동                                                                                 
            tetrisScreen.tetrisBoardInit(); // 테트리스 맵을 초기화  
            keyInput(inputKey); // 방향키에 해당되는 곳으로 블럭 이동

        }
     /*   else { // 방향키를 누르지 않았을 때 아래로 한칸 이동
            tetrisScreen.tetrisBoardInit(); // 테트리스 맵을 초기화  
            draw(ConsoleKey.DownArrow);
        }*/
    }

}

