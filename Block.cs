using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



// 블록의 회전방향(4개)
enum BlockRotate { 
    RA = 0, 
    RB = 1,
    RC = 2,
    RD = 3
}

// 블록의 모양(7개)
enum BlockType {

    // ■■■■
    BT_I, 

    // ■
    // ■■■■
    BT_J,

    //       ■
    // ■■■■
    BT_L,

    // ■■     
    // ■■
    BT_O,

    //   ■■          
    // ■■
    BT_S,

    // ■■          
    //   ■■
    BT_Z,

    //   ■     
    // ■■■
    BT_T
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
    BlockType currentBlockType = BlockType.BT_T;
    BlockRotate currentBlockRotate = BlockRotate.RA;

    public Block(TetrisScreen tetrisScreen ,BlockData blockData) {
        // 블록 객체를 생성할때 무조건 생성자로 테트리스 스크린 정보를 가져와야 한다. 
        // 테트리스 스크린 정보가 있어야지 블록이 이동했을 때 스크린에다가 이동된 블록을 표시해 줄 수 있다.    
        this.tetrisScreen = tetrisScreen;

        // 모든 블록에 대한 정보
        blockData.DataInit();
        this.blockData = blockData;
    }

    // 키보드의 방향키를 눌렀을 때 블럭이 이동할 방향
    public void keyInput() {        
        ConsoleKey key = Console.ReadKey().Key;
        switch (key)
        {
            case ConsoleKey.UpArrow: // 블록회전
                // 2가지 정보가 필요함 
                // 1. 어떤 블록을 회전시킬건지 
                // 2. 그 블록을 어느 방향으로 회전시킬건지 

                // 블록을 회전
                currentBlockRotate++;
                if ((int)currentBlockRotate == 4) currentBlockRotate = 0;

                // 블록정보(종류, 회전방향)을 가져와서 블록의 모양을 찾는다. 
                currentBlock = blockData.AllBlockData[(int)currentBlockType][(int)currentBlockRotate];

                // 블록 회전후 모양을 테트리스 판에 그리기 
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (currentBlock[i][j] == "■")
                        {
                            tetrisScreen.setBlock(i+y, j+x, TetrisBlock.MOVEBLOCK);
                        }
                    }
                }
         
         
                break;
            case ConsoleKey.DownArrow:
                y++; // 블록의 좌표가 y축(아래로)으로 1만큼 이동
                currentBlock = blockData.AllBlockData[(int)currentBlockType][(int)currentBlockRotate];

                // 현재 움직이고 있는 블록으로 아래로 1만큼 이동 후 테트리스 화면에 그려준다. 
                for (int i = 0; i < 4; i++) {
                    for (int j = 0; j < 4; j++) {
                        if (currentBlock[i][j] == "■") {
                            // 해당블록의 좌표값에서 y축으로 이동한 만큼 좌표를 변경
                            tetrisScreen.setBlock(i+y, j+x, TetrisBlock.MOVEBLOCK);
                        }                        
                    }
                    Console.WriteLine("");
                }
                                
                break;
            case ConsoleKey.LeftArrow:
                x--;
                currentBlock = blockData.AllBlockData[(int)currentBlockType][(int)currentBlockRotate];

                // 현재 움직이고 있는 블록으로 오른쪽으로 1만큼 이동 후 테트리스 화면에 그려준다. 
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (currentBlock[i][j] == "■")
                        {
                            // 해당블록의 좌표값에서 y축으로 이동한 만큼 좌표를 변경
                            tetrisScreen.setBlock(i + y, j + x, TetrisBlock.MOVEBLOCK);
                        }
                    }
                    Console.WriteLine("");
                }
                break;
            case ConsoleKey.RightArrow:
                x++;
                currentBlock = blockData.AllBlockData[(int)currentBlockType][(int)currentBlockRotate];

                // 현재 움직이고 있는 블록으로 오른쪽으로 1만큼 이동 후 테트리스 화면에 그려준다. 
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (currentBlock[i][j] == "■")
                        {
                            // 해당블록의 좌표값에서 y축으로 이동한 만큼 좌표를 변경
                            tetrisScreen.setBlock(i+y, j+x, TetrisBlock.MOVEBLOCK);
                        }
                    }
                    Console.WriteLine("");
                }
                break;
            default:
                break;
        }
    }

    // 블럭이 이동한다.
    // 블럭이 이동하고 난뒤 이전에 블럭에 대한 위치를 테트리스 스크린에서 지워준다.
    public void moveBlock() {        
        // 플레이어가 방향키를 눌렀을때만 블럭을 이동시킨다.
        if (Console.KeyAvailable == true) {                       
            // 현재 블록의 위치를 테트리스 공간에서 지움
            tetrisScreen.blockMoveRender();

            // 플레이어가 누른 방향키에 해당되는 좌표로 테트리스 공간에 블럭을 생성한다.
            keyInput();
        }        
    }

}

