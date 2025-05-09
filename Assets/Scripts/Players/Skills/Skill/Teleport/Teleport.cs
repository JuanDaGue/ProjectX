using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Skills/Teleport")]
public class Teleport : Skill
{

    [Header("Teleport Settings")]
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private float castTime = 0.5f;


    private Transform playerTransform;

    public override void Use()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //playerTransform.StartCoroutine(TeleportRoutine());
        TeleportRoutine();
        Debug.Log("Teleporting to a new location!");
    }
    

    private IEnumerator TeleportRoutine()
    {
        
        Vector3 targetPosition = playerTransform.position + playerTransform.forward * maxDistance;
        
        float elapsedTime = 0f;
        while(elapsedTime < castTime)
        {
            if(Input.GetMouseButton(1)) 
            {
                Cancel();
                yield break;
            }
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        playerTransform.position = targetPosition;

    }


    public override void Cancel()
    {
        //cancellation effects
        Debug.Log("Teleport cancelled!");
    }
}
