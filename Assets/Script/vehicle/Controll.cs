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
    //�Է°��� �޾Ƶ��̴� �޼���
    public void GetInput()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");
        m_breakInput = Input.GetAxis("Jump");
    }
    //�ڵ� ���� ���� �޼���
    private void Steer()
    {
        //����Ű �Է½� ������ ȸ����Ŵ
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
    //�ڵ��� ���� ���� �޼���
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
    //�ڵ��� ���� �극��ũ �޼���

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(Front_Left_Wheel, Front_Left_T);
        UpdateWheelPose(Front_Right_Wheel, Front_Right_T);
        UpdateWheelPose(Rear_Left_Wheel, Rear_Left_T);
        UpdateWheelPose(Rear_Right_Wheel, Rear_Right_T);
    }

    //�ڵ��� ���� ������ ȸ����Ű�� �޼���
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
        if (collision.transform.gameObject.tag=="obstacle") //��ֹ��� �ε����� ���
        {
            SceneManager.LoadScene(manager.stage);
        }
    }

    private void OnTriggerEnter(Collider other) //check point ���� ���� �Լ� 
    {
        if (other.tag == "Item")
        {
            itemCount++;
            
            other.gameObject.SetActive(false);  //üũ ����Ʈ ���� �� �ش� ����Ʈ ��Ȱ��ȭ 
        }
        else if (other.tag == "finish")
        {
            if (itemCount == manager.TotalItemCount)
            {
                //��� üũ����Ʈ ���޽� Ŭ����
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

    //�Է� Ű���� ���� ����
    private float m_horizontalInput; 
    private float m_verticalInput;
    private float m_steeringAngle;
    private float m_breakInput; 
    
    //���� ���� ����
    int itemCount;
    public GameManager manager;


    //���� �浹, ���� ���� ����
    public WheelCollider Front_Left_Wheel, Front_Right_Wheel;
    public WheelCollider Rear_Left_Wheel, Rear_Right_Wheel;
    public Transform Front_Left_T, Front_Right_T;
    public Transform Rear_Left_T, Rear_Right_T;
    
    //������ �������� �� ���� ����
    public float maxSteerAngle = 30;
    public float motorForce = 100;
    public float breakForce = 1000;
    public float default_speed = 50;
}
