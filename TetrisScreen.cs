using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum TetrisBlock
{
    NONBLOCK = 0,  // 테트리스 판에서 비어있는 블록 
    WALLBLOCK = 1, // 테트리스 판에서 벽의 역할을 하는 담당
    MOVEBLOCK = 2  // 테트리스 판에서 움직이는 블록        
}

class TetrisScreen
{
    // 움직인 테트리스 블럭이 보여지는 공간
    List<List<int>> tetrisArray;

    // 테트리스 블럭이 쌓이는 공간 
    List<List<int>> tetrisStackArray;

    // 테트리스 보드를 새롭게 렌더링 할껀지 아닌지 체크
    public static int removeCheck = 0;

    public List<List<int>> TetristArray
    {
        get { return tetrisArray; }
    }

    // 테트리스 판의 해당 위치에 블럭 세팅
    public void setBlock(int y, int x, TetrisBlock blockType)
    {
        tetrisArray[y][x] = (int)blockType;
    }

    // 블럭이 이동하고 나서 테트리스 판을 다시 그려준다 
    public void blockMoveRender()
    {
        for (int y = 0; y < tetrisArray.Count; y++)
        {
            for (int x = 0; x < tetrisArray[x].Count; x++)
            {
                tetrisArray[y][x] = (int)TetrisBlock.NONBLOCK;
            }
        }

        // <벽의 역할을 담당할 블록>
        // 마지막 테트리스 공간을 쌓여있는 블록으로 설정
        for (int i = 0; i < tetrisArray[tetrisArray.Count - 1].Count; i++)
        {
            tetrisArray[tetrisArray.Count - 1][i] = (int)TetrisBlock.WALLBLOCK;
        }
    }

    // x * y 공간의 테트리스 판 만들기 
    public TetrisScreen(int x, int y)
    {
        tetrisArray = new List<List<int>>();
        for (int i = 0; i < y; i++)
        {
            tetrisArray.Add(new List<int>());
            for (int j = 0; j < x; j++)
            {
                tetrisArray[i].Add((int)TetrisBlock.NONBLOCK);
            }
        }

        // <벽의 역할을 담당할 블록>
        // 마지막 테트리스 공간을 쌓여있는 블록으로 설정        
        for (int i = 0; i < tetrisArray[tetrisArray.Count - 1].Count; i++)
        {
            tetrisArray[tetrisArray.Count - 1][i] = (int)TetrisBlock.WALLBLOCK;
        }
    }

    // 테트리스 판을 화면에 그려주기 
    public void TetrisRender()
    {
        for (int y = 0; y < tetrisArray.Count; y++)
        {
            for (int x = 0; x < tetrisArray[x].Count; x++)
            {
                switch (tetrisArray[y][x])
                {
                    case (int)TetrisBlock.NONBLOCK:
                        Console.Write("□");
                        break;
                    case (int)TetrisBlock.WALLBLOCK:
                        Console.Write("■");
                        break;
                    case (int)TetrisBlock.MOVEBLOCK:
                        Console.Write("▣");
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine("");
        }
    }
}
