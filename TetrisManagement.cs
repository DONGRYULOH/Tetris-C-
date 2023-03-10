using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Tetris
{
    internal class TetrisManagement
    {
        // 화면상에 보여지는 보드
        TetrisScreen tetrisScreen;

        // 블록 정보 
        Block block;

        public TetrisManagement(TetrisScreen tetrisScreen, Block block) {
            this.tetrisScreen = tetrisScreen;
            this.block = block;
        }


        public void startTetris() {
            while (true)
            {
                Thread.Sleep(1000);
                Console.Clear();
                tetrisScreen.TetrisRender();
                bool keyInputChk = keyInputConfirmStatus(); // 플레이어가 방향키를 눌렀는가?
                if (keyInputChk) {
                    ConsoleKey inputKey = Console.ReadKey().Key;
                    keyInput(inputKey); // 방향키에 해당되는 곳으로 블럭 이동
                }
                else {
                    // 방향키를 누르지 않았을 때 아래로 한칸 이동                                      
                }
            }
        }

        public bool keyInputConfirmStatus() {
            bool keyInput = (Console.KeyAvailable == true) ? true : false;
            return keyInput;
        }
        
        // 키보드의 방향키를 눌렀을 때 블럭이 이동할 방향   
        public void keyInput(ConsoleKey inputKey)
        {
            switch (inputKey)
            {
                case ConsoleKey.UpArrow: // 블록회전
                    block.RotateBlock();                    
                    break;
                case ConsoleKey.DownArrow: // 아래로 1 이동
                    int bottomtWall = tetrisScreen.FrontTetris.Count - 1; // 테트리스 보드의 맨아래쪽 벽 (y축 길이 - 1)
                    bool wallCheck = moveWallBlockCheck(block, inputKey);
                    bool stackBlockCheck = moveStackBlockCheck();
                    bool collisionChk = false; // 충돌여부

                    // 1.아래쪽 벽과 충돌하는가?                
                    // 2.쌓여있는 블록과 충돌하는가?
                    if (wallCheck == true || stackBlockCheck == true)
                    {
                        // 블록으로 가득찬 라인이 있는지 체크하고 가득찬 라인이 있으면 없애기 
                        blockFillCheck();
                        collisionChk = true;
                    }

                    // 아래로 한 칸 이동                
                    if (collisionChk == true) {
                        // 새로운 블록 생성 
                    }
                    else {
                        block.MoveBlock(inputKey);
                    }

                    break;
                case ConsoleKey.LeftArrow: // 왼쪽으로 1이동                
                    block.MoveBlock(inputKey);
                    break;
                case ConsoleKey.RightArrow: // 오른쪽으로 1이동                                 
                    block.MoveBlock(inputKey);
                    break;
                default:
                    break;
            }
        }

        // 블록이 이동 할 때 벽이랑 충돌하는지 확인
        public bool moveWallBlockCheck(Block block, ConsoleKey inputKey)
        {
            // 방향키에 해당되는 방향만큼 이동중인 블록의 위치값을 변경            

            // 1.이동중인 블록의 좌표값을 가져와서 키보드의 방향만큼 좌표를 이동했을 때 벽인지 확인
            for (int y = 0; y < 4; y++) {
                for (int x = 0; x < 4; x++)
                {
                    if (block.currentMoveBlock[y, x] == (int)Block.BlockState.MOVEBLOCK) {
                        if (inputKey == ConsoleKey.DownArrow && tetrisScreen.FrontTetris[y + block.movePositionY + 1][x + block.movePositionX] == (int)Block.BlockState.WALLBLOCK)
                        {
                            return true;
                        }
                        else if (inputKey == ConsoleKey.LeftArrow && tetrisScreen.FrontTetris[y + block.movePositionY][x + block.movePositionX - 1] == (int)Block.BlockState.WALLBLOCK)
                        {
                            return true;
                        }
                        else if (inputKey == ConsoleKey.RightArrow && tetrisScreen.FrontTetris[y + block.movePositionY + 1][x + block.movePositionX + 1] == (int)Block.BlockState.WALLBLOCK)
                        {
                            return true;
                        }                        
                    }
                }
            }

            return false;
        }

        public bool rotateWallBlockCheck()
        {
            return true;
        }

        public bool moveStackBlockCheck()
        {
            return true;
        }

        public bool rotateStackBlockCheck()
        {
            return true;
        }

        public void blockFillCheck()
        {
            
        }

        public bool fillBlockToEmptyBlock()
        {
            return true;
        }

    }
}
