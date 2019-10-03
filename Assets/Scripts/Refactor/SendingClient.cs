using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public struct ClientData 
{
    public float clientDataTime;
    public Vector2 pos;
}


public class SendingClient : MonoBehaviour
{   
    float speed = 10;
    Vector2 velocity;
    Rigidbody2D body;
    Vector2 position;

    public const float clientTransportDelay = 0.05f;

    float emissionTimer = 0;
    const float emissionRate = 0.05f;

    int randomDirectionDelay;

    float globalTime = 0.0f;

    [SerializeField]Server server;

    List<ClientData> clientDataBuffer = new List<ClientData>();

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        StartCoroutine(ChangeDirection());
        position = transform.position;
    }

    void Update()
    {
        globalTime += Time.deltaTime;
        emissionTimer += Time.deltaTime;

        if(emissionTimer >= emissionRate)
        {
            PutDatasInPacket();
        }

        if (clientDataBuffer.Count != 0)
        {
            for (int i = clientDataBuffer.Count -1 ; i >= 0; i--)
            {
                if (clientDataBuffer[i].clientDataTime + clientTransportDelay < globalTime)
                {
                    server.AddClientValue(clientDataBuffer[i]);
                    clientDataBuffer.RemoveAt(i);
                }
            }
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

    void PutDatasInPacket()
    {
        position = transform.position;

        ClientData clientData;
        clientData.clientDataTime = globalTime;
        clientData.pos = position;

        clientDataBuffer.Add(clientData);
        emissionTimer = 0;
    }

    public Vector2 GetClientVelocity()
    {
        return body.velocity;
    }
}
