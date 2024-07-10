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
            //クリア時の速度情報をGameManagerに送信
            Player player = other.GetComponent<Player>();
            float clearSpeed = player.CurrentSpeed;
            GameManager.Instance.SetClearSpeed(clearSpeed);

            SceneManager.LoadScene("ClearScene");
        }

    }
}
