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
 */


namespace Tetris
{    
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1.테트리스 판 생성
            TetrisScreen tetrisScreen = new TetrisScreen(10,15);            
            tetrisScreen.TetrisRender();

            // 2.테트리스 블록 생성
            BlockData blockDataInfo = new BlockData();
            Block block = new Block(tetrisScreen, blockDataInfo);

            while (true)
            {
                Thread.Sleep(1000);
                block.moveBlock();
                Console.Clear();
                tetrisScreen.TetrisRender();
            }

            /*Thread.Sleep(1000);            
            Console.Clear();
            block.moveBlock();
            tetrisScreen.TetrisRender();*/

        }
    }
}
