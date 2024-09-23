using UnityEngine;
using UnityEngine.Advertisements;

// Unity Ads���g�p���ăC���^�[�X�e�B�V�����L����\������N���X
// IUnityAdsLoadListener��IUnityAdsShowListener��2�̃C���^�[�t�F�[�X���������āA�L���̓ǂݍ��݂ƕ\���̃��X�i�[���
public class InterstitialAdExample : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    // Android��iOS�p�̍L�����j�b�gID��ݒ�i�L�����j�b�g�̓v���b�g�t�H�[�����ƂɈقȂ�j
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOsAdUnitId = "Interstitial_iOS";
    string _adUnitId; // ���݂̃v���b�g�t�H�[���ɑΉ�����L�����j�b�gID

    // �X�N���v�g���A�N�e�B�u�ɂȂ����ۂɌĂ΂��i�ŏ��ɍL�����j�b�gID���v���b�g�t�H�[���ɉ����Đݒ�j
    void Awake()
    {
        // ���s����Ă���v���b�g�t�H�[���ɉ����āA�L�����j�b�gID��ݒ肷��
        // iPhone�iiOS�j�̏ꍇ��iOS�p�̍L�����j�b�gID�A����ȊO��Android�p
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsAdUnitId
            : _androidAdUnitId;
    }

    // �L���R���e���c��ǂݍ��ރ��\�b�h
    public void LoadAd()
    {
        // �L����ǂݍ��ށiIMPORTANT! ��������ɂ̂ݍL����ǂݍ��ނ��Ƃ��d�v�j
        // �����������͕ʂ̃X�N���v�g�ōs�����̂Ƃ���
        Debug.Log("Loading Ad: " + _adUnitId); // �f�o�b�O���O�Ō��݂̍L�����j�b�gID���o��
        Advertisement.Load(_adUnitId, this);  // �L����ǂݍ��ލہA�ǂݍ��݃��X�i�[�ithis�j���w��
    }

    // �ǂݍ��񂾍L����\�����郁�\�b�h
    public void ShowAd()
    {
        // ���O�ɍL�����ǂݍ��܂�Ă��Ȃ��ƁA���̃��\�b�h�͎��s����̂Œ���
        Debug.Log("Showing Ad: " + _adUnitId); // �L�����j�b�gID��\��
        Advertisement.Show(_adUnitId, this);   // �L����\������ہA�\�����X�i�[�ithis�j���w��
    }

    // IUnityAdsLoadListener��IUnityAdsShowListener�̃C���^�[�t�F�[�X���\�b�h������

    // �L��������ɓǂݍ��܂ꂽ�Ƃ��ɌĂ΂��
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        // �L��������ɓǂݍ��܂ꂽ�ۂɎ��s����R�[�h�������ɋL�q�\
    }

    // �L���̓ǂݍ��݂Ɏ��s�����Ƃ��ɌĂ΂��
    public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
        // �L���̓ǂݍ��݂Ɏ��s�����ꍇ�A�Ď��s����Ȃǂ̏����������ɋL�q�\
    }

    // �L���̕\���Ɏ��s�����Ƃ��ɌĂ΂��
    public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
        // �L���̕\���Ɏ��s�����ꍇ�A�ʂ̍L����ǂݍ��ނȂǂ̏����������ɋL�q�\
    }

    // �L���̕\�����J�n���ꂽ�Ƃ��ɌĂ΂��
    public void OnUnityAdsShowStart(string _adUnitId) { }

    // ���[�U�[���L�����N���b�N�����Ƃ��ɌĂ΂��
    public void OnUnityAdsShowClick(string _adUnitId) { }

    // �L���̕\�������������Ƃ��ɌĂ΂��
    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState) { }
}