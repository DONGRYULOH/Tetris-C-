using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace Tetris
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1.테트리스 판 생성		
            Console.Write("테트리스 판의 x축 길이를 입력해주세요 : ");
            int x = int.Parse(Console.ReadLine());
            Console.Write("테트리스 판의 y축 길이를 입력해주세요 : ");
            int y = int.Parse(Console.ReadLine());
            TetrisScreen tetrisScreen = new TetrisScreen(x , y); // 화면에 보여질 테트리스 보드            

            // 2.테트리스 블록 생성
            BlockShape blockShape = new BlockShape(); // 모든 블록의 종류별 모양 생성 
            Block block = new Block(blockShape);

            // 3.테트리스 관리 
            TetrisManagement tetrisManagement = new TetrisManagement(tetrisScreen, block);

            // 4. 테트리스 시작
            tetrisManagement.startTetris();            
        }
    }
}

