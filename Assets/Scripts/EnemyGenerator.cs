using UnityEngine;
 
public class EnemyGenerator : MonoBehaviour
{
    //�G�v���n�u
    [SerializeField] GameObject enemyPrefab;
    //�G�������ԊԊu
    [SerializeField] private  float interval;
    //�o�ߎ���
    private float time = 0f;


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
            enemy.transform.position = new Vector3(-10, 1, -5);
            //�o�ߎ��Ԃ����������čēx���Ԍv�����n�߂�
            time = 0f;
        }
    }
}