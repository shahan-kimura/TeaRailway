using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoutine : MonoBehaviour
{
    [SerializeField] GameObject lockOnCircle;   // ���b�N�I���T�[�N���̃I�u�W�F�N�g
    [SerializeField] string TagName;            //�����蔻��ƂȂ�^�O
    bool isLockOn = false;                      //���b�N�I������pbool


    void Start()
    {
        // ���b�N�I���T�[�N�����q�I�u�W�F�N�g���猟������i��Ƃ��Ďq�I�u�W�F�N�g�̖��O�� "LockOnCircle" �Ƃ���j
        lockOnCircle = transform.Find("LockOnCircle").gameObject;

        // ������Ԃł͔�\���ɂ���
        lockOnCircle.SetActive(false);
    }

    // ���b�N�I�����J�n����
    public void LockOnCheck()
    {
        if (isLockOn)
        {
            lockOnCircle.SetActive(true);    // ���b�N�I���T�[�N����\������
        }
        else
        {
            lockOnCircle.SetActive(false);    // ���b�N�I���T�[�N�����\������
        }
    }

    // ���b�N�I�����~����
    public void ActivateLockOn()
    {
        isLockOn = true;
        LockOnCheck();      //bool�̍X�V�ɍ��킹�ăZ�b�g��LockOnCheck��������
    }

    private void OnTriggerEnter(Collider other)
    {
        //�����蔻��̃^�O�l�[���ƈ�v���Ă���Ώ����̌Ăяo��
        if (other.CompareTag(TagName))
        {
            isLockOn = false;
            LockOnCheck();
        }
    }
}
