using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //�N���A���̑��x����GameManager�ɑ��M
            Player player = other.GetComponent<Player>();
            float clearSpeed = player.CurrentSpeed;
            GameManager.Instance.SetClearSpeed(clearSpeed);

            SceneManager.LoadScene("ClearScene");
        }

    }
}
