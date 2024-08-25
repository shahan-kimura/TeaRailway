using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    RectTransform rectTransform; // UI�v�f��RectTransform
    Vector3 startPosition;       // �X���C�v�J�n���̈ʒu
    Vector3 lastMousePosition;   // �O�t���[���ł̃}�E�X�̈ʒu

    // SmokeController������GameObject�ւ̎Q�Ƃ�ێ����邽�߂̃V���A���C�Y�t�B�[���h
    [SerializeField]
    private GameObject smokeObject;
    // SmokeController�R���|�[�l���g�ւ̎Q��
    private SmokeController smokeController;

    // �x�J�������肷��ϐ�
    private bool isSmoking = false;
    //�h���b�O�����ǂ����𔻒肷��t���O
    private bool isDragging = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>(); // RectTransform���擾
        startPosition = rectTransform.anchoredPosition3D; // �����ʒu��ۑ��iVector3�^�j
        lastMousePosition = Input.mousePosition; // �O�t���[���̃}�E�X�ʒu��ۑ�

        // smokeObject��null�łȂ��ꍇ�A���̃I�u�W�F�N�g����SmokeController�R���|�[�l���g���擾
        if (smokeObject != null)
        {
            smokeController = smokeObject.GetComponent<SmokeController>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // UI�v�f���̃N���b�N�̂݃h���b�O���J�n
            if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition, null)) // �C��: �}�E�X�ʒu��UI���ɂ��邩����
            {
                isDragging = true; // �C��: �h���b�O��Ԃ��Ǘ�����t���O��ݒ�
                // �}�E�X���N���b�N�����ꍇ�̓X���C�v�J�n
                startPosition = rectTransform.anchoredPosition3D;
                lastMousePosition = Input.mousePosition;
            }
        }

        if (isDragging && Input.GetMouseButton(0)) // �h���b�O��Ԃ̂�UI���ړ�
        {
            // �h���b�O����UI�v�f���ړ�����
            Vector3 delta = Input.mousePosition - lastMousePosition;
            rectTransform.anchoredPosition3D += new Vector3(0, delta.y, 0);
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false; // �C��: �h���b�O�I�����Ƀt���O�����Z�b�g
            // �}�E�X�𗣂����ꍇ�͏����ʒu�ɖ߂�
            rectTransform.anchoredPosition3D = startPosition;
        }


        //�x�J�����ȉ��ɉ������Ă����瓮��
        WhistleAction();

    }

    private void WhistleAction()
    {
        if (rectTransform.anchoredPosition3D.y < -400)
        {
            smokeController.SmokeUp();
            Debug.Log(rectTransform.anchoredPosition3D);
            if (!isSmoking)
            {
                isSmoking = true;
                GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            isSmoking = false;
            smokeController.SmokeDown();
        }
    }
}
