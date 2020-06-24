using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using utility;

/// <summary>Mono component adding a unity editor like free cam to a game object.</summary>
[RequireComponent(typeof(Camera))]
public class PlayerCameraController : MonoBehaviour
{
    #region Inspector Properties
    [Header("Movement")]
    /// <summary>WASD Camera movement speed exponent</summary>
    [Tooltip("WASD Camera movement speed exponent")]
    public float moveSpeed = 3f;

    /// <summary>Left Click Drag movement speed exponent</summary>
    [Tooltip("Left Click Drag movement speed exponent")]
    private float dragSpeed = 3f;
    
    /// <summary>Mousewheel zoom speed exponent</summary>
    [Tooltip("Mousewheel zoom speed exponent")]
    private float zoomSpeed = 2f;

    /// <summary>Right mouse button horizontal movement speed exponent</summary>
    [Tooltip("Right mouse button horizontal movement speed exponent")]
    private float lookSpeedH = 2f;
    
    /// <summary>Right mouse button vertical movement speed exponent</summary>
    [Tooltip("Right mouse button vertical movement speed exponent")]
    private float lookSpeedV = 2f;
    #endregion
    
    #region Properties
    /// <summary>Current Euler angle Y</summary>
    /// gameObject.transform.rotation.y
    private float yaw = 0f;

    /// <summary>Current Euler angle x</summary>
    /// gameObject.transform.rotation.x
    private float pitch = 0f;

    /// <summary>The camera component which this controller manages</summary>
    /// Relies on [RequireComponent] to inherited, at monobehaviour start, a camera on the same object as this controller.
    private Camera connectedCamera = null;

    /// <summary> Transform to return to after entering menu mode</summary>
    private TransformData prevTransform = new TransformData();

    /// <summary> Transform to move to when entering menu mode.</summary>
    /// As per the player camera prefab, a static child object is provided for this.
    private GameObject MenuTarget = null;

    /// <summary>Determines if camera is in menu mode.</summary>
    /// True disables key input, and may invoke idle camera rotation in the future.
    public bool menu {get; private set;} = false;

    /// <summary>Argument hashtable for itween movement when focusing camera on an entity.</summary>
    private Hashtable rabbitFocusArgs = new Hashtable()
                {
                    { "orienttopath", true },
                    { "position", null },
                    { "time", 2f }
                };
    #endregion

    #region component
    /// <summary>Monobehaviour start</summary>
    /// Corrects initial rotation and validates component configuration
    private void Start()
    {
        // Initialize the correct initial rotation
        this.yaw = this.transform.eulerAngles.y;
        this.pitch = this.transform.eulerAngles.x;
        validateConfifuration();
    }

    /// <summary>Monobehaviour update</summary>
    private void Update()
    {
        // IMPORTANT
        // Updates compatable with the camera whilst in menu mode must appear before here.

        if (menu) return;                           // If the camera is in menu mode, disallow keychecking.
        CheckInput();                               // Check for new  input,and handle.
    }

    /// <summary>Gets local references to components, and asserts the controller is correctly configured.</summary>
    private void validateConfifuration()
    {
        connectedCamera = GetComponent<Camera>();
        MenuTarget = GameObject.Find(Literals.OBJECT_PLAYER_CAM_MENU);
        Assert.IsNotNull(MenuTarget, "No camera menu location");
    }

    /// <summary>Checks for key and mouse input, and interacts with the camera accordingly.</summary>
    private void CheckInput()
    {

        #region Mouse movement
        if (Input.GetMouseButton(0))                                 // If left mouse
        {
            this.yaw = this.transform.eulerAngles.y + -(this.lookSpeedH * Input.GetAxis("Mouse X"));  // get mouse values for local use. 
            this.pitch = this.transform.eulerAngles.x + (this.lookSpeedV * Input.GetAxis("Mouse Y"));
            this.transform.eulerAngles = new Vector3(pitch, yaw, 0f);// Transform camera rotation relative to difference in angles.
        }

        //drag camera around with Middle Mouse
        if (Input.GetMouseButton(2))
            transform.Translate(-Input.GetAxisRaw("Mouse X") * Time.deltaTime * dragSpeed, -Input.GetAxisRaw("Mouse Y") * Time.deltaTime * dragSpeed, 0);

        //Zoom in and out with Right Mouse
        if (Input.GetMouseButton(1))
            this.transform.Translate(0, 0, Input.GetAxisRaw("Mouse X") * this.zoomSpeed * .07f, Space.Self);

        //Zoom in and out with Mouse Wheel
        this.transform.Translate(0, 0, Input.GetAxis("Mouse ScrollWheel") * this.zoomSpeed, Space.Self);
        #endregion

        #region WASD Lateral movement
        if (Input.GetKey(KeyCode.W))
            this.transform.Translate(0, 0, 1 * this.moveSpeed, Space.Self);

        if (Input.GetKey(KeyCode.A))
            this.transform.Translate(-(1 * this.moveSpeed), 0, 0, Space.Self);

        if (Input.GetKey(KeyCode.S))
            this.transform.Translate(0, 0, -(1 * this.moveSpeed), Space.Self);


        if (Input.GetKey(KeyCode.D))
            this.transform.Translate((1 * this.moveSpeed), 0, 0, Space.Self);
        #endregion

        #region Vertical movement
        if (Input.GetKey(KeyCode.Space))
            this.transform.Translate(0, (1 * this.moveSpeed), 0, Space.Self);

        if (Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.LeftShift))
            this.transform.Translate(0, -(1 * this.moveSpeed), 0, Space.Self);
        #endregion

