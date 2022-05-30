using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controll : MonoBehaviour
{
    private void Start()
    {
        itemCount = 0;
    }
    //입력값을 받아들이는 메서드
    public void GetInput()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");
        m_breakInput = Input.GetAxis("Jump");
    }
    //핸들 조절 관련 메서드
    private void Steer()
    {
        //방향키 입력시 바퀴를 회전시킴
        m_steeringAngle = maxSteerAngle * m_horizontalInput;
        Front_Left_Wheel.steerAngle = m_steeringAngle;
        Front_Right_Wheel.steerAngle = m_steeringAngle;
    }

    private void ControlVelocity()
    {
        bool isbreak = WheelBreak();
        Accelerate(isbreak);

    }
    private bool WheelBreak()
    {  
        Front_Left_Wheel.brakeTorque = m_breakInput * breakForce;
        Front_Right_Wheel.brakeTorque = m_breakInput * breakForce;
        Rear_Left_Wheel.brakeTorque = m_breakInput * breakForce;
        Rear_Right_Wheel.brakeTorque = m_breakInput * breakForce;
        if (m_breakInput > 0.01)
            return true;
        else
            return false;
    }
    //자동차 가속 관련 메서드
    private void Accelerate(bool _isBreak)
    {
        if (_isBreak == false)
        {
            Front_Left_Wheel.motorTorque = m_verticalInput * motorForce + default_speed;
            Front_Right_Wheel.motorTorque = m_verticalInput * motorForce + default_speed;
        }
        else
        {
            Front_Left_Wheel.motorTorque = 0;
            Front_Right_Wheel.motorTorque = 0;
        }

    }
    //자동차 바퀴 브레이크 메서드

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(Front_Left_Wheel, Front_Left_T);
        UpdateWheelPose(Front_Right_Wheel, Front_Right_T);
        UpdateWheelPose(Rear_Left_Wheel, Rear_Left_T);
        UpdateWheelPose(Rear_Right_Wheel, Rear_Right_T);
    }

    //자동차 바퀴 모형을 회전시키는 메서드
    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.tag=="obstacle") //장애물에 부딪히는 경우
        {
            SceneManager.LoadScene(manager.stage);
        }
    }

    private void OnTriggerEnter(Collider other) //check point 도달 관련 함수 
    {
        if (other.tag == "Item")
        {
            itemCount++;
            
            other.gameObject.SetActive(false);  //체크 포인트 도달 시 해당 포인트 비활성화 
        }
        else if (other.tag == "finish")
        {
            if (itemCount == manager.TotalItemCount)
            {
                //모든 체크포인트 도달시 클리어
                SceneManager.LoadScene(manager.stage);
            }
            else
            {
                SceneManager.LoadScene(manager.stage);
            }
        }
    }

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        ControlVelocity();
        UpdateWheelPoses();
    }

    //입력 키보드 관련 변수
    private float m_horizontalInput; 
    private float m_verticalInput;
    private float m_steeringAngle;
    private float m_breakInput; 
    
    //게임 관련 변수
    int itemCount;
    public GameManager manager;


    //바퀴 충돌, 모형 관련 변수
    public WheelCollider Front_Left_Wheel, Front_Right_Wheel;
    public WheelCollider Rear_Left_Wheel, Rear_Right_Wheel;
    public Transform Front_Left_T, Front_Right_T;
    public Transform Rear_Left_T, Rear_Right_T;
    
    //바퀴에 전해지는 힘 관련 변수
    public float maxSteerAngle = 30;
    public float motorForce = 100;
    public float breakForce = 1000;
    public float default_speed = 50;
}
