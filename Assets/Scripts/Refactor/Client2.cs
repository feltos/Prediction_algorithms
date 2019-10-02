using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client2 : MonoBehaviour
{

    Vector2 velocity;
    Rigidbody2D body;
    float timeGlobal;

    Dictionary<float, Vector2> otherPositions = new Dictionary<float, Vector2>();

    private void Start()
    {
        
    }

    private void Update()
    {
        timeGlobal += Time.deltaTime;

        Debug.Log("KEY = " + otherPositions.Keys.ToString());
        Debug.Log("VALUE = " + otherPositions.Values.ToString());
    }

    private void FixedUpdate()
    {
        
    }

    public void SetOthersPosition(Vector2 position, float timeOthers)
    {
        otherPositions.Add(timeOthers, position);
    }

}
