using UnityEngine;

public class AudioClipChanger : MonoBehaviour
{
    public AudioSource audioSource; // AudioSourceコンポーネントをアタッチ
    public AudioClip clipA; // Aキーで再生するAudioClip
    public AudioClip clipB; // Bキーで再生するAudioClip

    private AudioClip currentClip; // 現在再生中のクリップ

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
        if (currentClip == newClip) return; // 既に再生中の曲と同じ場合は再生しない

        float currentTime = audioSource.time; // 現在の再生時間を保持

        audioSource.Stop(); // 現在のクリップの再生を停止
        audioSource.clip = newClip;
        audioSource.time = currentTime; // 新しいクリップの再生時間を設定
        audioSource.Play();

        currentClip = newClip; // 現在のクリップを更新
    }
}
