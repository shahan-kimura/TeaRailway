using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    RectTransform rectTransform; // UI�v�f��RectTransform
    Vector3 startPosition;       // �X���C�v�J�n���̈ʒu
    Vector3 lastMousePosition;   // �O�t���[���ł̃}�E�X�̈ʒu

    void Start()
    {
        rectTransform = GetComponent<RectTransform>(); // RectTransform���擾
        startPosition = rectTransform.anchoredPosition3D; // �����ʒu��ۑ��iVector3�^�j
        lastMousePosition = Input.mousePosition; // �O�t���[���̃}�E�X�ʒu��ۑ�
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // �}�E�X���N���b�N�����ꍇ�̓X���C�v�J�n
            startPosition = rectTransform.anchoredPosition3D;
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            // �h���b�O����UI�v�f���ړ�����
            Vector3 delta = Input.mousePosition - lastMousePosition;
            rectTransform.anchoredPosition3D += new Vector3(0, delta.y, 0);
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // �}�E�X�𗣂����ꍇ�͏����ʒu�ɖ߂�
            rectTransform.anchoredPosition3D = startPosition;
        }


        //
        if(rectTransform.anchoredPosition3D.y < -400) 
        {
            Debug.Log(rectTransform.anchoredPosition3D);
        }
        
    }
}
