using UnityEngine;

public class Attack : MonoBehaviour
{

    public GameObject Melee;
    bool isAttacking = false;
    public bool attackAnim = false;
    float attackDuration = 0.3f;
    float attackTimer = 0f;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("IsAttacking", attackAnim);

        CheckMeleeTimer();

        if(Input.GetMouseButton(0))
        {
            OnAttack();
        }
    }

    void OnAttack()
    {
        if(!isAttacking)
        {
            Melee.SetActive(true);
            attackAnim = true;
            isAttacking = true;
        }
    }

    void CheckMeleeTimer()
    {
        if(isAttacking)
        {
            attackTimer += Time.deltaTime;
            if(attackTimer >= attackDuration)
            {
                attackTimer = 0;
                isAttacking = false;
                Melee.SetActive(false);
                attackAnim = false;
            }
        }
    }
}
