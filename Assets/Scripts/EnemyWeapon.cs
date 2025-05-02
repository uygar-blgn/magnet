using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public float damage = 1;
    public bool isAttacking = false;
    bool attackAnim = false;
    float attackDuration = 0.3f;
    float attackTimer = 0f;
    public Animator anim;

    private void Update()
    {
        anim.SetBool("IsAttacking", attackAnim);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackDuration)
        {
            attackTimer = 0;
            isAttacking = false;
            attackAnim = false;
        }

        Player player = collision.GetComponent<Player>();
        if (player != null && !isAttacking)
        {
            //spriteRenderer.enabled = true;
            isAttacking = true;
            attackAnim = true;
            player.TakeDamage(damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //spriteRenderer.enabled = false;
        attackAnim = false;
    }
}