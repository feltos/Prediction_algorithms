using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Server_Player : MonoBehaviour
{
    float speed;
    Vector2 velocity;
    Rigidbody2D rigidbody;
    Vector2 position;
    [SerializeField]Client_Player client_player;

    const float transportDelay = 0.1f;
    float randomSpeed;
    float randomVelocity;
    float randomDirectionDelay;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(ChangeDirection());
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(velocity.x * speed, velocity.y * speed);

        var dir = rigidbody.velocity;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rigidbody.MoveRotation(angle);
    }

    IEnumerator ChangeDirection()
    {
        randomSpeed = Random.Range(1, 5);
        randomVelocity = Random.Range(-1, 2);
        randomDirectionDelay = Random.Range(1, 3);
        speed = randomSpeed;
        velocity.x = randomVelocity;
        velocity.y = randomVelocity;
        yield return new WaitForSeconds(randomDirectionDelay);
        StartCoroutine(ChangeDirection());
    }

     public IEnumerator SendDatas()
    {
        yield return new WaitForSeconds(transportDelay);//Emission
        position = transform.position;
        client_player.serverSpeed = speed;
        if(client_player.serverPositions.Count >= 2)
        {
            client_player.serverPositions.Remove(client_player.serverPositions[0]);
        }
        client_player.serverPositions.Add(position);
    }

    public Vector2 GetVelocity()
    {
        return rigidbody.velocity;
    }


}
