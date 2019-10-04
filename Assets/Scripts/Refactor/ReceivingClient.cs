using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceivingClient : MonoBehaviour
{

    Vector2 velocity;
    Rigidbody2D body;
    float globalTime = 0.0f;
    const float lerpDelay = 0.6f;

    List<ClientData> clientDataBuffer = new List<ClientData>();

    float realGlobalTime;


    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        globalTime += Time.deltaTime;
        realGlobalTime = globalTime - lerpDelay;

        if (clientDataBuffer.Count >= 2)
        {
            for (int i = 0; i < clientDataBuffer.Count; i++)
            {
                if (clientDataBuffer[i].clientDataTime < realGlobalTime && clientDataBuffer[i + 1].clientDataTime > realGlobalTime)
                {
                    float time0 = clientDataBuffer[i].clientDataTime;
                    float time1 = clientDataBuffer[i + 1].clientDataTime;

                    float period = time1 - time0;
                    float delta = realGlobalTime - time0;
                    float DeltaTime = delta / period;

                    Vector2 pos0 = clientDataBuffer[i].pos;
                    Vector2 pos1 = clientDataBuffer[i + 1].pos;

                    transform.position = Vector2.Lerp(pos0, pos1, DeltaTime);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        for (int i = clientDataBuffer.Count - 1; i >= 0; i--)
        {
            if (clientDataBuffer[i].clientDataTime + lerpDelay * 1.5f < globalTime)
            {
                clientDataBuffer.RemoveAt(i);
            }
        }
        /* var dir = body.velocity;
         var angle = Mathf.Atan2(dir.x,dir.y) * Mathf.Rad2Deg;
         body.MoveRotation(angle);*/

    }

    public void ReceiveClientData(ClientData data)
    {
        clientDataBuffer.Add(data);
    }

}
