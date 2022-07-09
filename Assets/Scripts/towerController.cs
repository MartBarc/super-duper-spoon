using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerController : MonoBehaviour
{

    [SerializeField]
    public static List<GameObject> enemyList = new List<GameObject>();

    public float attackSpeed = 1f;
    public float attackDamage = 1f;
    public bool canAttack = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Debug.Log("enemy entered");
            enemyList.Add(collision.gameObject);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Debug.Log("enemy exit");
            enemyList.Remove(collision.gameObject);
        }

    }

    private void Update()
    {
        int listnumber = 0;
        foreach (GameObject item in enemyList)
        {
            
            Debug.Log("enemy " + listnumber + ": " + item.name);
            listnumber++;
        }
    }

    private void FixedUpdate()
    {
        if (canAttack && enemyList.Count > 0)
        {
            canAttack = false;
            enemyList[0].gameObject.GetComponent<enemyMove>().TakeHit(attackDamage);
            StartCoroutine(attackCooldown());
            return;
        }
    }

    IEnumerator attackCooldown()
    {
        yield return new WaitForSecondsRealtime(1);
        canAttack = true;
    }




    //public static List<GameObject> enemyList = new List<GameObject>();
    //public Collider2D[] debugEnemyArray;
    //public LayerMask enemyMask;

    //private void Update()
    //{
    //    Collider2D[] enemyArray = Physics2D.OverlapCircleAll(transform.position, 5f, enemyMask);
    //    //foreach (Collider2D collider2D in enemyArray)
    //    //{
    //    //    if (collider2D.gameObject.tag == "Enemy")
    //    //    {
    //    //        //Debug.Log("enemy entered");
    //    //        enemyList.Add(collider2D.gameObject);
    //    //    }
    //    //}
    //    debugEnemyArray = enemyArray;
    //}

}
