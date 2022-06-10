using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //check point 수
    public int TotalItemCount;
    //현재 스테이지 저장하는 변수
    public string stage1;
    public string stage2;

    private void Awake() {

    }

    private void OnTriggerEnter(Collider other)
    {
    }
}
