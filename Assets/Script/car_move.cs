using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car_move : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform k = GameObject.Find("PlayerCar").GetComponent<Transform>();
        float velocity = 0.02f;
        Vector3 destination = new Vector3(15, 0, 166);
        int check_flag = 0;
        Vector3 dir = (destination - transform.position).normalized;

        float acceleration = 0.4f;

        velocity = (velocity + acceleration * Time.deltaTime);

        float distance = Vector3.Distance(k.position, transform.position);


        if (distance <= 15.0f)
            check_flag = 1;





        if (check_flag == 1)
        {
            Debug.Log("조건만족");

            transform.position = new Vector3(transform.position.x + (dir.x * velocity),

                                                   transform.position.y,

                                                     transform.position.z + (dir.z * velocity));





        }

        else

        {

            velocity = 0.0f;

        }

    }

    private void FixedUpdate()
    {
        
    }

    private void LateUpdate()
    {
      
    }

}
