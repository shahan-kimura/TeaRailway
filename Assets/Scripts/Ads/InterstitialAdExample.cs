using UnityEngine;
using UnityEngine.Advertisements;

// Unity Adsを使用してインタースティシャル広告を表示するクラス
// IUnityAdsLoadListenerとIUnityAdsShowListenerの2つのインターフェースを実装して、広告の読み込みと表示のリスナーを提供
public class InterstitialAdExample : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    // AndroidとiOS用の広告ユニットIDを設定（広告ユニットはプラットフォームごとに異なる）
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOsAdUnitId = "Interstitial_iOS";
    string _adUnitId; // 現在のプラットフォームに対応する広告ユニットID

    // スクリプトがアクティブになった際に呼ばれる（最初に広告ユニットIDをプラットフォームに応じて設定）
    void Awake()
    {
        // 実行されているプラットフォームに応じて、広告ユニットIDを設定する
        // iPhone（iOS）の場合はiOS用の広告ユニットID、それ以外はAndroid用
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsAdUnitId
            : _androidAdUnitId;
    }

    // 広告コンテンツを読み込むメソッド
    public void LoadAd()
    {
        // 広告を読み込む（IMPORTANT! 初期化後にのみ広告を読み込むことが重要）
        // 初期化処理は別のスクリプトで行うものとする
        Debug.Log("Loading Ad: " + _adUnitId); // デバッグログで現在の広告ユニットIDを出力
        Advertisement.Load(_adUnitId, this);  // 広告を読み込む際、読み込みリスナー（this）を指定
    }

    // 読み込んだ広告を表示するメソッド
    public void ShowAd()
    {
        // 事前に広告が読み込まれていないと、このメソッドは失敗するので注意
        Debug.Log("Showing Ad: " + _adUnitId); // 広告ユニットIDを表示
        Advertisement.Show(_adUnitId, this);   // 広告を表示する際、表示リスナー（this）を指定
    }

    // IUnityAdsLoadListenerとIUnityAdsShowListenerのインターフェースメソッドを実装

    // 広告が正常に読み込まれたときに呼ばれる
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        // 広告が正常に読み込まれた際に実行するコードをここに記述可能
    }

    // 広告の読み込みに失敗したときに呼ばれる
    public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
        // 広告の読み込みに失敗した場合、再試行するなどの処理をここに記述可能
    }

    // 広告の表示に失敗したときに呼ばれる
    public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
        // 広告の表示に失敗した場合、別の広告を読み込むなどの処理をここに記述可能
    }

    // 広告の表示が開始されたときに呼ばれる
    public void OnUnityAdsShowStart(string _adUnitId) { }

    // ユーザーが広告をクリックしたときに呼ばれる
    public void OnUnityAdsShowClick(string _adUnitId) { }

    // 広告の表示が完了したときに呼ばれる
    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState) { }
}