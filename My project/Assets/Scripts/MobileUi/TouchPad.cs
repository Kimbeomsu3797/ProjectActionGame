using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchPad : MonoBehaviour
{
    //_touchPad ������Ʈ�� ����
    private RectTransform _touchPad;
    //��ġ �Է� �߿� ���� ��Ʈ�ѷ��� ���� �ȿ� �ִ� �Է��� �����ϱ� ���� ���̵�
    private int _touchId = -1;
    //�Է��� ���۵Ǵ� ��ǥ 
    private Vector3 _startPos = Vector3.zero;
    //���� ��Ʈ�ѷ��� ������ �����̴� ������ 
    public float _dragRadius = 60f;
    //�÷��̾��� �������� �����ϴ� PlayerMovement ��ũ��Ʈ�� ����
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
        //��ġ���̵� �ű�� ���� ��ȣ
        int i = 0;
        //��ġ �Է��� �ѹ��� ���� ���� ���ü��� ������ ��ġ�� �ϳ� �̻� �ԷµǸ� ����
        if(Input.touchCount > 0)
        {
            //������ ��ġ �Է��� �ϳ��� ��ȸ
            foreach(Touch touch in Input.touches)
            {
                //��ġ���̵� �ű�� ���� ��ȣ�� 1 ����
                i++;
                //���� ��ġ �Է��� x,y��ǥ�� ���ϱ�
                Vector3 touchPos = new Vector3(touch.position.x, touch.position.y);
                //��ġ �Է��� ��� ���۵Ǿ��ٸ�(TouchPhase.Began�̶��)
                if(touch.phase == TouchPhase.Began)
                {
                    //��ġ�� ��ǥ�� ���� ����Ű ���� ���� �ִٸ�
                    if(touch.position.x <= (_startPos.x + _dragRadius))
                    {
                        //�� ��ġ ���̵� �������� ���� ��Ʈ�ѷ��� �����ϵ��� �Ѵ�
                        _touchId = i;
                    }
                }
                //��ġ �Է��� �������ٴ���, ������ �ִٸ�
                if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    //��ġ ���̵�� ������ ��쿡��
                    if(_touchId == i)
                    {
                        //��ǥ �Է��� �޴´�.
                        HandleInput(touchPos);
                    }
                }
            }
        }
    }

    private void HandleInput(Vector3 input)
    {
        //��ư�� ������ ��Ȳ�̶��,
        if (_buttonPressed)
        {
            //���� ��Ʈ�ѷ��� ���� ��ǥ�κ��� �Է¹��� ��ǥ�� �󸶳� ������ �ִ��� ���Ѵ�.
            Vector3 diffVector = (input - _startPos);
            //�̷� ������ ���� ��ǥ�� �Ÿ��� ���ϰ� ���� �ִ�ġ���� ũ�ٸ�
            if(diffVector.sqrMagnitude > _dragRadius * _dragRadius)
            {
                //���⺤���� �Ÿ��� 1�� �����.(��������, ����ȭ)
                diffVector.Normalize();
                //�׸��� ���� ��Ʈ�ѷ��� �ִ�ġ��ŭ�� �����̰� ��
                _touchPad.position = _startPos + diffVector * _dragRadius;
            }
            else
            {
                //���� �Է� ��ǥ�� ����Ű�� �̵� ��Ŵ
                _touchPad.position = input;
            }
        }
        else
        {
            //��ư���� pressout�Ǹ� ����Ű�� ������ġ�� ����
            _touchPad.position = _startPos;
        }
        //����Ű�� ���� ������ ���̸� ����
        Vector3 diff = _touchPad.position - _startPos;
        //����Ű�� ������ ������ ä��, �Ÿ��� ������ ���⸸ ����
        Vector2 normDiff = new Vector3(diff.x / _dragRadius, diff.y / _dragRadius);

        if(_player != null)
        {
            //�÷��̾ ����Ǿ� ������, �÷��̾�� ����� ��ǥ�� ����
            _player.OnStickChanged(normDiff);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
