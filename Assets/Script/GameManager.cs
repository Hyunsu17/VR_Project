using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //check point ��
    public int TotalItemCount;
    //���� �������� �����ϴ� ����
    public string stage;

    private void Awake() {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
            SceneManager.LoadScene(stage);
    }
}
