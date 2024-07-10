using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // �V���O���g���C���X�^���X�ւ̐ÓI�v���p�e�B
    public static GameManager Instance { get; private set; }

    // ���x�X�R�A���
     public float VelocityScore { get; private set; }

    private void Awake()
    {
        // �C���X�^���X�����݂��Ȃ��ꍇ�A���̃C���X�^���X��ݒ�
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �V�[���ԂŔj������Ȃ��悤�ɂ���
        }
        else if (Instance != this)
        {
            // ���ɃC���X�^���X�����݂���ꍇ�A���̃I�u�W�F�N�g��j��
            Destroy(gameObject);
        }
    }

    // �X�R�A��ǉ����郁�\�b�h
    public void SetClearSpeed(float clearSpeed)
    {
        VelocityScore = clearSpeed;
    }

    // �X�R�A�����Z�b�g���郁�\�b�h
    public void ResetScore()
    {
        VelocityScore = 0f;
    }

    // �^�C�g���V�[���ɖ߂郁�\�b�h
    public void ReturnToTitle()
    {
        ResetScore();
        SceneManager.LoadScene("TitleScene"); // "TitleScene" ���^�C�g���V�[���̖��O�ɒu�������Ă�������
    }

    // ���̃��\�b�h��v���p�e�B��ǉ��ł����A����ȕ��ɏ����Ă������I
    public void SomeGameManagerFunction()
    {
        Debug.Log("GameManager function called!");
    }
}