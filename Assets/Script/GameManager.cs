using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //check point ��
    public int TotalItemCount;
    //���� �������� �����ϴ� ����
    public string stage1;
    public string stage2;

    private void Awake() {

    }

    private void OnTriggerEnter(Collider other)
    {
    }
}
