using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // シングルトンインスタンスへの静的プロパティ
    public static GameManager Instance { get; private set; }

    // 速度スコア情報
     public float VelocityScore { get; private set; }

    private void Awake()
    {
        // インスタンスが存在しない場合、このインスタンスを設定
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーン間で破棄されないようにする
        }
        else if (Instance != this)
        {
            // 既にインスタンスが存在する場合、このオブジェクトを破棄
            Destroy(gameObject);
        }
    }

    // スコアを追加するメソッド
    public void SetClearSpeed(float clearSpeed)
    {
        VelocityScore = clearSpeed;
    }

    // スコアをリセットするメソッド
    public void ResetScore()
    {
        VelocityScore = 0f;
    }

    // タイトルシーンに戻るメソッド
    public void ReturnToTitle()
    {
        ResetScore();
        SceneManager.LoadScene("TitleScene"); // "TitleScene" をタイトルシーンの名前に置き換えてください
    }

    // 他のメソッドやプロパティを追加できるよ、こんな風に書いていこう！
    public void SomeGameManagerFunction()
    {
        Debug.Log("GameManager function called!");
    }
}