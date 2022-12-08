# C# 테트리스 만들기

1. 테트리스 맵(보드)
- 비어있는 보드가 필요
- [x] 벽의 역할을 담당할 블록

2. 테트리스에서 사용될 블록  
    - [x] 총 4개의 블록조합으로 구성    
    - [x] 7가지의 블록 모양
    - [x] 모양에 따른 방향
    - [x] 블록을 보드에 그린다.  

3. 블록 이동 또는 회전시 충돌감지
    - [X] 왼쪽 또는 오른쪽 벽으로 이동하는 경우
      - [x] 오른쪽으로 이동하고 왼쪽으로 이동시 벽이 없는데도 움직이지 않음   
       ![image](https://user-images.githubusercontent.com/53106848/206368244-6ea5da0c-99b0-4935-a699-4e4f910c55c0.png)
       ![image](https://user-images.githubusercontent.com/53106848/206368269-4ab2fdc9-4ac0-4ab4-9880-8fb375557f94.png)
       ![image](https://user-images.githubusercontent.com/53106848/206368290-d6f6ffa1-18ec-46ee-81eb-427e0af942df.png)  
       **이전 블록의 위치에 대해서 방향을 이동하지 않고 블록이 최초로 떨어지는 위치에서 방향을 이동시키니까 벽이랑 충돌되는 현상이 발생함**
    - [X] 아래쪽 벽으로 이동하는 경우
    - [ ] 회전하는 경우

4. 블록 이동
    - [X] 시간이 흐를때마다 블록을 한칸씩 아래로 이동 

5. 랜덤으로 생성될 블록

![화면 캡처 2022-12-04 223837](https://user-images.githubusercontent.com/53106848/205493736-b49188fc-19e6-4def-bbfe-08963e8d0dd4.png)
<img width="50%" src="https://user-images.githubusercontent.com/53106848/205493726-d675153b-ee52-45a3-8267-928f43e737eb.jpg" />



