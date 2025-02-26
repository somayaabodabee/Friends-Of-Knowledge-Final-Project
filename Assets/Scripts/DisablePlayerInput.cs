using UnityEngine;
using UnityEngine.EventSystems; // Required for UI interactions

public class DisablePlayerInput : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private CharacterMovement playerMovement; // Reference to the player's movement script

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Automatically find the player
        if (player != null)
        {
            playerMovement = player.GetComponent<CharacterMovement>();
        }
        else
        {
            Debug.LogError(" Player object not found! Make sure the player has the 'Player' tag.");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
            Debug.Log(" Player movement disabled!");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
            Debug.Log("Player movement enabled!");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(" Clicked on the Panel! Preventing interaction with the player.");
        eventData.Use(); // Stops the event from propagating to other objects
    }
}
