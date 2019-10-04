using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Server : MonoBehaviour
{
    float timerDelay;
    float serverTranportDelay = 0.15f;
    [SerializeField]SendingClient sendingClient;
    [SerializeField]ReceivingClient ReceivingClient;
    float globalTime;

    List<ClientData> clientDataBuffer = new List<ClientData>();

    private void Start()
    {
        
    }

    void Update()
    {
        
        globalTime += Time.deltaTime;
     
        if(clientDataBuffer.Count != 0)
        {
            for (int i = clientDataBuffer.Count -1 ; i >= 0; i--)
            {
                if (clientDataBuffer[i].clientDataTime + serverTranportDelay + SendingClient.clientTransportDelay < globalTime)
                {
                    ReceivingClient.ReceiveClientData(clientDataBuffer[i]);
                    clientDataBuffer.RemoveAt(i);
                }
            }
        }  
    }

    public void AddClientValue(ClientData data)
    {
        clientDataBuffer.Add(data);
    }
}
