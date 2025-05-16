using UnityEngine;

[CreateAssetMenu(fileName = "Teleport", menuName = "Skills/Teleport")]
public class Teleport : Skill
{
    public float teleportDistance = 10f;

    public override void Use(GameObject user)
    {
        if (user == null) return;

        // Optional: show visual effect at the original position
        if (visualEffect != null)
        {
            GameObject effect = Instantiate(visualEffect, user.transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }

        // Play sound effect
        if (soundEffect != null)
        {
            AudioSource.PlayClipAtPoint(soundEffect, user.transform.position);
        }

        // Calculate the teleport target position
        Vector3 targetPosition = user.transform.position + user.transform.forward * teleportDistance;

        // Perform the teleportation
        user.transform.position = targetPosition;
    }

    public override void Cancel()
    {
        Debug.Log("Teleport cancelled!");
    }
}
