using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// �Q�[���̃t�F�[�Y���Ǘ�����N���X�B
/// �t�F�[�Y���ς�邱�ƂŁA�G�̐����₻�̑��̃Q�[���v�f���X�V�����B
/// </summary>
public class PhaseManager : MonoBehaviour
{
    public enum Phase
    {
        Phase1,
        Phase2,
        Phase3,
        Phase4,
        Phase5,
    }

    [SerializeField] public Phase CurrentPhase { get; private set; } // ���݂̃t�F�[�Y���Ǘ�����v���p�e�B
    [SerializeField] private EnemyGenerator enemyGenerator; // �G�̐������Ǘ����� EnemyGenerator �̎Q��

    public CinemachineVirtualCamera vCamSide; //�T�C�h�r���[�J����
    [SerializeField] private CinemachineVirtualCamera vCamTop;  //�g�b�v�r���[
    [SerializeField] private CinemachineVirtualCamera vCamBack; //�o�b�N�r���[

    private int currentEnemy = 0;


    private void Start()
    {
        // �����t�F�[�Y��ݒ�
        SetPhase(CurrentPhase);
    }

    /// <summary>
    /// �t�F�[�Y��ݒ肵�AEnemyGenerator �ɐV�����t�F�[�Y��ʒm���郁�\�b�h�B
    /// </summary>
    /// <param name="newPhase">�V�����t�F�[�Y</param>
    public void SetPhase(Phase newPhase)
    {
        CurrentPhase = newPhase;
        // �J�����̐ݒ�
        UpdateCamera(CurrentPhase);

        // EnemyGenerator �ɐV�����t�F�[�Y��ʒm
        if (enemyGenerator != null)
        {
            enemyGenerator.UpdatePhase(CurrentPhase);
        }
        else
        {
            Debug.LogWarning("EnemyGenerator is not assigned in PhaseManager.");
        }

        // �ǉ��̃t�F�[�Y�ύX����������΂����ɒǉ�
    }
    public void NextPhase()
    {
        CurrentPhase++;
        // �J�����̐ݒ�
        UpdateCamera(CurrentPhase);

        Debug.Log("nextphase");

        // EnemyGenerator �ɐV�����t�F�[�Y��ʒm
        if (enemyGenerator != null)
        {
            enemyGenerator.UpdatePhase(CurrentPhase);
        }
        else
        {
            Debug.LogWarning("EnemyGenerator is not assigned in PhaseManager.");
        }

        // �ǉ��̃t�F�[�Y�ύX����������΂����ɒǉ�
    }

    /// <summary>
    /// ���݂̃t�F�[�Y���擾���郁�\�b�h�B
    /// </summary>
    /// <returns>���݂̃t�F�[�Y</returns>
    public Phase GetCurrentPhase()
    {
        return CurrentPhase;
    }

    /// <summary>
    /// �t�F�[�Y�ɉ����ăJ�������X�V���郁�\�b�h�B
    /// </summary>
    /// <param name="phase">���݂̃t�F�[�Y</param>
    private void UpdateCamera(Phase phase)
    {
        switch (phase)
        {
            case Phase.Phase1:
                SetCamera(vCamSide);
                break;
            case Phase.Phase2:
                SetCamera(vCamTop);
                break;
            case Phase.Phase3:
            case Phase.Phase4:
                SetCamera(vCamBack);
                break;
            case Phase.Phase5:
                SetCamera(vCamSide);
                break;
        }
    }

    // �w�肳�ꂽ�J�������A�N�e�B�u�ɂ��郁�\�b�h
    private void SetCamera(CinemachineVirtualCamera activeCamera)
    {
        // ���ׂẴJ�����̗D��x��0�ɐݒ�
        vCamSide.Priority = 0;
        vCamTop.Priority = 0;
        vCamBack.Priority = 0;

        // �w�肳�ꂽ�J�����̗D��x��10�ɐݒ�
        activeCamera.Priority = 10;
    }

    public void PhaseEnemyCountSet(int phaseEnemy)
    {
        currentEnemy = phaseEnemy;
    } 
    public void EnemyDestroyCount()
    {
        currentEnemy--;

        if (currentEnemy <= 0)
        {
            NextPhase();
        }

    }
}
