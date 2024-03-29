﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
    <블록이 쌓일 때 마다 블록 정보를 저장하는 테트리스 보드>
 */
internal class TetrisDataSaveScreen : TetrisScreen
{
    // 블록이 이동하거나 회전할 때 마다 새로 그리는 보드(부모)
    TetrisScreen tetrisScreen;

    // 블록이 쌓일 때 마다 블록 정보를 저장하는 보드(자식)
    static public List<List<int>> tetrisStackArray;

    public TetrisDataSaveScreen(TetrisScreen tetrisScreen) : base(tetrisScreen.tetrisBoardGetX, tetrisScreen.tetrisBoardGetY)
    {
        /*
                base() 생성자 : 자식 클래스의 생성자에서 부모 클래스의 생성자를 초기화 하는 경우
                부모 클래스가 먼저 생성되야지 상속을 받을 수 있는데 부모 인스턴스를 생성하기 위해서는 
                생성자 호출이 일어나기 때문에 부모 클래스의 생성자를 초기화 시켜 준다. 
        */

        // 1. base() 생성자의 인자값으로 부모 테트리스 보드의 X, Y 좌표를 설정한다. 
        // why? 자식 테트리스 보드(블록이 쌓일 때 마다 블록 정보를 저장하는 보드)를 만들기 위해서는 부모의 테트리스 보드 X축, Y축 길이를 가져와야 한다.

        this.tetrisScreen = tetrisScreen;

        saveBlockMake();
    }

    // 블록을 쌓을 테트리스 공간을 만든다.
    public void saveBlockMake()
    {
        tetrisStackArray = new List<List<int>>();
        for (int i = 0; i < tetrisBoardGetY; i++)
        {
            tetrisStackArray.Add(new List<int>());
            for (int j = 0; j < tetrisBoardGetX; j++)
            {
                tetrisStackArray[i].Add((int)TetrisBlock.NONBLOCK);
            }
        }

        // <벽의 역할을 담당할 블록>
        // 왜 벽을 만들어주지??? 
        // 1. 마지막 테트리스 공간을 벽으로 설정 
        for (int i = 0; i < tetrisStackArray[tetrisStackArray.Count - 1].Count; i++)
        {
            tetrisStackArray[tetrisStackArray.Count - 1][i] = (int)TetrisBlock.WALLBLOCK;
        }
        // 2. 가장 왼쪽과 오른쪽의 공간을 벽으로 설정
        for (int i = 0; i < tetrisStackArray.Count; i++)
        {
            tetrisStackArray[i][0] = (int)TetrisBlock.WALLBLOCK;
            tetrisStackArray[i][tetrisStackArray[tetrisStackArray.Count - 1].Count - 1] = (int)TetrisBlock.WALLBLOCK;
        }
    }

    // 알맹이 보드에 블럭이 위치하는 좌표값을 저장
    public void blockSave(int x, int y, int[,] currentBlockShape)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (currentBlockShape[i, j] == 1)
                {
                    tetrisStackArray[y + i][x + j] = (int)TetrisBlock.MOVEBLOCK;
                }
            }
        }
    }

    // 벽을 제외한 맨 아래쪽 부터 블록이 다 차있는 라인이 있는지 검사 
    public void blockFullCheck() {
        // 한줄에 블록으로 가득 차 있는지 확인 
        int blockXSize = tetrisScreen.tetrisBoardGetX - 2;

        // 가득 차 있는 블록라인을 비어있는 블록라인으로 변경
        for (int i = tetrisStackArray.Count - 2; i >= 0; i--) {
            for (int j = 1; j < tetrisStackArray[i].Count - 1; j++) {
                if (tetrisStackArray[i][j] == (int)TetrisBlock.MOVEBLOCK) {                    
                    blockXSize--; // 블록이 있으면 하나씩 차감                    
                }
            }
            // 0일 경우는 블록이 다 차 있는 라인이므로 비어있는 블록으로 변경
            if (blockXSize == 0) {
                for (int x = 1; x < tetrisStackArray[i].Count - 1; x++) {
                    tetrisStackArray[i][x] = (int)TetrisBlock.NONBLOCK;
                }                                
            }
            blockXSize = tetrisScreen.tetrisBoardGetX - 2;
        }

        int blockLineEmptyCk = 0; // 비어있는 블록라인 개수 
        blockXSize = tetrisScreen.tetrisBoardGetX - 2;        
        for (int a = tetrisStackArray.Count - 2; a >= 0; a--) {
            for (int b = 1; b < tetrisStackArray[a].Count - 1; b++)
            {                
                if (tetrisStackArray[a][b] == (int)TetrisBlock.MOVEBLOCK)
                {
                    int temp1 = a;
                    int temp2 = a;
                    // 비어있는 블록라인 개수만큼 아래로 이동
                    for (int i = 0; i < blockLineEmptyCk; i++) {
                        tetrisStackArray[++temp1][b] = (int)TetrisBlock.MOVEBLOCK;
                    }
                    // 아래로 이동하기전의 블록을 비어있는 블록으로 수정
                    if (blockLineEmptyCk != 0) {
                        tetrisStackArray[temp2][b] = (int)TetrisBlock.NONBLOCK;                        
                    }                    
                }else { 
                    blockXSize--; // 블록이 다 차 있는 라인 체크
                }
            }            
            // 0일 경우는 블록이 다 차 있는 라인이므로 비어있는 블록라인 개수 하나 추가
            if (blockXSize == 0) blockLineEmptyCk++;
            blockXSize = tetrisScreen.tetrisBoardGetX - 2;
        }

    }

}

