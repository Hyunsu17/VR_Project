using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class steering_wheel : MonoBehaviour
{
    public KeyCode pressup;
    public KeyCode pressdown;
    public KeyCode pressleft;
    public KeyCode pressright;
    private int left_flag = 0;
    private int right_flag = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        

        if (Input.GetKeyDown(pressleft))
        { 
                left_flag = 1;
                right_flag = 0;
                GetComponent<Transform>().Rotate(0, -30, 0);
            }
   
        
           
               

           
        if (Input.GetKeyDown(pressright))  
            {
                left_flag = 0;
                right_flag = 1;
                GetComponent<Transform>().Rotate(0, 30, 0);
            }
         

        if(Input.GetKeyUp(pressleft))
        {
            left_flag = 0;
            GetComponent<Transform>().Rotate(0, 30, 0);

        }

        if( Input.GetKeyUp(pressright))
        {
            right_flag = 0;
            GetComponent<Transform>().Rotate(0, -30, 0);
        }


    }
}
