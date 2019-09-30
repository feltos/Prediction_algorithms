using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client_Player : MonoBehaviour
{
    Vector2 velocity;
    Rigidbody2D rigidbody;
    public Vector2 serverPos;
    public float serverSpeed;

    Vector3 offset = new Vector3(1.0f,1.0f,0.0f);
    [SerializeField]Server_Player server_player;
    float timer;
    const float period = 0.1f;

    public List<Vector2> serverPositions = new List<Vector2>(2);


    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        timer = 0;
    }

    void Update()
    {
        //Transport
        timer += Time.deltaTime;

        if(timer >= period)
        {
            server_player.StartCoroutine(server_player.SendDatas());
            timer = 0;
        }
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(serverPositions[0],serverPositions[1], Time.deltaTime * serverSpeed);

        var dir = server_player.GetVelocity();
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rigidbody.MoveRotation(angle);
    }
}
