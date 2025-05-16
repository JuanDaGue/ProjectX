using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Skills/Laser Destroyer")]
public class LaserDestroyer : Skill
{
    [Header("Laser Settings")]
    [SerializeField] private float laserRange = 20f;
    [SerializeField] private float damagePerSecond = 30f;
    [SerializeField] private LayerMask targetLayers;
    [SerializeField] private float laserWidth = 0.1f;
    
    private Transform playerTransform;
    private LineRenderer laserLine;
    private Coroutine laserRoutine;
    private MonoBehaviour coroutineRunner;

    public override void Use(GameObject user)
    {
        Debug.Log("using laser destroyer");
        if (user == null)
        {
            Debug.LogError("User is null!");
            return;
        }
        // Get references once
        if (playerTransform == null)
        {
            Debug.Log("Initializing LaserDestroyer");
            playerTransform = user.transform;
            coroutineRunner = user.GetComponent<MonoBehaviour>();
            laserLine = user.GetComponentInChildren<LineRenderer>();

            if (laserLine == null)
            {
                Debug.LogError("No LineRenderer found on player!");
                return;
            }

            InitializeLaser();
        }
        else
        {
            Debug.Log("LaserDestroyer already initialized");
            playerTransform = user.transform;
            coroutineRunner = user.GetComponent<MonoBehaviour>();
            laserLine = user.GetComponentInChildren<LineRenderer>();

            if (laserLine == null)
            {
                Debug.LogError("No LineRenderer found on player!");
                return;
            }
            InitializeLaser();
        }

        if (laserRoutine == null)
        {
            StartLaser();
        }
        else
        {
            StopLaser();
        }
    }

    private void InitializeLaser()
    {
        laserLine.positionCount = 2;
        laserLine.startWidth = laserWidth;
        laserLine.endWidth = laserWidth;
        laserLine.enabled = false;
    }

    private void StartLaser()
    {
        if (coroutineRunner == null) return;
        
        laserLine.enabled = true;
        laserRoutine = coroutineRunner.StartCoroutine(LaserRoutine());
        Debug.Log("Laser activated!");
    }

    private IEnumerator LaserRoutine()
    {
        while (true)
        {
            UpdateLaser();
            ApplyDamage();
            yield return null;
        }
    }

    private void UpdateLaser()
    {
        Vector3 start = playerTransform.position + playerTransform.up * 0.7f;
        Vector3 end = start + playerTransform.forward * laserRange;

        if (Physics.Raycast(start, playerTransform.forward, out RaycastHit hit, laserRange, targetLayers))
        {
            end = hit.point;
        }

        laserLine.SetPosition(0, start);
        laserLine.SetPosition(1, end);
    }

    private void ApplyDamage()
    {
        Vector3 direction = playerTransform.forward;
        RaycastHit[] hits = Physics.SphereCastAll(
            playerTransform.position,
            laserWidth * 0.5f,
            direction,
            laserRange,
            targetLayers
        );

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
        if (laserRoutine != null)
        {
            coroutineRunner.StopCoroutine(laserRoutine);
            laserRoutine = null;
        }
        laserLine.enabled = false;
        Debug.Log("Laser deactivated!");
    }

    public override void Cancel()
    {
        StopLaser();
        playerTransform = null;
    }
}