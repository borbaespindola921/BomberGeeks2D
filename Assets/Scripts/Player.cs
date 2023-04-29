using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;
using System;

[Serializable]
public class IntEvent : UnityEvent<int> {}

public class Player : NetworkBehaviour
{
    Rigidbody2D rb;
    float inputX;
    float inputY;
    public float speed = 3;
    
    [SyncVar]
    public int coins;
    
    [SyncVar]
    public Color playerColor;
   
    //Events
    public IntEvent OnCoinCollect;


    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
                GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>().AddPlayerListener(this);

    }


    // Update is called once per frame
    void Update()
    {
        
        if(isLocalPlayer)
        {
            inputX = Input.GetAxisRaw("Horizontal");
            inputY = Input.GetAxisRaw("Vertical");

            rb.velocity = new Vector2(inputX, inputY) * speed;
            
            if(Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Pedindo um Hamburguer ao Server");
                TalkToServer();
            }
        }
    }

    [Command]
    void TalkToServer()
    {
        Debug.Log("Player pediu um hamburguer!");
    }
}
