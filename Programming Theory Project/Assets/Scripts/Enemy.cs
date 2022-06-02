using UnityEngine;
using UnityEngine.Events;

public class Enemy : Entity
{
    public UnityEvent OnDisableEvent = new UnityEvent();
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        speed = 8f + GameManager.Instance.Wave * .25f;
    }

    private void OnEnable()
    {
        transform.position = RandomSpawn();
    }
    private void OnDisable()
    {
        OnDisableEvent.Invoke();
        OnDisableEvent.RemoveAllListeners();
    }
    // Update is called once per frame
    private void Update()
    {
        if (!GM.GameOver)
        {
            Move();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sensor"))
        {
            gameObject.SetActive(false);
            GM.DeadEnemyCount++;
        }
    }
}