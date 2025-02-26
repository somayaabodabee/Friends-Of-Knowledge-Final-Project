using UnityEngine;
using UnityEngine.UI; // ������ ��� ������ ��������

public class DragDrop : MonoBehaviour
{
    public GameObject objectsToDrag;
    public GameObject dropPositions;
    public float DropDistance;
    public bool isLocked;

    public AudioSource dropSound; // ����� ��� ������
    public GameObject successPanel; // ��� Panel ���� ���� ��� ������
    public Image successImage; // ������ ���� ����� ��� ������

    private Vector3 objectinitPos; // ����� ������ ������

    void Start()
    {
        objectinitPos = objectsToDrag.transform.position;
        successPanel.SetActive(false); // ����� ��� Panel �� �������
        successImage.gameObject.SetActive(false); // ����� ������ �� �������
    }

    public void DragObject()
    {
        if (!isLocked)
        {
           
            objectsToDrag.transform.position = Input.mousePosition;
        }
    }

    public void DropObject()
    {
        float Distance = Vector3.Distance(objectsToDrag.transform.position, dropPositions.transform.position);

        if (Distance < DropDistance)
        {
            isLocked = true;
            objectsToDrag.transform.position = dropPositions.transform.position;

            if (dropSound != null)
            {
                dropSound.Play(); // ����� �����
            }

            if (successImage != null)
            {
                successImage.gameObject.SetActive(true); // ����� ������ ��� ������
            }
        }
        else
        {
            objectsToDrag.transform.position = objectinitPos; // ����� ������ ������ ������
        }
    }
}
