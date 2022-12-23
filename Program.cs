using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

/*
     1. 테트리스 판
     - 2차원 배열로 테트리스 판 만들기 
     - 테트리스 판을 화면에 그려주는 함수 만들기 
     
    2. 테트리스에서 사용될 블록 

    3. 블록이 쌓이는 공간 
    3.1 어딘가에 블록이 쌓이는 공간이 필요함 
    3.2 블록은 어떻게 쌓이는가? 
    - 블록이 이동하려고 하는 위치에 벽이 있으면 이동하기 바로전의 위치에 블록을 고정시킨다. 
      
 */

namespace Tetris
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1.테트리스 판 생성					            
            TetrisScreen tetrisScreen = new TetrisScreen(10, 15); //  테트리스 보드
            TetrisDataSaveScreen saveTetrisScreen = new TetrisDataSaveScreen(tetrisScreen); // 블럭 저장용 테트리스
            tetrisScreen.TetrisRender();

            // 2.테트리스 블록 생성
            BlockData blockDataInfo = new BlockData(4, 4);
            Block block = new Block(tetrisScreen, blockDataInfo, saveTetrisScreen);
            tetrisScreen.getBlockInfo(block);

            while (true)
            {
                Thread.Sleep(300); 
                block.moveBlock();
                Console.Clear();
                tetrisScreen.TetrisRender();                
            }

        }
    }
}

