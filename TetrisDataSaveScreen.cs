using System;
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
    List<List<int>> tetrisStackArray;    

    public TetrisDataSaveScreen(TetrisScreen tetrisScreen) : base(tetrisScreen.tetrisBoardGetX, tetrisScreen.tetrisBoardGetY) {
        /*
                base() 생성자 : 자식 클래스의 생성자에서 부모 클래스의 생성자를 초기화 하는 경우
                부모 클래스가 먼저 생성되야지 상속을 받을 수 있는데 부모 인스턴스를 생성하기 위해서는 
                생성자 호출이 일어나기 때문에 부모 클래스의 생성자를 초기화 시켜 준다. 
        */

        // 1. base() 생성자의 인자값으로 부모 테트리스 보드의 X, Y 좌표를 설정한다. 
        // why? 자식 테트리스 보드(블록이 쌓일 때 마다 블록 정보를 저장하는 보드)를 만들기 위해서는 부모의 테트리스 보드 X축, Y축 길이를 가져와야 한다.

        this.tetrisScreen = tetrisScreen;
        this.tetrisStackArray = tetrisScreen.TetristArray;        
    }

    // 자식 보드에 쌓인 블록을 부모 보드에 쌓는다.
    public override void TetrisRender() {
        // 1. 블록에 대한 정보 필요 why? 어떤 블록을 쌓을지 알아야 되므로 

        // 2. 쌓인 블록을 가져와서 부모 테트리스 보드에 그려줌 
        for (int y = 0; y < tetrisStackArray.Count; y++)
        {
            for (int x = 0; x < tetrisStackArray[y].Count; x++)
            {
                this.tetrisScreen.TetristArray[y][x] = tetrisStackArray[y][x];
            }            
        }
    }

    // 알맹이 보드에 블럭이 위치하는 좌표값을 저장
    public void blockSave(int x, int y, int[,] currentBlockShape) {
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

}

