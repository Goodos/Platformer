using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashController : MonoBehaviour
{
    public Vector2 direction;
    private float speed = 5f;

    void Update()
    {
        if (transform.position.x >= 10f || transform.position.x <= -10f)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
