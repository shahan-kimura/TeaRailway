using UnityEngine;
 
public class EnemyGenerator : MonoBehaviour
{
    //�G�v���n�u
    [SerializeField] GameObject enemyPrefab;
    //�v���C���[�ʒu
    [SerializeField] Transform player;

    //�G�������ԊԊu
    [SerializeField] private  float interval;
    //�o�ߎ���
    private float time = 0f;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //���Ԍv��
        time += Time.deltaTime;

        //�o�ߎ��Ԃ��������ԂɂȂ����Ƃ�(�������Ԃ��傫���Ȃ����Ƃ�)
        if (time > interval)
        {
            //enemy���C���X�^���X������(��������)
            GameObject enemy = Instantiate(enemyPrefab);
            //���������G�̍��W�����肷��
            enemy.transform.position = 
                new Vector3(player.transform.position.x -15f,
                            1,
                            Random.Range(-2f,-10f));
            //�o�ߎ��Ԃ����������čēx���Ԍv�����n�߂�
            time = 0f;
        }
    }
}