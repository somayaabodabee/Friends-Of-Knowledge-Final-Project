using UnityEngine;

public class PanelTrigger : MonoBehaviour
{
    public GameObject panel;
    private bool isPlayerInside = false;

    void Start()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            panel.SetActive(!panel.activeSelf);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            panel.SetActive(false);
        }
    }
}
