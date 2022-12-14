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
    // 블록이 어디서부터 시작될지 나타내는 좌표
    int x = 0;
    int y = 0;

    // 블록을 이동하고 나서 껍데기 보드에 이동된 블록을 그려주기 위해 필요
    TetrisScreen tetrisScreen;

    // 모든 블록에 대한 정보(종류, 회전방향)
    BlockData blockData;

    // 이동하고 있는 블록에 대한 정보(모양, 종류, 회전방향)
    int[,] currentBlockShape;
    BlockType currentBlockType;
    BlockRotate currentBlockRotate;

    // 쌓여있는 블록을 담고 있는 알맹이 보드
    TetrisDataSaveScreen tetrisDataSaveScreen;

    // 랜덤 블록 생성을 위한 랜덤 함수
    Random randomBlock = new Random();

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

        // 쌓여있는 블록을 담고 있는 알맹이 보드
        this.tetrisDataSaveScreen = tetrisDataSaveScreen;
    }

    // 보드에 블록을 그린다.
    public void draw(ConsoleKey inputKey) {

        // 블록 이동시 벽이 있는지 확인 
        bool moveCheck = blockMoveWallCheck(inputKey);
        if (moveCheck == false) return;
        
        // 방향키에 해당되는 방향으로 좌표값을 이동
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
            for(int blockX = 0; blockX < blockData.heightMaxLength; blockX++)
            {
                // 블럭이 존재(1)하는 좌표값만 껍데기 테트리스의 위치에 넣어준다.
                if (currentBlockShape[blockY, blockX] == 1) {
                    tetrisScreen.TetristArray[y + blockY][x + blockX] = (int)TetrisBlock.MOVEBLOCK;
                }                
            }
        }  
    }

    // 랜덤 블록 생성
    public void randomBlockTypeMake() {
        
        // 랜덤 블록 종류, 회전방향
        int randomBlockNumberType = randomBlock.Next((int)BlockType.BT_I, (int)BlockType.BT_T); // 0부터 6까지         
        currentBlockType = (BlockType)randomBlockNumberType;        
        currentBlockRotate = (int)BlockRotate.RA;

        // 블록 초기화(위치값, 블럭고정여부)
        this.x = 1;
        this.y = 0;

        // 블록의 모양을 가져와서 껍데기 보드에 넣어준다.
        currentBlockShape = blockData.AllBlockData[(int)currentBlockType][(int)currentBlockRotate];
        for (int blockY = 0; blockY < blockData.widthMaxLength; blockY++)
        {
            for (int blockX = 0; blockX < blockData.heightMaxLength; blockX++)
            {
                if (currentBlockShape[blockY, blockX] == 1)
                {
                    tetrisScreen.TetristArray[y + blockY][x + blockX] = (int)TetrisBlock.MOVEBLOCK;
                }
            }
        }

        // 알맹이 보드에 쌓인 블럭을 껍데기 보드에 넣어준다.
        for (int y = 0; y < TetrisDataSaveScreen.tetrisStackArray.Count; y++)
        {
            for (int x = 0; x < TetrisDataSaveScreen.tetrisStackArray[y].Count; x++)
            {
                if (TetrisDataSaveScreen.tetrisStackArray[y][x] == (int)TetrisBlock.MOVEBLOCK)
                {
                    tetrisScreen.TetristArray[y][x] = TetrisDataSaveScreen.tetrisStackArray[y][x];
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
        // 현재 움직이고 있는 블록을 가져옴
        currentBlockShape = blockData.AllBlockData[(int)currentBlockType][(int)currentBlockRotate];


        // 1.현재 움직이고 있는 블록의 좌표값을 가져와서 아래로 한칸 이동했을때 맨아래층의 벽과 충돌하는지 확인 
        for (int a = 0; a < blockData.widthMaxLength; a++)
        {
            for (int b = 0; b < blockData.heightMaxLength; b++)
            {
                if (currentBlockShape[a, b] == 1)
                {
                    // 블록이 더 이상 내려갈수 없으면(벽이 있으면) 자식 테트리스 보드에 블럭을 쌓는다.         
                    if (tempY + a + 1 == bottomtWall)
                    {                        
                        tetrisDataSaveScreen.blockSave(tempX, tempY, currentBlockShape); // 벽이 있으면 블록이 위치하고 있는 좌표값을 알맹이 보드에 저장                                    
                        tetrisDataSaveScreen.blockFullCheck(); // 블록이 다 차있는 라인이 있는지 검사                      
                        randomBlockTypeMake();    // 랜덤블록생성 후 블록이 떨어지는 위치값 초기화
                        return false; // 이동 불가능
                    }
                }
            }
        }

        // 알맹이 보드에 쌓여있는 블록의 좌표값을 가져와서 현재 움직이고 있는 블록과 충돌하는지 확인 
        for (int y = 0; y < TetrisDataSaveScreen.tetrisStackArray.Count; y++)
        {
            for (int x = 0; x < TetrisDataSaveScreen.tetrisStackArray[y].Count; x++)
            {
                // 알맹이 보드에 쌓여있는 블록의 좌표값을 모두 가져온다.        
                if (TetrisDataSaveScreen.tetrisStackArray[y][x] == (int)TetrisBlock.MOVEBLOCK) {
                    for (int a = 0; a < blockData.widthMaxLength; a++)
                    {
                        for (int b = 0; b < blockData.heightMaxLength; b++)
                        {                            
                            if (currentBlockShape[a, b] == 1) // 블록이 존재하는 좌표값일 경우만 
                            {
                                // 블록이 더 이상 내려갈수 없으면(벽이 있으면) 자식 테트리스 보드에 블럭을 쌓는다.
                                // 껍데기 보드상의 블록 위치 + 블록의 모양을 저장하는 공간에서 블록이 있는 좌표값을 가져옴 + 아래로 한칸 이동 == 알맹이 보드에 쌓여 있는 블록의 Y좌표값과 일치
                                if (tempY + a + 1 == y)
                                {
                                    if(tempX + b  == x) {                                         
                                        tetrisDataSaveScreen.blockSave(tempX, tempY, currentBlockShape); // 벽이 있으면 블록이 위치하고 있는 좌표값을 알맹이 보드에 저장                                    
                                        tetrisDataSaveScreen.blockFullCheck(); // 블록이 다 차있는 라인이 있는지 검사                                         
                                        randomBlockTypeMake(); // 랜덤블록생성 후 블록이 떨어지는 위치값 초기화
                                        return false; // 이동 불가능
                                    }
                                }
                            }
                        }
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
                bool result = downWallCheck(tempX, tempY, bottomtWall);
                if (result == false) return false;
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
        // 테트리스 맵을 초기화  
        tetrisScreen.tetrisBoardInit(); 

        // 플레이어가 방향키를 눌렀을때 
        if (Console.KeyAvailable == true)
        {
            ConsoleKey inputKey = Console.ReadKey().Key;            
            keyInput(inputKey); // 방향키에 해당되는 곳으로 블럭 이동
        }
        else { // 방향키를 누르지 않았을 때 아래로 한칸 이동            
            draw(ConsoleKey.DownArrow);
        }
    }

}

