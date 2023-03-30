using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class TetrisScreen
{
    
    /* 멤버변수 */    

    // 화면상에 보여지는 보드
    List<List<int>> frontTetrisScreen;


    /* 생성자 함수 */

    //  y * x 공간의 테트리스 판 만들기 
    public TetrisScreen(int x, int y)
    {
        frontTetrisScreen = new List<List<int>>();
        for (int i = 0; i < y; i++)
        {
            frontTetrisScreen.Add(new List<int>());
            for (int j = 0; j < x; j++)
            {
                frontTetrisScreen[i].Add((int)Block.BlockState.NONBLOCK);
            }
        }

        createTetrisWall(frontTetrisScreen);
        // createDeadLine(frontTetrisScreen);
    }

    /* getter, setter */

    public List<List<int>> FrontTetris
    {
        get { return frontTetrisScreen; }
    }

    // 테트리스 보드의 X, Y축 
    public int tetrisBoardGetX
    {
        get { return frontTetrisScreen[0].Count; }
    }
    public int tetrisBoardGetY
    {
        get { return frontTetrisScreen.Count; }
    }


    /* 메서드 */

    // 테트리스의 벽을 생성 
    public void createTetrisWall(List<List<int>> frontTetrisScreen) {
        // <벽의 역할을 담당할 블록>        
        // 1. 마지막 테트리스 공간을 벽으로 설정 
        for (int i = 0; i < frontTetrisScreen[frontTetrisScreen.Count - 1].Count; i++)
        {
            frontTetrisScreen[frontTetrisScreen.Count - 1][i] = (int)Block.BlockState.WALLBLOCK;
        }
        // 2. 가장 왼쪽과 오른쪽의 공간을 벽으로 설정
        for (int i = 0; i < frontTetrisScreen.Count; i++)
        {
            frontTetrisScreen[i][0] = (int)Block.BlockState.WALLBLOCK;
            frontTetrisScreen[i][frontTetrisScreen[frontTetrisScreen.Count - 1].Count - 1] = (int)Block.BlockState.WALLBLOCK;
        }
    }

    // 테트리스 보드의 맨 위쪽(데드라인 블록) 생성 
    /*public void createDeadLine(List<List<int>> frontTetrisScreen) {
        for (int x = 1; x < tetrisBoardGetX - 1; x++)
        {
            frontTetrisScreen[0][x] = (int)Block.BlockState.DEADBLOCK;            
        }
    }*/
    

    // 테트리스 보드를 콘솔에 출력
    public virtual void TetrisRender()
    {
        for (int y = 1; y < frontTetrisScreen.Count; y++)
        {
            for (int x = 0; x < frontTetrisScreen[y].Count; x++)
            {
                switch (frontTetrisScreen[y][x])
                {
                    case (int)Block.BlockState.NONBLOCK:
                        Console.Write("□");
                        break;
                    case (int)Block.BlockState.WALLBLOCK:
                        Console.Write("■");
                        break;
                    case (int)Block.BlockState.MOVEBLOCK:
                        Console.Write("▣");
                        break;
                    case (int)Block.BlockState.STACKBLOCK:
                        Console.Write("▩");
                        break;                    
                    default:
                        break;
                }
            }
            Console.WriteLine("");
        }
    }
    
}
