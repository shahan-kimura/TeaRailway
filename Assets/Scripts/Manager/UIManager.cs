using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI phaseText; // Phase����\������UI Text
    [SerializeField] private TextMeshProUGUI currentEnemyText; // �c��Enemy������\������UI Text
    [SerializeField] private Image hpGage;                      //hp�Q�[�W�摜
    [SerializeField] private GameObject gameOverUI;      //�Q�[���I�[�o�[UI


    [SerializeField] private PhaseManager phaseManager; // PhaseManager�ւ̎Q��
    [SerializeField] private StatusManager playerStatusManager; // PhaseManager�ւ̎Q��

    private void Start()
    {
        gameOverUI.SetActive(false);
    }

    void Update()
    {
        // PhaseManager���猻�݂�Phase���擾����UI�ɕ\��
        phaseText.text = "Current Phase: " + phaseManager.CurrentPhase.ToString();
        currentEnemyText.text = "CurrentEnemy: " + phaseManager.CurrentEnemy.ToString();
        hpGage.fillAmount = (float)playerStatusManager.HP / (float)playerStatusManager.MaxHP;
        
    }

    public void GameOver() 
    {
        Debug.Log("gameover");
        //�Q�[���I�[�o�[����UI�\��
        gameOverUI.SetActive (true);
    }
}
