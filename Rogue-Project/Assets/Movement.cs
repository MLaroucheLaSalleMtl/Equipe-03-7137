using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D player;
    private Vector2 velocity = new Vector2(0,0);
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        velocity.x = Input.GetAxisRaw("Horizontal");
        velocity.y = Input.GetAxisRaw("Vertical");
    }
    void FixedUpdate()
    {
        player.MovePosition(player.position + velocity * Time.fixedDeltaTime); 
    }
}
