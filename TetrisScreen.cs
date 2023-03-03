using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// 테트리스 보드에서 역할을 담당할 블록종류
enum TetrisBlock
{
    NONBLOCK = 0,  // 테트리스 판에서 비어있는 블록 
    WALLBLOCK = 1, // 테트리스 판에서 벽의 역할을 하는 담당
    MOVEBLOCK = 2  // 테트리스 판에서 움직이는 블록, 쌓여있는 블록      
}

class TetrisScreen
{
    /* 멤버변수 */

    // 화면상에 보여지는 보드
    List<List<int>> frontTetrisScreen;

    // 쌓인 블록 정보를 저장하는 보드
    List<List<int>> backTetrisScreen;


    /* 생성자 함수 */

    // x * y 공간의 테트리스 판 만들기 
    public TetrisScreen(int x, int y)
    {
        frontTetrisScreen = new List<List<int>>();
        for (int i = 0; i < y; i++)
        {
            frontTetrisScreen.Add(new List<int>());
            for (int j = 0; j < x; j++)
            {
                frontTetrisScreen[i].Add((int)TetrisBlock.NONBLOCK);
            }
        }

        backTetrisScreen = new List<List<int>>();
        for (int i = 0; i < y; i++)
        {
            backTetrisScreen.Add(new List<int>());
            for (int j = 0; j < x; j++)
            {
                backTetrisScreen[i].Add((int)TetrisBlock.NONBLOCK);
            }
        }

        createTetrisWall(frontTetrisScreen);
        createTetrisWall(backTetrisScreen);
    }

    /* getter, setter */

    public List<List<int>> TetristArray
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
            frontTetrisScreen[frontTetrisScreen.Count - 1][i] = (int)TetrisBlock.WALLBLOCK;
        }
        // 2. 가장 왼쪽과 오른쪽의 공간을 벽으로 설정
        for (int i = 0; i < frontTetrisScreen.Count; i++)
        {
            frontTetrisScreen[i][0] = (int)TetrisBlock.WALLBLOCK;
            frontTetrisScreen[i][frontTetrisScreen[frontTetrisScreen.Count - 1].Count - 1] = (int)TetrisBlock.WALLBLOCK;
        }
    }



    // 블럭이 이동하기 전에 테트리스 판을 초기화 시킨다.
    public void tetrisBoardInit()
    {
        // 1.벽을 제외한 껍데기 보드의 모든 공간을 빈 블럭으로 초기화
        for (int y = 0; y < frontTetrisScreen.Count; y++)
        {
            for (int x = 0; x < frontTetrisScreen[y].Count; x++)
            {                
               frontTetrisScreen[y][x] = (int)TetrisBlock.NONBLOCK;                
            }
        }

        createTetrisWall(frontTetrisScreen);

        // 2. 알맹이 보드에 쌓인 블럭을 껍데기 보드에 그려줌
        // static 클래스도 아니고 상속받지 않았는데 어떻게 TetrisDataSaveScreen에 접근할수 있는지??
        for (int y = 0; y < TetrisDataSaveScreen.tetrisStackArray.Count; y++)
        {
            for (int x = 0; x < TetrisDataSaveScreen.tetrisStackArray[y].Count; x++)
            {
                if (TetrisDataSaveScreen.tetrisStackArray[y][x] == (int)TetrisBlock.MOVEBLOCK)
                {
                    frontTetrisScreen[y][x] = TetrisDataSaveScreen.tetrisStackArray[y][x];
                }
            }
        }        

    }

    

    // 테트리스 블록을 보드에 그리기
    public virtual void TetrisRender()
    {
        for (int y = 0; y < frontTetrisScreen.Count; y++)
        {
            for (int x = 0; x < frontTetrisScreen[y].Count; x++)
            {
                switch (frontTetrisScreen[y][x])
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

    // 벽을 제외한 맨 아래쪽 부터 블록이 다 차있는 라인이 있는지 검사 
    public void blockFullCheck()
    {
        int blockCount = tetrisBoardGetX - 2; // 한 라인이 모두 블록으로 가득 차 있는지 체크
        int downCount = 0; // 블록이 아래로 이동할 횟수 
        bool blockLineFullChk = false; // 블록으로 가득 찬 라인이 하나라도 있는지 체크

        // 가득 차 있는 블록라인을 비어있는 블록라인으로 변경

        // 1. 해당 라인이 모두 블록으로 차 있는지 확인 
        // 2. 블록으로 모두 차 있으면 해당 라인을 비어있는 블록으로 변경 후 이동횟수 1증가(누적 1) 
        // 3. 그 다음 블록라인이 모두 블록으로 차 있는지 확인
        // 4. 블록으로 차있지 않은 경우 이동횟수 만큼 아래로 이동 
        // 5. 이동 하기전 블록 라인에 있는 모든 블록을 비어있는 블록으로 변경 
        // 6. 그 다음 블록라인이 꽉차 있는지 체크 
        // 7. 블록으로 모두 차 있으면 비어있는 블록으로 변경 후 이동횟수 1증가(누적 2)
        // 8. 그 다음 블록라인이 꽉차 있는지 체크
        // 9. 블록으로 차있지 않은 경우 이동횟수 만큼 아래로 이동
        // 10. 이동 하기전 블록 라인에 있는 모든 블록을 비어있는 블록으로 변경
        // 11. 맨위의 블록라인까지 1~10번 과정을 수행 
        for (int i = tetrisBoardGetY - 2; i >= 0; i--)
        {
            for (int j = 1; j < tetrisBoardGetX - 1; j++)
            {
                if (TetristArray[i][j] == (int)TetrisBlock.MOVEBLOCK)
                {
                    blockCount--; // 블록이 있으면 하나씩 차감                    
                }
            }
            // 블록으로 가득차있는 라인
            if (blockCount == 0)
            {
                // 해당라인을 비어있는 블록으로 변경 
                for (int a = 1; a < tetrisBoardGetX - 1; a++)
                {
                    frontTetrisScreen[i][a] == (int)TetrisBlock.NONBLOCK;

                }
                
            }
            blockCount = tetrisBoardGetX - 2;
        }

        // 블록으로 가득찬 라인이 하나도 없는 경우 
        if (blockLineFullChk == false) { 
            
        }

    }
}
