using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float health = 10;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sword"))
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            {
                health -= 5;
                anim.Play("Hurt");
                transform.position -= Vector3.forward;
                if (health <= 0)
                {
                    StartCoroutine(Death());
                }
            }
        }
    }
    
    IEnumerator Death()
    {
        anim.Play("Death");
        GameController.singleton.enemyCount -= 1;
        yield return new WaitForSeconds(.2f);
        gameObject.SetActive(false);
    }
}
