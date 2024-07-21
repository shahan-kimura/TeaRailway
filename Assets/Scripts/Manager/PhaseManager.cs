using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private Phase currentPhase; // ���݂̃t�F�[�Y���Ǘ�����ϐ�
    [SerializeField] private EnemyGenerator enemyGenerator; // �G�̐������Ǘ����� EnemyGenerator �̎Q��

    private void Start()
    {
        // �����t�F�[�Y��ݒ�
        SetPhase(currentPhase);
    }

    /// <summary>
    /// �t�F�[�Y��ݒ肵�AEnemyGenerator �ɐV�����t�F�[�Y��ʒm���郁�\�b�h�B
    /// </summary>
    /// <param name="newPhase">�V�����t�F�[�Y</param>
    public void SetPhase(Phase newPhase)
    {
        currentPhase = newPhase;

        // EnemyGenerator �ɐV�����t�F�[�Y��ʒm
        if (enemyGenerator != null)
        {
            enemyGenerator.UpdatePhase(currentPhase);
        }
        else
        {
            Debug.LogWarning("EnemyGenerator is not assigned in PhaseManager.");
        }

        // �ǉ��̃t�F�[�Y�ύX����������΂����ɒǉ�
    }
    public void NextPhase()
    {
        currentPhase++;;

        Debug.Log("nextphase");

        // EnemyGenerator �ɐV�����t�F�[�Y��ʒm
        if (enemyGenerator != null)
        {
            enemyGenerator.UpdatePhase(currentPhase);
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
        return currentPhase;
    }
}
