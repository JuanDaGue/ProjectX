using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Teleport")]
public class Teleport : Skill
{
    [Header("Teleport Settings")]
    [SerializeField] private float forceAmount = 500f;
    [SerializeField] private float upwardBoost = 2f;

    private Rigidbody playerRb;

    public override void Use(GameObject user)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerRb = player.GetComponent<Rigidbody>();

            if (playerRb != null)
            {
                Vector3 forceDirection = player.transform.forward * forceAmount + Vector3.up * upwardBoost;
                playerRb.AddForce(forceDirection, ForceMode.Impulse);
                Debug.Log("Teleporting with force!");
            }
            else
            {
                Debug.LogWarning("Player Rigidbody not found!");
            }
        }
    }

    public override void Cancel()
    {
        Debug.Log("Teleport cancelled!");
    }
}