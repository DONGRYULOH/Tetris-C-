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
    public enum RotateDirection
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

    // 이동하고 있는 블록 종류
    public List<int[,]> currentMoveBlock;

    // 이동하고 있는 블록의 위치값
    private int movePositionX;
    private int movePositionY;

    // 이동하고 있는 블록 회전방향        
    private RotateDirection currentBlockRotate;

    // 모든 블록의 모양
    BlockShape blockShape;

    public Block(BlockShape blockShape)
    {
        this.blockShape = blockShape;                
        randomBlockPick(); // 7개의 블록중에서 이동할 블록을 랜덤으로 뽑기 
    }

    public int MovePositionX
    {
        get { return movePositionX; }
        set { this.movePositionX = value; }
    }

    public int MovePositionY
    {
        get { return movePositionY; }
        set { this.movePositionY = value; }
    }

    public RotateDirection CurrentBlockRotate
    {
        get { return currentBlockRotate; }
        set { this.currentBlockRotate = value; }
    }

    // 랜덤 블록 뽑기
    public void randomBlockPick() {
        BlockInit(); // 떨어지는 블록의 시작 좌표값 초기화 

        Random randomBlock = new Random();

        int randomBlockType = randomBlock.Next((int)BlockType.BT_I, (int)BlockType.BT_T); // 0부터 6 사이의 랜덤 블록 종류 뽑기
        
        currentMoveBlock = this.blockShape.BlockTypeDatas[randomBlockType];
    }

    // 블록 초기화
    public void BlockInit() {
        this.movePositionY = 0;
        this.movePositionX = 1;
        currentBlockRotate = (int)RotateDirection.RA;             
    }

    // 블록 회전
    public void RotateBlock()
    {

    }
    

}

