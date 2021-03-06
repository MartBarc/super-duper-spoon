using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMove : MonoBehaviour
{
    public float speed;
    public Transform baseTransform;
    public bool isAtBase = false;
    public float hitPoints;
    public float maxHitPoints = 5;
    public HealthBar healthbar;
    //public Vector3 healthbarOffset;
    public float attackSpeed = 1f;
    public float attackDamage = 1f;
    public bool canAttack = true;
    public AudioSource attackSound;

    private void Start()
    {
        hitPoints = maxHitPoints;
        healthbar.SetHealth(hitPoints, maxHitPoints);
        //healthbar.transform.position = new Vector3(0f, healthbarOffset);
    }

    void FixedUpdate()
    {
        if (!isAtBase)
        {
            transform.position = Vector2.MoveTowards(transform.position, baseTransform.position, speed * Time.deltaTime);
        }
        if (isAtBase)
        {
            if (canAttack)
            {
                attackSound.Play();
                canAttack = false;
                baseTransform.gameObject.GetComponent<baseManager>().TakeHit(attackDamage);
                StartCoroutine(attackCooldown());
                return;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Base")
        {
            isAtBase = true;
        }
        //isAtBase = true;
        //Debug.Log("collision name = " + collision.gameObject.name);
        if (collision.gameObject.tag == "damageEnemy")
        {
            TakeHit(collision.gameObject.GetComponent<bullet>().damage);
        }
    }

    public void TakeHit(float damage)
    {
        hitPoints -= damage;
        healthbar.SetHealth(hitPoints, maxHitPoints);
        if (hitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator attackCooldown()
    {
        yield return new WaitForSecondsRealtime(1);
        canAttack = true;
    }
}
