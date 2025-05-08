using UnityEngine;

/// <summary>
/// Clase controlable por el jugador que hereda de Carrier.
/// Incorpora sistemas de energía, experiencia y habilidades.
/// </summary>
public class Playable : Carrier
{
    [Header("Sistemas del jugador")]
    [SerializeField] protected EnergySystem energiaSystem;
    [SerializeField] protected XpSystem xpSystem;
    [SerializeField] protected SkillSystem skillSystem;

    [Header("Movimiento 3D")]
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody rb;
    private Vector3 movementInput;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void Update()
    {
        //HandleInput(); // Captura de input del usuario
    }

    // private void FixedUpdate()
    // {
    //     MoveCharacter(); // Aplicación del movimiento con física
    // }

    /// <summary>
    /// Captura el input en 3D (X, Z).
    // /// </summary>
    // private void HandleInput()
    // {
    //     float moveX = Input.GetAxisRaw("Horizontal"); // Movimiento lateral (A/D)
    //     float moveZ = Input.GetAxisRaw("Vertical");   // Movimiento frontal (W/S)

    //     movementInput = new Vector3(moveX, 0f, moveZ).normalized;
    // }

    /// <summary>
    /// Mueve al personaje usando Rigidbody para un control físico.
    /// </summary>
    // private void MoveCharacter()
    // {
    //     Vector3 moveVelocity = movementInput * moveSpeed;
    //     rb.linearVelocity = new Vector3(moveVelocity.x, rb.linearVelocity.y, moveVelocity.z); 
    // }

    /// <summary>
    /// Intenta usar una habilidad según el índice.
    /// </summary>
    public bool TryUseSkill(int skillIndex)
    {
        Debug.Log(skillSystem.Skills.Count);
        if (skillIndex < 0 || skillIndex >= skillSystem.Skills.Count) return false;

        Skill skill = skillSystem.Skills[skillIndex];
        if (energiaSystem.TryUseEnergy(skill.energyCost))
        {
            skill.Use();
            return true;
        }
        return false;
    }
}
