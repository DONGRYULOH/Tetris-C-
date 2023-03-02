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
            Console.Write("테트리스 판의 x축 길이를 입력해주세요 : ");
            int x = int.Parse(Console.ReadLine());
            Console.Write("테트리스 판의 y축 길이를 입력해주세요 : ");
            int y = int.Parse(Console.ReadLine());

            /*
                블럭 저장용 테트리스 보드가 필요한 이유는?
                블록이 움직일때 마다 보드를 초기화하고 움직인 만큼 보드에 그려주는데
                보드를 초기화 하고 기존에 쌓여있던 블록을 보드에 넣어줘야 되는 과정에서 
                초기화를 하게 되면 쌓여있던 블록의 데이터가 날라가니까 쌓여있는 블록이 저장되어 있는 보드가 필요                   
            */
            TetrisScreen tetrisScreen = new TetrisScreen(x , y); // 화면에 보여질 테트리스 보드            
            tetrisScreen.TetrisRender();                       

            // 2.테트리스 블록 생성
            BlockData blockDataInfo = new BlockData(); // 모든 블록의 종류별 모양 생성 
            Block block = new Block(tetrisScreen, blockDataInfo);             
           
            // 3. 테트리스 시작
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

