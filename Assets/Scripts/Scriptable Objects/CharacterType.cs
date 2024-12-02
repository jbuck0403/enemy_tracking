using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterType", menuName = "Characters/Character Type", order = 2)]
public class CharacterType : ScriptableObject
{
    [SerializeField]
    int maxHealth = 10;

    [SerializeField]
    float decelerationRate = 1f;

    [SerializeField]
    float decelerationFloor = 1f;

    [SerializeField]
    float speed = 5f;

    [SerializeField]
    float maxSpeed = 5f;

    [SerializeField]
    float acceleration = 5f;

    [SerializeField]
    float maxRotationSpeed = 360f;

    [SerializeField]
    float detectionRange = 30f;

    [SerializeField]
    float detectionRangeTolerance = 3f;

    [SerializeField]
    DamageHandlerStrategy damageHandlerStrategy;

    // Public read-only properties
    public int MaxHealth => maxHealth;
    public float DecelerationRate => decelerationRate;
    public float DecelerationFloor => decelerationFloor;
    public float Speed => speed;
    public float MaxSpeed => maxSpeed;
    public float Acceleration => acceleration;
    public float MaxRotationSpeed => maxRotationSpeed;
    public float DetectionRange => detectionRange;
    public float DetectionRangeTolerance => detectionRangeTolerance;
    public DamageHandlerStrategy DamageHandlerStrategy => damageHandlerStrategy;
}
