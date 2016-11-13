using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {

    public float groundDist;
    public float moveSpeed;
    public float rotSpeed;
    public float moveInterpolation;
    public float rotInterpolation;
    public float screenOffset;

    private Vector3 newPos;
    private Vector3 newRot;
    private float groundAdjustment;

	// Use this for initialization
	void Start () {
        newPos = this.transform.position;
        newRot = this.transform.rotation.eulerAngles;
    }
	
	// Update is called once per frame
	void Update () {
        WASD();
        MouseRotate();
        //CheckMousePosition();

        Move();
        Rotate();
    }

    void FixedUpdate()
    {
        CheckGroundDistance();
    }

    /// <summary>
    /// This method is in charge of adjusting the target position to move towards when the mouse touches or gets near to the edge of the screen
    /// </summary>
    private void CheckMousePosition()
    {
        // Move the camera right
        if (Input.mousePosition.x >= Screen.width - screenOffset)
        {
            newPos.x += moveSpeed;
        }

        // Move the camera left
        if (Input.mousePosition.x <= 0 + screenOffset)
        {
            newPos.x -= moveSpeed;
        }

        // Move the camera forward
        if (Input.mousePosition.y >= Screen.height - screenOffset)
        {
            newPos.z += moveSpeed;
        }

        // Move the camera backwards
        if (Input.mousePosition.y <= 0 + screenOffset)
        {
            newPos.z -= moveSpeed;
        }
    }

    /// <summary>
    /// WASD controls
    /// </summary>
    private void WASD()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if(horizontal < 0)
        {
            newPos -= (this.transform.right * .5f);
        }

        if(horizontal > 0)
        {
            newPos += (this.transform.right * .5f);
        }

        if(vertical < 0)
        {
            newPos -= (this.transform.forward * .5f);
            //newPos.y = this.transform.position.y;
        }

        if(vertical > 0)
        {

            newPos += (this.transform.forward * .5f);
            //newPos.y = this.transform.position.y;
        }

    }

    /// <summary>
    /// This handles the mouse going towards the edge of the screen and changes our desired quaternion
    /// </summary>
    private void MouseRotate()
    {
        // Move the camera right
        if (Input.mousePosition.x >= Screen.width - screenOffset)
        {
            newRot = this.transform.rotation.eulerAngles;
            newRot.y += rotSpeed;
            
        }

        // Move the camera left
        if (Input.mousePosition.x <= 0 + screenOffset)
        {
            newRot = this.transform.rotation.eulerAngles;
            newRot.y -= rotSpeed;
        }
    }

    /// <summary>
    /// This will check our distance to the ground and move us accordingly
    /// </summary>
    private void CheckGroundDistance() {
        Ray ray = new Ray(this.transform.position, Vector3.down);
        RaycastHit hit;
        float currentDistace = 0;
        if(Physics.Raycast(ray, out hit, 100f))
        {
            currentDistace = this.transform.position.y - hit.point.y;
        }

        groundAdjustment = groundDist - currentDistace;
        newPos = new Vector3(newPos.x, groundAdjustment, newPos.z);
    }

    /// <summary>
    /// This handles our actual rotation of the camera
    /// </summary>
    private void Rotate()
    {
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(newRot), rotInterpolation);
    }

    /// <summary>
    /// We move the camera here with lerping it
    /// </summary>
    private void Move()
    {
        //transform.position = Vector3.Lerp(this.transform.position, newPos, interpolation * Time.deltaTime);
        transform.position = Vector3.Lerp(this.transform.position, newPos, moveInterpolation);
    }
}
