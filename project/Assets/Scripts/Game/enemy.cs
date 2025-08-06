using UnityEngine;

public class enemy : MonoBehaviour
{
    public float speed = 5f;
    Transform target;
    Rigidbody2D rb;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        transform.localScale = new Vector3(target.position.x < transform.position.x ? -1 : 1,1,1);
        //if (target.position.x < transform.position.x)
        //{
        //    transform.localScale = new Vector3(-1, 1, 1);
        //}
        //else
        //{
        //    transform.localScale = new Vector3(1, 1, 1);
        //}

        rb.MovePosition(Vector2.MoveTowards(rb.position, target.position, speed * Time.fixedDeltaTime));
        //transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
    }
}
