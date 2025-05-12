using UnityEngine;
using TMPro; // Required for TextMeshProUGUI

public class PlayerSwitcher : MonoBehaviour
{
    [Header("Player References")]
    [Tooltip("First player's GameObject (should be active at start).")]
    [SerializeField] private GameObject playerOne;
    
    [Tooltip("Second player's GameObject (should be inactive at start).")]
    [SerializeField] private GameObject playerTwo;
    public TextMeshProUGUI textMeshProUGUI;
    // Flag to track which player is currently active.
    private bool isPlayerOneActive = true;

    private void Start()
    {
        // Ensure that both players are assigned.
        if (playerOne == null || playerTwo == null)
        {
            Debug.LogError("Both playerOne and playerTwo must be assigned in the Inspector.");
            return;
        }
        textMeshProUGUI.text = "Switch player press F or Right Mouse Button";
        // Set initial state: Player One is active, Player Two is disabled.
        playerOne.SetActive(true);
        playerTwo.SetActive(false);
        isPlayerOneActive = true;
    }

    private void Update()
    {
        // Listen for the right mouse button click.
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.F))
        {
            SwitchPlayers();
        }

    }

    /// <summary>
    /// Switches the active player by disabling the current one, copying its transform to the other,
    /// and then enabling the other player.
    /// </summary>
    private void SwitchPlayers()
    {
        if (isPlayerOneActive)
        {
            // Get the current transform data from Player One.
            Vector3 pos = playerOne.transform.position;
            Quaternion rot = playerOne.transform.rotation;

            // Disable Player One.
            playerOne.SetActive(false);

            // Place Player Two at the same location and enable it.
            playerTwo.transform.position = pos;
            playerTwo.transform.rotation = rot;
            playerTwo.SetActive(true);

            // Update the flag.
            isPlayerOneActive = false;
            textMeshProUGUI.text = playerTwo.name + " is active";
        }
        else
        {
            // Get the current transform data from Player Two.
            Vector3 pos = playerTwo.transform.position;
            Quaternion rot = playerTwo.transform.rotation;

            // Disable Player Two.
            playerTwo.SetActive(false);

            // Place Player One at the same location and enable it.
            playerOne.transform.position = pos;
            playerOne.transform.rotation = rot;
            playerOne.SetActive(true);

            // Update the flag.
            isPlayerOneActive = true;
            textMeshProUGUI.text = playerOne.name + " is active";
        }
    }
}