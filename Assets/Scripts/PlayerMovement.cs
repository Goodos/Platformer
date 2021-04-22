using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    private Rigidbody2D rb;
    private Animator anim;
    private float runSpeed = 2f;
    private float jumpSpeed = 260f;
    private bool isGrounded = true;
    private bool canMove = true;
    private Vector2 playerDirection;

    [SerializeField] Button attackButton;
    [SerializeField] Button rangeAttackButton;

    private enum StateAnim
    {
        start,
        hold,
        back
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameController.singleton.endGameEvent += StopMovement;
        attackButton.onClick.AddListener(Attack);
        rangeAttackButton.onClick.AddListener(RangeAttack);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            anim.Play("Idle");
            isGrounded = true;
        }
    }

    void Update()
    {
        if (canMove)
        {
#if UNITY_EDITOR
            MovementKeyboard();
#endif
#if UNITY_ANDROID
            MovementTouch();
#endif
        }
    }

    void MovementTouch()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            return;
        else
        {
            if (joystick.Horizontal != 0)
            {
                Move(StateAnim.hold, new Vector3(joystick.Horizontal, 0));
            }
            if (joystick.Horizontal == 0)
            {
                Move(StateAnim.back, Vector3.zero);
            }
            if (joystick.Vertical > .3f)
            {
                Jump();
            }
            if (joystick.Vertical < -.3f)
            {
                Crouch(StateAnim.start);
            }
            if (joystick.Vertical == 0)
            {
                Crouch(StateAnim.back);
            }
        }
    }

    void MovementKeyboard()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            return;
        else
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                Move(StateAnim.hold, new Vector3(Input.GetAxis("Horizontal"), 0));
            }
            if (Input.GetAxis("Horizontal") == 0)
            {
                Move(StateAnim.back, Vector3.zero);
            }
            if (Input.GetAxis("Vertical") > .3f)
            {
                Jump();
            }
            if (Input.GetAxis("Vertical") < -.3f)
            {
                Crouch(StateAnim.start);
            }
            if (Input.GetAxis("Vertical") == 0)
            {
                Crouch(StateAnim.back);
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            RangeAttack();
        }
    }

    void Flip(Transform transform, Vector3 vector)
    {
        bool bLeft = vector.x >= 0 ? false : true;
        if (transform == gameObject.transform)
            transform.localScale = new Vector3(bLeft ? .5f : -.5f, .5f, 1);
        else
            transform.localScale = new Vector3(bLeft ? -.1f : .1f, .2f, 1);
    }

    void Move(StateAnim _stateAnim, Vector3 vector)
    {
        if (_stateAnim == StateAnim.back)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            {
                anim.Play("Idle");
                return;
            }
        }
        if (_stateAnim == StateAnim.hold)
        {
            Flip(transform, vector);
            playerDirection = vector;
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Sit"))
            {
                transform.Translate(vector * runSpeed * Time.deltaTime);
            }
            if (isGrounded && !anim.GetCurrentAnimatorStateInfo(0).IsName("Sit") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            {
                anim.Play("Run");
            }
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            anim.Play("Jump");
            rb.AddForce(Vector2.up * jumpSpeed);
            isGrounded = false;
        }
    }

    void Attack()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            anim.Play("Attack");
        }
    }

    void Crouch(StateAnim _stateAnim)
    {
        if (_stateAnim == StateAnim.start)
        {
            if (isGrounded)
            {
                anim.Play("Sit");
            }
        }
        if (_stateAnim == StateAnim.back)
        {
            if (isGrounded)
            {
                anim.Play("Idle");
            }
        }
    }

    void RangeAttack()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            anim.Play("Attack");
            GameObject slashPref = Resources.Load("SwordSlash") as GameObject;
            GameObject slash = Instantiate(slashPref);
            slash.transform.position = transform.position;
            Flip(slash.transform, playerDirection);
            slash.GetComponent<Rigidbody2D>().AddForce(playerDirection * 200f);
        }        
    }

    void StopMovement()
    {
        canMove = false;
    }
}
