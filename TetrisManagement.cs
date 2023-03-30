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
                Thread.Sleep(500);
                Console.Clear();                
                tetrisScreen.TetrisRender();
                bool keyInputChk = keyInputConfirmStatus(); // 플레이어가 방향키를 눌렀는가?
                if (keyInputChk) 
                {
                    ConsoleKey inputKey = Console.ReadKey().Key;
                    keyInput(inputKey); // 방향키에 해당되는 곳으로 블럭 이동
                } 
                else 
                {   // 방향키를 누르지 않았을 때 아래로 한칸 이동
                    bool wallCheck = moveWallBlockCheck(block, ConsoleKey.DownArrow);
                    bool stackBlockCheck = moveStackBlockCheck(block, ConsoleKey.DownArrow);                    

                    // 충돌 여부 검사(아래쪽 벽과 충돌하는가? 또는 2.쌓여있는 블록과 충돌하는가?)
                    if (wallCheck == true || stackBlockCheck == true) 
                    {
                        if (deadLineCheck()) {
                            Console.Clear();
                            tetrisScreen.TetrisRender();
                            Console.WriteLine("게임 오버");
                            break;
                        } else {
                            if (blockFillLineCheck())
                            {
                                blockFillLineRemove();
                            }
                            else 
                            {
                                changeBlockState();
                            }                            
                            block.randomBlockPick();
                            block.BlockInit();
                        }
                    } 
                    else 
                    {
                        MoveBlock(ConsoleKey.DownArrow);
                    }                    
                }
            }
        }

        // 랜덤으로 생성된 블록을 테트리스 보드에 그려줌
        public void blockDraw() {
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (block.currentMoveBlock[(int)block.CurrentBlockRotate][y, x] == (int)Block.BlockState.MOVEBLOCK)
                    {
                        tetrisScreen.FrontTetris[block.MovePositionY + y][block.MovePositionX + x] = (int)Block.BlockState.MOVEBLOCK;
                    }
                }
            }
        }

        public bool deadLineCheck() {
            blockDraw(); // 랜덤으로 생성된 블록을 테트리스 보드에 그려주기

            for (int y = 0; y > -1; y--) {
                for (int x = 1; x < tetrisScreen.tetrisBoardGetX - 1; x++)
                {
                    if (tetrisScreen.FrontTetris[y][x] == (int)Block.BlockState.MOVEBLOCK) {
                        return true;
                    }
                }
            }

            return false;
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
                    bool wallCheck = moveWallBlockCheck(block, inputKey);
                    bool stackBlockCheck = moveStackBlockCheck(block, inputKey);
                    bool collisionChk = false; // 충돌여부

                    // 1.아래쪽 벽과 충돌하는가? 또는 2.쌓여있는 블록과 충돌하는가?
                    if (wallCheck == true || stackBlockCheck == true)
                    {
                        if (blockFillLineCheck()) {
                            blockFillLineRemove();
                        }
                        collisionChk = true;
                    }
                    
                    if (collisionChk == true) { // 랜덤 블록 생성 
                        // 블록이 충돌하는 경우는 
                        block.BlockInit();
                        block.randomBlockPick();
                    }
                    else { // 블록이동 
                        MoveBlock(inputKey);
                    }

                    break;
                case ConsoleKey.LeftArrow: // 왼쪽으로 1이동                
                    MoveBlock(inputKey);
                    break;
                case ConsoleKey.RightArrow: // 오른쪽으로 1이동                                 
                    MoveBlock(inputKey);
                    break;
                default:
                    break;
            }
        }

        // 이동중인 블록을 쌓여있는 블록으로 상태를 변경
        public void changeBlockState()
        {
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (block.currentMoveBlock[(int)block.CurrentBlockRotate][y, x] == (int)Block.BlockState.MOVEBLOCK)
                    {
                        tetrisScreen.FrontTetris[y + block.MovePositionY][x + block.MovePositionX] = (int)Block.BlockState.STACKBLOCK;
                    }
                }
            }
        }

        // 블록이 이동 할 때 벽이랑 충돌하는지 확인
        public bool moveWallBlockCheck(Block block, ConsoleKey inputKey)
        {            
            // 1.이동중인 블록의 좌표값을 가져와서 키보드의 방향만큼 좌표를 이동했을 때 벽인지 확인
            for (int y = 0; y < 4; y++) {
                for (int x = 0; x < 4; x++)
                {
                    if (block.currentMoveBlock[(int)block.CurrentBlockRotate][y, x] == (int)Block.BlockState.MOVEBLOCK) {
                        if (inputKey == ConsoleKey.DownArrow && tetrisScreen.FrontTetris[y + block.MovePositionY + 1][x + block.MovePositionX] == (int)Block.BlockState.WALLBLOCK)
                        {
                            return true;
                        }
                        else if (inputKey == ConsoleKey.LeftArrow && tetrisScreen.FrontTetris[y + block.MovePositionY][x + block.MovePositionX - 1] == (int)Block.BlockState.WALLBLOCK)
                        {
                            return true;
                        }
                        else if (inputKey == ConsoleKey.RightArrow && tetrisScreen.FrontTetris[y + block.MovePositionY + 1][x + block.MovePositionX + 1] == (int)Block.BlockState.WALLBLOCK)
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

        // 쌓여있는 블록과 충돌여부 검사 
        public bool moveStackBlockCheck(Block block, ConsoleKey inputKey)
        {
            // 1.이동중인 블록의 좌표값을 가져와서 키보드의 방향만큼 좌표를 이동했을 때 벽인지 확인
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (block.currentMoveBlock[(int)block.CurrentBlockRotate][y, x] == (int)Block.BlockState.MOVEBLOCK)
                    {
                        if (inputKey == ConsoleKey.DownArrow && tetrisScreen.FrontTetris[block.MovePositionY + y + 1][block.MovePositionX + x] == (int)Block.BlockState.STACKBLOCK)
                        {
                            return true;
                        }
                        else if (inputKey == ConsoleKey.LeftArrow && tetrisScreen.FrontTetris[block.MovePositionY + y][block.MovePositionX - 1 + x] == (int)Block.BlockState.STACKBLOCK)
                        {
                            return true;
                        }
                        else if (inputKey == ConsoleKey.RightArrow && tetrisScreen.FrontTetris[block.MovePositionY +  y + 1][block.MovePositionX + x + 1] == (int)Block.BlockState.STACKBLOCK)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;            
        }

        public bool rotateStackBlockCheck()
        {
            return true;
        }

        // 블록 이동 
        public void MoveBlock(ConsoleKey inputKey)
        {

            // 이동하기 전 블록을 비어있는 블록으로 변경
            for (int blockY = 0; blockY < 4; blockY++)
            {
                for (int blockX = 0; blockX < 4; blockX++)
                {
                    if (block.currentMoveBlock[(int)block.CurrentBlockRotate][blockY, blockX] == (int)Block.BlockState.MOVEBLOCK)
                    {
                        tetrisScreen.FrontTetris[blockY + block.MovePositionY][blockX + block.MovePositionX] = (int)Block.BlockState.NONBLOCK;
                    }
                }
            }

            // 방향키에 해당되는 방향만큼 이동중인 블록의 위치값을 변경
            if (inputKey == ConsoleKey.DownArrow)
            {
                block.MovePositionY++;                
            }
            else if (inputKey == ConsoleKey.LeftArrow)
            {
                block.MovePositionX--;                
            }
            else if (inputKey == ConsoleKey.RightArrow)
            {
                block.MovePositionX++;                
            }
            else if (inputKey == ConsoleKey.UpArrow)
            {
                block.CurrentBlockRotate++;
                if ((int)block.CurrentBlockRotate == 4) block.CurrentBlockRotate = 0;
            }

            // 이동중인 블록을 해당 방향만큼 이동
            for (int blockY = 0; blockY < 4; blockY++)
            {
                for (int blockX = 0; blockX < 4; blockX++)
                {
                    if (block.currentMoveBlock[(int)block.CurrentBlockRotate][blockY, blockX] == (int)Block.BlockState.MOVEBLOCK)
                    {
                        tetrisScreen.FrontTetris[blockY + block.MovePositionY][blockX + block.MovePositionX] = (int)Block.BlockState.MOVEBLOCK;                        
                    }
                }
            }

        }

        // 블록으로 가득찬 라인을 없애기
        public void blockFillLineRemove() {
            int blockMoveCnt = 0; // 블록 이동횟수 
            int blockNumCnt = 0; // 해당라인에 블록이 몇개가 있는지

            // 벽을 제외한 보드의 맨 아래층 부터 해당 라인이 블록으로 모두 채워져 있는지 확인 
            for (int y = tetrisScreen.tetrisBoardGetY - 2; y > -1; y--)
            {
                for (int x = 1; x < tetrisScreen.tetrisBoardGetX - 1; x++)
                {
                    if (tetrisScreen.FrontTetris[y][x] == (int)Block.BlockState.STACKBLOCK || tetrisScreen.FrontTetris[y][x] == (int)Block.BlockState.MOVEBLOCK)
                    {
                        blockNumCnt++;
                    }
                }

                // 해당라인에 블록이 하나라도 없는 경우는 다음 라인의 블록 여부를 검사할 필요가 없음
                if (blockNumCnt == 0) break;

                // 해당라인이 블록으로 가득 차있으면(양쪽 벽 제외) 비어있는 블록으로 변경 
                if (blockNumCnt == tetrisScreen.tetrisBoardGetX - 2)
                {                    
                    for (int x = 1; x < tetrisScreen.tetrisBoardGetX - 1; x++)
                    {
                        tetrisScreen.FrontTetris[y][x] = (int)Block.BlockState.NONBLOCK;
                    }
                    blockMoveCnt++; // 블록이동 횟수 1증가                    
                }
                else if (blockNumCnt >= 1 && blockNumCnt != tetrisScreen.tetrisBoardGetX - 2 && blockMoveCnt > 0)
                {
                    // 해당 라인에 있는 모든 블록(쌓여있는 블록, 이동중인 블록)을 내리기 
                    for (int x = 1; x < tetrisScreen.tetrisBoardGetX - 1; x++)
                    {
                        if(tetrisScreen.FrontTetris[y][x] == (int)Block.BlockState.STACKBLOCK || tetrisScreen.FrontTetris[y][x] == (int)Block.BlockState.MOVEBLOCK)
                        {
                            tetrisScreen.FrontTetris[y + blockMoveCnt][x] = (int)Block.BlockState.STACKBLOCK;
                            tetrisScreen.FrontTetris[y][x] = (int)Block.BlockState.NONBLOCK;
                        }                        
                    }                                       
                }
                blockNumCnt = 0;
            }
        }

        // 블록으로 가득찬 라인이 있는지 체크
        public bool blockFillLineCheck()
        {                        
            int blockNumCnt = 0; // 해당라인에 블록이 몇개가 있는지

            // 벽을 제외한 보드의 맨 아래층 부터 해당 라인이 블록으로 모두 채워져 있는지 확인 
            for (int y = tetrisScreen.tetrisBoardGetY - 2; y > -1; y--)
            {
                for (int x = 1; x < tetrisScreen.tetrisBoardGetX - 1; x++)
                {
                    if (tetrisScreen.FrontTetris[y][x] == (int)Block.BlockState.STACKBLOCK || tetrisScreen.FrontTetris[y][x] == (int)Block.BlockState.MOVEBLOCK) {
                        blockNumCnt++;
                    }
                }
                
                // 블록으로 채워져 있는 라인이 하나라도 있는 경우
                if (blockNumCnt == tetrisScreen.tetrisBoardGetX - 2) {                    
                    return true;
                }
                blockNumCnt = 0;
            }

            return false;
        }

        public bool fillBlockToEmptyBlock()
        {
            return true;
        }

    }
}
