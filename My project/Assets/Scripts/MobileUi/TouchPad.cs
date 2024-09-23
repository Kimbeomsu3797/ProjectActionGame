using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchPad : MonoBehaviour
{
    //_touchPad 오브젝트를 연결
    private RectTransform _touchPad;
    //터치 입력 중에 방향 컨트롤러의 영역 안에 있는 입력을 구분하기 위한 아이디
    private int _touchId = -1;
    //입력이 시작되는 좌표 
    private Vector3 _startPos = Vector3.zero;
    //방향 컨트롤러가 원으로 움직이는 반지름 
    public float _dragRadius = 60f;
    //플레이어의 움직임을 관리하는 PlayerMovement 스크립트와 연결
    public PlayerMovement _player;
    
    private bool _buttonPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        _touchPad = GetComponent<RectTransform>();
        _startPos = _touchPad.position;
    }
    public void ButtonDown()
    {
        _buttonPressed = true;
    }
    public void ButtonUp()
    {
        _buttonPressed = false;
        HandleInput(_startPos);
    }
    public void FixedUpdate()
    {
        HandleTouchInput();
#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_WEBPLAYER
        HandleInput(Input.mousePosition);
#endif
    }

    private void HandleTouchInput()
    {
        //터치아이디를 매기기 위한 번호
        int i = 0;
        //터치 입력은 한번에 여러 개가 들어올수도 있으니 터치가 하나 이상 입력되면 실행
        if(Input.touchCount > 0)
        {
            //각각의 터치 입력을 하나씩 조회
            foreach(Touch touch in Input.touches)
            {
                //터치아이디를 매기기 위한 번호를 1 증가
                i++;
                //현재 터치 입력의 x,y좌표를 구하기
                Vector3 touchPos = new Vector3(touch.position.x, touch.position.y);
                //터치 입력이 방금 시작되었다면(TouchPhase.Began이라면)
                if(touch.phase == TouchPhase.Began)
                {
                    //터치의 좌표가 현재 방향키 범위 내에 있다면
                    if(touch.position.x <= (_startPos.x + _dragRadius))
                    {
                        //이 터치 아이디를 기준으로 방향 컨트롤러를 조작하도록 한다
                        _touchId = i;
                    }
                }
                //터치 입력이 움직였다던가, 가만히 있다면
                if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    //터치 아이디로 지정된 경우에만
                    if(_touchId == i)
                    {
                        //좌표 입력을 받는다.
                        HandleInput(touchPos);
                    }
                }
            }
        }
    }

    private void HandleInput(Vector3 input)
    {
        //버튼이 눌러진 상황이라면,
        if (_buttonPressed)
        {
            //방향 컨트롤러의 기준 좌표로부터 입력받은 좌표가 얼마나 떨어져 있는지 구한다.
            Vector3 diffVector = (input - _startPos);
            //이력 지점과 기준 좌표의 거리를 비교하고 만약 최대치보다 크다면
            if(diffVector.sqrMagnitude > _dragRadius * _dragRadius)
            {
                //방향벡터의 거리를 1로 만든다.(단위벡터, 정규화)
                diffVector.Normalize();
                //그리고 방향 컨트롤러의 최대치만큼만 움직이게 함
                _touchPad.position = _startPos + diffVector * _dragRadius;
            }
            else
            {
                //현재 입력 좌표에 방향키를 이동 시킴
                _touchPad.position = input;
            }
        }
        else
        {
            //버튼에서 pressout되면 방향키를 원래위치로 복귀
            _touchPad.position = _startPos;
        }
        //방향키와 기준 지점의 차이를 구함
        Vector3 diff = _touchPad.position - _startPos;
        //방향키의 방향을 유지한 채로, 거리를 나누어 방향만 구함
        Vector2 normDiff = new Vector3(diff.x / _dragRadius, diff.y / _dragRadius);

        if(_player != null)
        {
            //플레이어가 연결되어 있으면, 플레이어에게 변경된 좌표를 전달
            _player.OnStickChanged(normDiff);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
