using UnityEngine;
using UnityEngine.UI; // ·≈÷«›… œ⁄„ ·Ê«ÃÂ… «·„” Œœ„

public class DragDrop : MonoBehaviour
{
    public GameObject objectsToDrag;
    public GameObject dropPositions;
    public float DropDistance;
    public bool isLocked;

    public AudioSource dropSound; // «·’Ê  ⁄‰œ «·‰Ã«Õ
    public GameObject successPanel; // «·‹ Panel «·–Ì ÌŸÂ— ⁄‰œ «·‰Ã«Õ
    public Image successImage; // «·’Ê—… «· Ì ” ŸÂ— ⁄‰œ «·‰Ã«Õ

    private Vector3 objectinitPos; //  Œ“Ì‰ «·„Ê÷⁄ «·√Ê·Ì

    void Start()
    {
        objectinitPos = objectsToDrag.transform.position;
        successPanel.SetActive(false); // ≈Œ›«¡ «·‹ Panel ›Ì «·»œ«Ì…
        successImage.gameObject.SetActive(false); // ≈Œ›«¡ «·’Ê—… ›Ì «·»œ«Ì…
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
                dropSound.Play(); //  ‘€Ì· «·’Ê 
            }

            if (successImage != null)
            {
                successImage.gameObject.SetActive(true); // ≈ŸÂ«— «·’Ê—… ⁄‰œ «·‰Ã«Õ
            }
        }
        else
        {
            objectsToDrag.transform.position = objectinitPos; // ≈⁄«œ… «·⁄‰’— ·„ﬂ«‰Â «·√’·Ì
        }
    }
}
