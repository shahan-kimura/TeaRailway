using UnityEngine;

public class AudioClipChanger : MonoBehaviour
{
    public AudioSource audioSource; // AudioSource�R���|�[�l���g���A�^�b�`
    public AudioClip clipA; // A�L�[�ōĐ�����AudioClip
    public AudioClip clipB; // B�L�[�ōĐ�����AudioClip

    private AudioClip currentClip; // ���ݍĐ����̃N���b�v

    void Start()
    {
        currentClip = clipA;
        audioSource.clip = currentClip;
        audioSource.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchClip(clipA);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            SwitchClip(clipB);
        }
    }

    void SwitchClip(AudioClip newClip)
    {
        if (currentClip == newClip) return; // ���ɍĐ����̋ȂƓ����ꍇ�͍Đ����Ȃ�

        float currentTime = audioSource.time; // ���݂̍Đ����Ԃ�ێ�

        audioSource.Stop(); // ���݂̃N���b�v�̍Đ����~
        audioSource.clip = newClip;
        audioSource.time = currentTime; // �V�����N���b�v�̍Đ����Ԃ�ݒ�
        audioSource.Play();

        currentClip = newClip; // ���݂̃N���b�v���X�V
    }
}
