using UnityEngine;

public class Player : Entity
{
    // INHERITANCE from Entity

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        speed = 5.0f;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!GM.GameOver)
        {
            Move();
        }
    }

    //Polymorphism
    public override void Move()
    {
        var Vertical = Input.GetAxis("Vertical");
        var Horizontal = Input.GetAxis("Horizontal");

        var movement = new Vector3(Horizontal, 0, Vertical).normalized;

        transform.position += speed * movement * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Limit"))
        {
            transform.position = new Vector3(transform.position.x, .61f, transform.position.z);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health--;
            GameManager.Instance.GameOver = true;
            collision.gameObject.SetActive(false);
        }
    }
}