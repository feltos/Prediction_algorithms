using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Server : MonoBehaviour
{
    float timerDelay;
    const float transportDelay = 0.05f;
    //public Vector2 baseClientPos;
    Vector2 clientPos;
    float timeClient1;
    [SerializeField]Client2 client2;

    void Start()
    {
        
    }

    void Update()
    {
        if (timerDelay >= transportDelay)
        {
            SendDatasToClients();
        }
    }

    private void FixedUpdate()
    {
        
    }

    public void SetClientValue(Vector2 position, float time)
    {
        clientPos = position;
        timeClient1 = time;
        timerDelay += Time.deltaTime;
    }

    public void SendDatasToClients()
    {
        client2.SetOthersPosition(clientPos,timeClient1);
        timerDelay = 0;
    }
}
