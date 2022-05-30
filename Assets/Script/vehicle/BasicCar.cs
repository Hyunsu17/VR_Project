using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCar : MonoBehaviour
{
    static class Constants
    {
        public const int d_x = 100;
        public const int d_y = 101;
        public const int d_z = 102;

        public const float road_length = 9f;
        public const float van_length =1.6f;
        public const float bus_length=2.15f;
        public const float truck_length = 2.4f;
        public const float car_length = 1.575f;
    }
    private void Update()
    {
      //  Debug.Log(rb.velocity.magnitude);
    }

    private void Accelerate(int key)
    {
        if((rb.velocity).magnitude > maxSpeed)
        {
            Front_Left_Wheel.motorTorque = 0;
            Front_Right_Wheel.motorTorque = 0;
            
        }
        else if (key == 1) //전면에 자동차가 위치한 경우
        {
            Rear_Left_Wheel.brakeTorque = 100;
            Rear_Right_Wheel.brakeTorque = 100;
            Front_Left_Wheel.motorTorque = -150;
            Front_Right_Wheel.motorTorque = -150;
            

        }
        else
        {
            Rear_Left_Wheel.brakeTorque = 0;
            Rear_Right_Wheel.brakeTorque = 0;
            Front_Left_Wheel.motorTorque = motorForce;
            Front_Right_Wheel.motorTorque = motorForce;
        }
        
    }
    private int FindMainDirection() //자동차의 현재 방향을 찾는 함수
    {
        var velocity = rb.velocity;
        int ret = velocity.x >= velocity.z ? Constants.d_x : Constants.d_z;
        return ret;
    }
    private Collider[] findlayerObject(LayerMask _layerMask, float _safeDistance)
    {
        return Physics.OverlapSphere(transform.position, safeDistance, _layerMask);
    }
    
    private List<GameObject> FindRoad(Collider[] _coll_road)
    {
        List<GameObject> road = new List<GameObject>();
        
        for(int i=0;i < _coll_road.Length; i++)
        {
            if (i == 0 || (_coll_road[i].transform.parent!=null && !road.Contains(_coll_road[i].transform.parent.gameObject)) )
            {
                road.Add(_coll_road[i].transform.parent.gameObject);                
            }
        }
        return road;
    }


    private List<Transform> FindVehicle(Collider[] _coll_vehicle) 
    {
        List<Transform> vehicle = new List<Transform>();

        for (int i = 0; i < _coll_vehicle.Length; i++)
        {
            if (i == 0 || (_coll_vehicle[i].transform.parent != null && !vehicle.Contains(_coll_vehicle[i].transform.parent)))
            {
                vehicle.Add(_coll_vehicle[i].transform.parent);
            }
        }
        return vehicle;
    }

    private void ControlVelocity()//여러 상황에서 자동차의 속도를 조절하는 함수
    {
        Collider[] coll_vehicle = findlayerObject(layerMask,safeDistance);
        Collider[] coll_road = findlayerObject(layerMask2, 6);

        List<GameObject> road = FindRoad(coll_road);
        List<Transform> vehicle = FindVehicle(coll_vehicle);

        var key = 0;

        var onRoad = road[road.Count-1];
        
           

        if (vehicle.Count > 0)
            for(int i=0; i < vehicle.Count ; i++)
            {
                var dir = vehicle[i].position - transform.position; //위치 차이 계산
                var car_z = vehicle[i].position.z; //위치 차이 계산

                if (dir.magnitude == 0) //자기자신 제외
                    { continue; }

                if (FindMainDirection() == Constants.d_x) // x축 방향으로 자동차가 이동 중
                {
                    if (dir.x > 0) //상대 자동차가 자신보다 x축방향으로 전면에 위치
                    {
                        if (car_z >= onRoad.transform.position.z && car_z < onRoad.transform.position.z+ Constants.road_length/2) 
                        {
                            
                            if(transform.position.z >= onRoad.transform.position.z && transform.position.z < onRoad.transform.position.z + Constants.road_length/2)
                            {
                                key = 1;
                                break;
                            }
                            else
                            {

                            }
                        }
                        else if (car_z < onRoad.transform.position.z && car_z > onRoad.transform.position.z - Constants.road_length / 2)
                        {
                            if(transform.position.z < onRoad.transform.position.z && transform.position.z > onRoad.transform.position.z - Constants.road_length / 2)
                            {
                                key = 1;
                                break;
                            }
                            else
                            {

                            }
                        }
                        else
                        {
                        }
                    }
                    else 
                    {
                    }

                }
                else // z축 방향으로 자동차가 이동 중
                {
                    if (dir.z > 0)
                    {
                        if (dir.x < 3 && dir.x > -3) //x축 방향으로 차이가 많이 안나는 경우 => 같은 차선인 경우
                        {
                            key = 1;
                        }
                        else
                        {
                            key = 0;
                        }
                    }
                }                
            }
        Accelerate(key);
    } 

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(Front_Left_Wheel, Front_Left_T);
        UpdateWheelPose(Front_Right_Wheel, Front_Right_T);
        UpdateWheelPose(Rear_Left_Wheel, Rear_Left_T);
        UpdateWheelPose(Rear_Right_Wheel, Rear_Right_T);
    }
    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;

    }
    private void FixedUpdate()
    {
        ControlVelocity();
        UpdateWheelPoses();
    }

    


    private float m_steeringAngle;

    public Rigidbody rb;

    public WheelCollider Front_Left_Wheel, Front_Right_Wheel;
    public WheelCollider Rear_Left_Wheel, Rear_Right_Wheel;
    public Transform Front_Left_T, Front_Right_T;
    public Transform Rear_Left_T, Rear_Right_T;
    public float maxSteerAngle = 30;
    public float motorForce = 50;
    public float maxSpeed= 10;

    public LayerMask layerMask;
    public LayerMask layerMask2;
    public float safeDistance=7;



}
