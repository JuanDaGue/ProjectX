using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Skills/Laser Destroyer")]
public class LaserDestroyer : Skill
{
    [Header("Laser Settings")]
    [SerializeField] private float laserRange = 20f;
    [SerializeField] private float damagePerSecond = 30f;
    [SerializeField] private LayerMask targetLayers;
    
    private Transform playerTransform;
    private LineRenderer laserLine;
    private bool isActive;

    public override void Use(GameObject user)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //Debug.Log(playerTransform);
        laserLine = playerTransform.GetComponentInChildren<LineRenderer>();
        
        if (!isActive)
        {
            StartLaser();
        }
        else
        {
            StopLaser();
        }
    }

    private void StartLaser()
    {
        isActive = true;
        laserLine.enabled = true;
        //playerTransform.StartCoroutine(LaserRoutine());
        LaserRoutine();
        Debug.Log("Destructor Ray activated! Enemies beware.");
    }

    private IEnumerator LaserRoutine()
    {
        while (isActive)
        {
            UpdateLaser();
            ApplyDamage();
            yield return null;
        }
    }

    private void UpdateLaser()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerTransform.position, playerTransform.forward, out hit, laserRange, targetLayers))
        {
            laserLine.SetPosition(0, playerTransform.position);
            laserLine.SetPosition(1, hit.point);
        }
        else
        {
            laserLine.SetPosition(0, playerTransform.position);
            laserLine.SetPosition(1, playerTransform.position + playerTransform.forward * laserRange);
        }
    }

    private void ApplyDamage()
    {
        RaycastHit[] hits = Physics.RaycastAll(playerTransform.position, playerTransform.forward, laserRange, targetLayers);
        
        foreach (var hit in hits)
        {
            if (hit.collider.TryGetComponent<LifeSystem>(out var healthSystem))
            {
                healthSystem.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }

    private void StopLaser()
    {
        isActive = false;
        laserLine.enabled = false;
    }

    public override void Cancel()
    {
        StopLaser();
    }
}