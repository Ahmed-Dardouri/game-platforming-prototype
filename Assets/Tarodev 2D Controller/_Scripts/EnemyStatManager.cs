using UnityEngine;

public class EnemyStatManager : MonoBehaviour
{
    public static int maxHealth = 100; /* must be positive */ 
    public int health = maxHealth;

    public void ApplyDamage(int damage){
        health -= damage;
        if(health <= 0){
            Destroy(transform.parent.gameObject);
        }
    }
}
