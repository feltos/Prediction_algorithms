using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client1 : MonoBehaviour
{

    float speed = 5;
    Vector2 velocity;
    Rigidbody2D body;
    Vector2 position;

    float timerDelay = 0;
    const float transportDelay = 0.05f;
    int randomDirectionDelay;

    float timeGlobal;

    [SerializeField]Server server;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        StartCoroutine(ChangeDirection());
        position = transform.position;
        //server.baseClientPos = position;
    }

    void Update()
    {
        timeGlobal += Time.deltaTime;
        timerDelay += Time.deltaTime;
        Debug.Log(timeGlobal);
        if(timerDelay >= transportDelay)
        {
            SendDatasToServer();
        }
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(velocity.x * speed, velocity.y * speed);

        var dir = body.velocity;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        body.MoveRotation(angle);
    }

    IEnumerator ChangeDirection()
    {
        randomDirectionDelay = Random.Range(1, 3);

        velocity.x = Random.Range(-1, 2);
        velocity.y = Random.Range(-1, 2);

        yield return new WaitForSeconds(randomDirectionDelay);

        StartCoroutine(ChangeDirection());
    }

    void SendDatasToServer()
    {
        position = transform.position;
        server.SetClientValue(position,timeGlobal);
        timerDelay = 0;
    }

}
