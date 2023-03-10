using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tetris;



partial class Block 
{
    // 블록의 회전방향
    enum RotateDirection
    {
        RA = 0,
        RB = 1,
        RC = 2,
        RD = 3
    }

    // 블록의 상태값
    public enum BlockState
    {
        NONBLOCK = 0,   // 비어있는 블록 
        WALLBLOCK = 1,  // 벽의 역할을 하는 블록
        MOVEBLOCK = 2,  // 움직이는 블록
        STACKBLOCK = 3  // 쌓여있는 블록
    }

    // 이동하고 있는 블록
    public int[,] currentMoveBlock;

    // 이동하고 있는 블록의 위치값
    public int movePositionX;
    public int movePositionY;

    // 이동하고 있는 블록 회전방향    
    RotateDirection currentBlockRotate = (int)RotateDirection.RA;

    public Block(BlockShape blockShape)
    {        
        this.movePositionY = 0;
        this.movePositionX = 1;

        randomBlockPick(blockShape); // 7개의 블록중에서 이동할 블록을 랜덤으로 뽑기 
    }

    // 랜덤 블록 뽑기
    public void randomBlockPick(BlockShape blockShape) {
        Random randomBlock = new Random();

        int randomBlockType = randomBlock.Next((int)BlockType.BT_I, (int)BlockType.BT_T); // 0부터 6 사이의 랜덤 블록 종류 뽑기                 
        
        currentMoveBlock = blockShape.BlockTypeDatas[randomBlockType]; // 이동할 블록의 모양을 가져옴

    }

    // 블록 회전
    public void RotateBlock()
    {

    }

    // 블록 이동 
    public void MoveBlock(ConsoleKey inputKey)
    {

        // 이동하기 전 블록을 비어있는 블록으로 변경
        for (int blockY = 0; blockY < blockData.BlockHeightLength; blockY++)
        {
            for (int blockX = 0; blockX < blockData.BlockWidthLength; blockX++)
            {
                if (currentMoveBlock[blockY, blockX] == 1)
                {
                    tetrisScreen.FrontTetris[blockY + this.y][blockX + this.x] = (int)BlockState.NONBLOCK;
                }
            }
        }

        // 방향키에 해당되는 방향만큼 이동중인 블록의 위치값을 변경
        if (inputKey == ConsoleKey.DownArrow)
        {
            movePositionY++;
        }
        else if (inputKey == ConsoleKey.LeftArrow)
        {
            movePositionX--;
        }
        else if (inputKey == ConsoleKey.RightArrow)
        {
            movePositionX++;
        }
        else if (inputKey == ConsoleKey.UpArrow)
        {
            currentBlockRotate++;
            if ((int)currentBlockRotate == 4) currentBlockRotate = 0;
        }

        // 보드에 이동한 블록을 그려준다.
        for (int blockY = 0; blockY < blockData.BlockHeightLength; blockY++)
        {
            for (int blockX = 0; blockX < blockData.BlockWidthLength; blockX++)
            {
                if (currentBlockShape[blockY, blockX] == 1)
                {
                    tetrisScreen.FrontTetris[blockY + this.y][blockX + this.x] = (int)BlockState.MOVEBLOCK;
                }
            }
        }

    }

}

