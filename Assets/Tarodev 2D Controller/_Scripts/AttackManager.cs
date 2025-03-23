using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public int damage = 25;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collision object tag: " + other.tag);
        // Check if the object has the "isMovable" tag
        if(other.CompareTag("isEnemie")){
            EnemyStatManager enemy_stat = other.GetComponent<EnemyStatManager>();
            enemy_stat.ApplyDamage(damage);
        }
    }
}
