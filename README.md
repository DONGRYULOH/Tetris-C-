# C# 테트리스 TODO-LIST

1. 사용자에게 보여질 테트리스 맵(껍데기 보드)
    ![image](https://user-images.githubusercontent.com/53106848/207209414-f70fbfb5-aaf2-4cf1-97a8-3f05ac135f9f.png)
    - [x] 블록이 이동하거나 회전했을 때 보드를 초기화 시키는 용도로 사용
    - [x] 테트리스 보드에서 역할을 담당할 블록종류
        - 비어있는 블록, 벽을 담당하는 블록, 움직이는 블록

2. 테트리스에서 사용될 블록          
    ![image](https://user-images.githubusercontent.com/53106848/207771853-59dffb48-25e7-4210-9fa5-eea3a00ee4a7.png)
    - [x] 7가지의 블록 모양
    - [x] 모양에 따른 방향(반시계 방향으로 90도씩 회전시 총 4개의 방향)
    - [x] 모든 블록의 모양이 들어갈 공간의 최대 가로길이와 최대 세로길이

3. 블록 이동 또는 회전시 충돌감지
    - [x] 왼쪽 또는 오른쪽 벽으로 이동하는 경우
      - [x] 오른쪽으로 이동하고 왼쪽으로 이동시 벽이 없는데도 움직이지 않음   
       ![image](https://user-images.githubusercontent.com/53106848/206368244-6ea5da0c-99b0-4935-a699-4e4f910c55c0.png)
       ![image](https://user-images.githubusercontent.com/53106848/206368269-4ab2fdc9-4ac0-4ab4-9880-8fb375557f94.png)
       ![image](https://user-images.githubusercontent.com/53106848/206368290-d6f6ffa1-18ec-46ee-81eb-427e0af942df.png)  
       **이전 블록의 위치에 대해서 방향을 이동하지 않고 블록이 최초로 떨어지는 위치에서 방향을 이동시키니까 벽이랑 충돌되는 현상이 발생함**
    - [x] 아래쪽 벽으로 이동하는 경우
    - [x] 회전하는 경우    
         ![image](https://user-images.githubusercontent.com/53106848/206642474-700fe035-df3f-4091-b2fb-3b05fa6d981c.png)    
     1. 블록이 위치하고 있는 좌표 
        - X좌표 : 2, Y좌표 : 0
     2. 회전한 블록이 위치하는 공간(배열)
        - [0][3], [1][1], [1][2], [1][3]
     3. 1번과 2번을 더한다. 
        - X좌표는 왼쪽 벽과 오른쪽 벽과 충돌하는지만 검사한다.
        - Y좌표는 맨 아래쪽 벽과 충돌하는지만 검사한다.

4. 블록 이동
    - [X] 시간이 흐를때마다 블록을 한칸씩 아래로 이동 
    - [x] 밑바닥 벽인 경우 블록을 쌓고 새로운 블록을 생성
        - [x] 쌓인 블록 정보를 저장하고 있는 보드 생성(알맹이 보드)
        - [x] 블록이 아래로 더 이상 내려갈수 없는 경우 알맹이 보드에 블록정보를 저장
            - [x] 블록이 쌓이고 새로운 블록을 이동시키면 기존에 쌓였던 블록이 사라지는 현상이 발생함
            ![image](https://user-images.githubusercontent.com/53106848/207199400-c1281303-fef9-42ab-b3de-d7597df17127.png)            
            ```C#
            class 알맹이 보드(자식) : 껍데기 보드(부모){
            
                // 블록이 쌓일 때 마다 블록정보를 저장하는 알맹이 보드(자식)
                static public List<List<int>> tetrisStackArray;                                                
                
                public TetrisDataSaveScreen(TetrisScreen tetrisScreen) : base(tetrisScreen.tetrisBoardGetX, tetrisScreen.tetrisBoardGetY) {
                    this.tetrisStackArray = tetrisScreen.tetristArray; 
                }
            }
            
            class TetrisScreen(껍데기 보드){
                // 껍데기 보드의 블록공간
                List<List<int>> tetrisArray;
            }            
            ```
            블록이 쌓일 때 마다 블록정보를 저장하는 알맹이 보드(자식) 생성시 껍데기 보드가 존재하는 주소를 할당해주면, 
            껍데기 보드의 공간을 초기화 시키면 같은 주소를 참조하고 있는 알맹이 보드의 공간도 초기화된다.
            
            ![image](https://user-images.githubusercontent.com/53106848/207202056-609d01b3-306c-4823-8c02-ec1c53792ad7.png)
            ```C#
            class 알맹이 보드(자식) : 껍데기 보드(부모){
            
                // 블록이 쌓일 때 마다 블록정보를 저장하는 알맹이 보드(자식)
                static public List<List<int>> tetrisStackArray;                                                
                
                public TetrisDataSaveScreen(TetrisScreen tetrisScreen) : base(tetrisScreen.tetrisBoardGetX, tetrisScreen.tetrisBoardGetY) {
                    saveBlockMake();
                }
                
                // 알맹이 보드 블록공간 생성
                public void saveBlockMake() {
                    tetrisStackArray = new List<List<int>>();
                    for (int i = 0; i < tetrisBoardGetY; i++)
                    {
                        tetrisStackArray.Add(new List<int>());
                        for (int j = 0; j < tetrisBoardGetX; j++)
                        {
                            tetrisStackArray[i].Add((int)TetrisBlock.NONBLOCK);
                        }
                    }                    
                }
                
            }
            
            class TetrisScreen(껍데기 보드){
                // 껍데기 보드의 블록공간
                List<List<int>> tetrisArray;
            }            
            ```
            껍데기 보드와 알맹이 보드의 객체를 각각 생성해야지 힙메모리 영역의 서로 다른 주소를 참조하기 때문에 서로 영향을 주지 않는다.
    - [x] 기존에 쌓인 블록과 충돌하면 멈추고 새로운 블록 생성
      ![image](https://user-images.githubusercontent.com/53106848/207268637-77c6604a-7743-4a3a-8463-d0dc3fb029aa.png)
      Y좌표와 X좌표가 기존에 쌓인 블록가 일치하는 경우에만 멈춘다.
      (맨 아래층은 모두 벽이기 때문에 벽인지 아닌지 판단하면 되므로 Y좌표만 검사하면 된다.)            
      ![image](https://user-images.githubusercontent.com/53106848/207489135-01c0bde2-4a0c-40eb-8515-e27af5652ce4.png)
      ![image](https://user-images.githubusercontent.com/53106848/207489221-4de4bc42-39e4-4a69-b723-415e361f413f.png)
      ```C#
        // 알맹이 보드에 쌓여있는 블록의 좌표값을 가져와서 현재 움직이고 있는 블록과 충돌하는지 확인 
        for (int y = 0; y < TetrisDataSaveScreen.tetrisStackArray.Count; y++)
        {
            for (int x = 0; x < TetrisDataSaveScreen.tetrisStackArray[y].Count; x++)
            {
                // 알맹이 보드에 쌓여있는 블록의 좌표값을 모두 가져온다.        
                if (TetrisDataSaveScreen.tetrisStackArray[y][x] == (int)TetrisBlock.MOVEBLOCK) {
                    for (int a = 0; a < blockData.widthMaxLength; a++)
                    {
                        for (int b = 0; b < blockData.heightMaxLength; b++)
                        {                            
                            if (currentBlockShape[a, b] == 1) // 블록이 존재하는 좌표값일 경우만 
                            {
                                // 블록이 더 이상 내려갈수 없으면(벽이 있으면) 자식 테트리스 보드에 블럭을 쌓는다.
                                // 껍데기 보드상의 블록 위치 + 블록의 모양을 저장하는 공간에서 블록이 있는 좌표값을 가져옴 + 아래로 한칸 이동 == 알맹이 보드에 쌓여 있는 블록의 Y좌표값과 일치
                                if (tempY + a + 1 == y)
                                {
                                    if(tempX + b  == x) { 
                                        // 벽이 있으면 블록이 위치하고 있는 좌표값을 알맹이 보드에 저장                                    
                                        tetrisDataSaveScreen.blockSave(tempX, tempY, currentBlockShape);
                                        // 랜덤블록생성 후 블록이 떨어지는 위치값 초기화
                                        randomBlockTypeMake();
                                        return false; // 이동 불가능
                                    }
                                }
                            }
                        }
                    }
                }              
            }
        }
        ```
        움직이고 있는 블록위치의 좌표값 + 블록의 모양을 저장하는 공간에서 블록이 있는 좌표값 + 아래(Y축방향)로 이동하면 이동한 블록의 위치를 테트리스 보드상에 그려줄 수 있다.
    - [ ] 알맹이 보드에 다 차있는 블록라인을 비어있는 블록으로 변경     
       
5. 랜덤으로 생성될 블록

![화면 캡처 2022-12-04 223837](https://user-images.githubusercontent.com/53106848/205493736-b49188fc-19e6-4def-bbfe-08963e8d0dd4.png)
<img width="50%" src="https://user-images.githubusercontent.com/53106848/205493726-d675153b-ee52-45a3-8267-928f43e737eb.jpg" />

# 테트리스 설계
https://gitmind.com/app/docs/fc9uuvve



