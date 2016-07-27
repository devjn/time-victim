using UnityEngine;

/// <summary>
/// Handle hitpoints and damages
/// </summary>
public class HealthScript : MonoBehaviour
{
    /// <summary>
    /// Total hitpoints
    /// </summary>
    public int hp = 100;

    /// <summary>
    /// Enemy or player?
    /// </summary>
    public bool isEnemy = true;

    /// <summary>
    /// Inflicts damage and check if the object should be destroyed
    /// </summary>
    /// <param name="damageCount"></param>
    public void Damage(int damageCount)
    {
        hp -= damageCount;

        if (hp <= 0)
        {
            // Dead!
            GameManager.instance.GameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // Is this a shot?
        AttackScript attack = otherCollider.gameObject.GetComponent<AttackScript>();
        if (attack != null)
        {
            // Avoid friendly fire
            if (attack.isEnemyAttack != isEnemy)
            {
                Damage(attack.damage);

                // Destroy the shot
                Destroy(attack.gameObject); // Remember to always target the game object, otherwise you will just remove the script
            }
        }
    }
}