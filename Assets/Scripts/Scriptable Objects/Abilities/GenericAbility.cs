using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Generic Ability")]
public class GenericAbility : ScriptableObject
{
    public float cost;
    public float duration;
    public ExtendedFloatValue playerMana;
    public Signal manaSignal;

    // necesita animator?
    public virtual void Ability(Vector2 playerPosition, Vector2 facingDirection = default, Animator playerAnimator = null, Rigidbody2D playerRigidbody = null) { }
}