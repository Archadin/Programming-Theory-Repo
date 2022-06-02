using UnityEngine;

public class Entity : MonoBehaviour
{
    protected GameManager GM;//instance of GameManager, added dor getting rid of repetition.
    public float speed = 5.0f;
    public int health = 1;
    private int XRange = 14;

    public virtual void Start()
    {
        GM = GameManager.Instance;
    }

    public virtual void Move()
    {
        if (!GM.GameOver)
        {
            transform.position += Vector3.back * speed * Time.deltaTime;
        }
    }

    public virtual Vector3 RandomSpawn()
    {
        float x = UnityEngine.Random.Range(-XRange, XRange);

        return new Vector3(x, 0.61f, 20);
    }
}