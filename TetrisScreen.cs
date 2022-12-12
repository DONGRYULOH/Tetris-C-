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

    // 블록에 대한 정보
    Block block;

    public List<List<int>> TetristArray
    {
        get { return tetrisArray; }
    }    

    // 테트리스 보드의 X, Y축 
    public int tetrisBoardGetX
    { 
        get { return tetrisArray[0].Count; }    
    }
    public int tetrisBoardGetY
    {
        get { return tetrisArray.Count; }
    }

    // 블록에 대한 정보를 가져온다.
    public void getBlockInfo(Block block) { 
        this.block = block;
    }

    // 블럭이 이동하기 전에 테트리스 판을 초기화 시킨다.
    public void tetrisBoardInit()
    {
        // 1.벽을 제외한 껍데기 보드의 모든 공간을 빈 블럭으로 초기화
        for (int y = 0; y < tetrisArray.Count; y++)
        {
            for (int x = 0; x < tetrisArray[y].Count; x++)
            {                
               tetrisArray[y][x] = (int)TetrisBlock.NONBLOCK;                
            }
        }        
        // 마지막 테트리스 공간을 벽으로 설정 
        for (int i = 0; i < tetrisArray[tetrisArray.Count - 1].Count; i++)
        {
            tetrisArray[tetrisArray.Count - 1][i] = (int)TetrisBlock.WALLBLOCK;
        }
        // 가장 왼쪽과 오른쪽의 공간을 벽으로 설정
        for (int i = 0; i < tetrisArray.Count; i++)
        {
            tetrisArray[i][0] = (int)TetrisBlock.WALLBLOCK;
            tetrisArray[i][tetrisArray[tetrisArray.Count - 1].Count - 1] = (int)TetrisBlock.WALLBLOCK;
        }

        // 2. 알맹이 보드에 쌓인 블럭을 껍데기 보드에 그려줌
        for (int y = 0; y < TetrisDataSaveScreen.tetrisStackArray.Count; y++)
        {
            for (int x = 0; x < TetrisDataSaveScreen.tetrisStackArray[y].Count; x++)
            {
                if (TetrisDataSaveScreen.tetrisStackArray[y][x] == (int)TetrisBlock.MOVEBLOCK)
                {
                    tetrisArray[y][x] = TetrisDataSaveScreen.tetrisStackArray[y][x];
                }
            }
        }

        // 알맹이 보드에 쌓인 블록의 데이터가 어디선가 초기화 되고 있음
        Console.WriteLine(TetrisDataSaveScreen.tetrisStackArray); 

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
        // 1. 마지막 테트리스 공간을 벽으로 설정 
        for (int i = 0; i < tetrisArray[tetrisArray.Count - 1].Count; i++)
        {
            tetrisArray[tetrisArray.Count - 1][i] = (int)TetrisBlock.WALLBLOCK;
        }
        // 2. 가장 왼쪽과 오른쪽의 공간을 벽으로 설정
        for (int i = 0; i < tetrisArray.Count; i++)
        {
            tetrisArray[i][0] = (int)TetrisBlock.WALLBLOCK;
            tetrisArray[i][tetrisArray[tetrisArray.Count - 1].Count - 1] = (int)TetrisBlock.WALLBLOCK;
        }        
    }

    // 테트리스 판을 화면에 그려주기 
    public virtual void TetrisRender()
    {
        for (int y = 0; y < tetrisArray.Count; y++)
        {
            for (int x = 0; x < tetrisArray[y].Count; x++)
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
