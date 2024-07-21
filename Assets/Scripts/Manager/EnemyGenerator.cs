using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �G�̐������Ǘ�����N���X�B
/// �t�F�[�Y�ɉ����ēG�̏o���ʒu���ނ𐧌䂷��B
/// </summary>
public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints; // �G�̏o���|�C���g
    [SerializeField] private GameObject[] enemyPrefabs; // �G�̃v���n�u
    [SerializeField] private float spawnInterval = 2f; // �G�̏o���Ԋu
    [SerializeField] private int enemiesPerPhase = 5; // �e�t�F�[�Y�ŏo������G�̐�

    Transform spawnPoint;   //���݂�spawnPoint
    GameObject enemyPrefab; //���݂�enemyPrefab


    private PhaseManager.Phase currentPhase; // ���݂̃t�F�[�Y

    private void Start()
    {
        spawnPoint = spawnPoints[(int)currentPhase];

        // �����ݒ�
        // �����ł͓��ɏ������s��Ȃ�
    }

    /// <summary>
    /// �t�F�[�Y���X�V���ꂽ�ۂɌĂ΂�郁�\�b�h�B
    /// �t�F�[�Y�ɉ����ēG�̐����ݒ��ύX����B
    /// </summary>
    /// <param name="newPhase">�V�����t�F�[�Y</param>
    public void UpdatePhase(PhaseManager.Phase newPhase)
    {
        currentPhase = newPhase;

        // �t�F�[�Y�ɉ����ēG�̏o���Ԋu�␔�𒲐�����
        switch (currentPhase)
        {
            case PhaseManager.Phase.Phase1:
                spawnInterval = 0.2f;
                enemiesPerPhase = 5;
                spawnPoint = spawnPoints[0];
                enemyPrefab = enemyPrefabs[0];
                break;
            case PhaseManager.Phase.Phase2:
                spawnInterval = 0.2f;
                enemiesPerPhase = 5;
                spawnPoint = spawnPoints[1];
                enemyPrefab = enemyPrefabs[1];
                break;
            case PhaseManager.Phase.Phase3:
                spawnInterval = 0.2f;
                enemiesPerPhase = 10;
                spawnPoint = spawnPoints[0];
                enemyPrefab = enemyPrefabs[0];
                break;
            case PhaseManager.Phase.Phase4:
                spawnInterval = 0.2f;
                enemiesPerPhase = 10;
                spawnPoint = spawnPoints[1];
                enemyPrefab = enemyPrefabs[1];
                break;
            case PhaseManager.Phase.Phase5:
                spawnInterval = 0.2f;
                enemiesPerPhase = 10;
                break;
        }

        // �V�����t�F�[�Y�ɉ����ēG�̐������J�n����
        StartCoroutine(SpawnEnemies());
    }

    /// <summary>
    /// �G���t�F�[�Y���ƂɃX�|�[��������R���[�`���B
    /// </summary>
    private IEnumerator SpawnEnemies()
    {
        // ���݂̃t�F�[�Y�ɉ������G�̐�������
        int enemiesToSpawn = enemiesPerPhase;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            // �G���X�|�[���|�C���g�ɃX�|�[��������
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

            // �w�肳�ꂽ�Ԋu�Ŏ��̓G���X�|�[��������
            yield return new WaitForSeconds(spawnInterval);
        }

        // �t�F�[�Y�I����̏���������΂����ɒǉ�
    }
}


//using UnityEngine;

//public class EnemyGenerator : MonoBehaviour
//{
//    //�G�v���n�u
//    [SerializeField] GameObject enemyPrefab;
//    //�v���C���[�ʒu
//    [SerializeField] Transform player;

//    //�G�������ԊԊu
//    [SerializeField] private  float interval;
//    //�o�ߎ���
//    private float time = 0f;

//    private void Start()
//    {
//        player = GameObject.Find("Player").GetComponent<Transform>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        //���Ԍv��
//        time += Time.deltaTime;

//        //�o�ߎ��Ԃ��������ԂɂȂ����Ƃ�(�������Ԃ��傫���Ȃ����Ƃ�)
//        if (time > interval)
//        {
//            //enemy���C���X�^���X������(��������)
//            GameObject enemy = Instantiate(enemyPrefab);
//            //���������G�̍��W�����肷��
//            enemy.transform.position = 
//                new Vector3(player.transform.position.x -15f,
//                            1,
//                            Random.Range(-2f,-10f));
//            //�o�ߎ��Ԃ����������čēx���Ԍv�����n�߂�
//            time = 0f;
//        }
//    }
//}