        #region Smart movement

        if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.R))                 // R => rotate the camera to a target rabbit.
        {
            GameObject target = GameObject.FindWithTag(Literals.OBJECT_RABBIT); // Try to locate a rabbit by tag for all smart move
            
            if (target != null){
                if (Input.GetKey(KeyCode.E))                                   // E => Move the camera to any detected rabbit.
                    if (target != null) moveTo(target);                        // move to it,                                             
                lookAt(target);                                                // Look at it, for both rotation and movement.
            }
        }
        #endregion
    
        #region Misc
        if (Input.GetKey(KeyCode.Escape)) {
            Delight.MainMenu.instance.enterMenu();
        }
        #endregion
    }

    #endregion

    #region movement
    /// <summary>Moves the connected camera to the same position as the object parsed.</summary>
    /// <param name="target">Target object to move to<param>
    public void moveTo(GameObject target) => moveTo(target.transform);

    /// <summary>Moves the connected camera to the same position as the transform parsed.</summary>
    /// <param name="transform">Target object to move to<param>
    public void moveTo(Transform transform) => moveTo(transform.position);

    /// <summary>Moves the connected camera to the same position as the 3d vector parsed.</summary>
    /// <param name="position">Target object to move to<param>
    public void moveTo(Vector3 position)
    {
        rabbitFocusArgs.Remove("position");                             // update the arg hastable with target position
        rabbitFocusArgs.Add("position", position);
        iTween.MoveTo(gameObject, rabbitFocusArgs);                   // Parse hastable to itween to begin moving the camera.
    }
    #endregion

    #region rotation
    /// <summary>Rotates the camera to look at the object parsed.</summary>
    /// <param name="target">Target object to look at<param>
    public void lookAt(GameObject target) => lookAt(target.transform);

    /// <summary>Rotates the camera to look at the transform parsed.</summary>
    /// <param name="transform">Target transform to look at<param>
    public void lookAt(Transform transform) => lookAt(transform.position);

    /// <summary>Rotates the camera to look at the object parsed.</summary>
    /// <param name="target">Object to look at<param>
    public void lookAt(Vector3 position) => iTween.LookTo(gameObject, position, 2f);      // Look at position of target.
    #endregion

    #region Menu
    /// <summary>Moves the connected camera to it's menu position</summary>
    /// Animates smooth motion over time using itwizzy utilities
    public void MenuPosition()
    {
        menu = true;                                    // Declare menu mode active
        prevTransform = tools.Clone(transform);         // Store playtime position for returning

        // Define itwizzy hastable keys
        Hashtable args = new Hashtable();               
        args.Add("name", "PlayerCamToMenu");
        args.Add("position", MenuTarget.transform.position);
        args.Add("time", 1f);
        args.Add("looktarget", new Vector3(1, gameObject.transform.rotation.y, gameObject.transform.rotation.z));
        args.Add("looktime", 1f);

        iTween.MoveTo(gameObject, args);                      //Execute movement
    }

    /// <summary>Moves the connected camera to it's play position</summary>
    /// Animates smooth motion over time using itwizzy utilities
    public void PlayPosition()
    {
        menu = false;                                            // Declare menu mode inactive

        // Define itwizzy keys for returning
        Hashtable args = new Hashtable();                        
        args.Add("name", "PlayerCamToPlay");
        args.Add("position", prevTransform.position);
        args.Add("time", 1f);
        args.Add("looktime", 1f);
        args.Add("looktarget", new Vector3(prevTransform.rotation.x, prevTransform.rotation.y, prevTransform.rotation.z));

        iTween.MoveTo(gameObject, args);                      //Execute movement
    }
    #endregion
}
