using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    //Variable List.//
    public GameObject player;
    //Vector2 is ambigious with operators, uses Vector3 instead.//

    private Vector3 camera;
    //Smooth camera.//
    //Lower value = Sharper/ Faster camera.//
    private Vector2 smoothness;
    public float SmoothnessX; 
    public float SmoothnessY;
    private float xPosition;
    private float yPosition;

	void Start ()
    { 
        camera = transform.position - player.transform.position;   
	}
	
	// Update is called once per frame
	void Update ()
    {
        xPosition = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref smoothness.x, SmoothnessX);
        yPosition = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref smoothness.y, SmoothnessY);
        transform.position = new Vector3(xPosition, yPosition, transform.position.z);
	}
}
