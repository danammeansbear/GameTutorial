using UnityEngine;
using System.Collections;
 
public class HeliMovement : MonoBehaviour {
    public Transform HeliFan;
    public Transform HeliTailFan;
    public float HeliFanSpeed;
    public float HeliTailFanSpeed;
    public float MaxSpeed;
    public float Acceleration;
    public float RotX;
    public float MovY;
    public float MovZ;
    public float RotY;
    public float TiltY;
    public float XRotSpeed;
    public float ZRotSpeed;
    public float hover;
    private float timer;
    private float HeliFanAcceleration = 0;
    private float HeliTailFanAcceleration = 0;
    private float CurFanSpeed = 0;
    private float CurTailFanSpeed = 0;
    float timer2;
    float timer3;
    float Zrot;
    Quaternion Z;
    public Rigidbody rb;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody> ();
    }
 
    void FixedUpdate () {
        Zrot = Mathf.LerpAngle (transform.localEulerAngles.z, 0.0f, ZRotSpeed * Time.deltaTime);
 
        Z = Quaternion.Euler (transform.localEulerAngles.x, transform.localEulerAngles.y, Zrot);
 
        rb.centerOfMass = Vector3.forward * 0.5f + Vector3.down * 2;
 
        if (Input.GetKey (KeyCode.UpArrow) && MovZ > 2000000) {
            rb.AddRelativeTorque (Vector3.right * RotX * XRotSpeed);
        }
        if (Input.GetKey (KeyCode.DownArrow) && MovZ > 2000000) {
            rb.AddRelativeTorque (Vector3.left * RotX * XRotSpeed);
        }
        if (Input.GetKey (KeyCode.LeftArrow) && MovZ > 2000000) {
            rb.AddRelativeTorque (0, -RotY, 0);
        } else {
            rb.MoveRotation (Z);
        }
        if (Input.GetKey (KeyCode.RightArrow) && MovZ > 2000000) {
            rb.AddRelativeTorque (0, RotY, 0);
        }else {
            rb.MoveRotation (Z);
        }
        if (Input.GetKey(KeyCode.W)) {
            rb.AddRelativeForce (Vector3.up * MovY);
        }
        if (Input.GetKey(KeyCode.S)) {
            rb.AddRelativeForce (Vector3.down * MovY);
        }
        if (Input.GetKey (KeyCode.Q)) {
            rb.angularVelocity = Vector3.up * TiltY;
        }
        if (Input.GetKey (KeyCode.E)) {
            rb.angularVelocity = Vector3.down * TiltY;
        }
        if (Input.GetKey (KeyCode.A)) {
            MovZ += Time.deltaTime * Acceleration;
        }
        if (Input.GetKey (KeyCode.D)) {
            MovZ -= Time.deltaTime * 1000000;
        }
        if (Vector3.Angle (Vector3.up, transform.up) >= 0 && CurFanSpeed > 25) {
            rb.AddRelativeForce (Vector3.forward * MovZ * Time.deltaTime);
        }
 
        if (CurFanSpeed > 25) {
            rb.velocity = new Vector3 (0, hover, 0);
        } else {
            rb.velocity = new Vector3 (0, 0, 0);
        }
 
        if (MovZ > MaxSpeed) {
            MovZ = MaxSpeed;
        }
        if (MovZ < 0) {
            MovZ = 0;
        }
    }
  
    // Update is called once per frame
    void Update () {
        HeliFanAcceleration = timer;
        HeliTailFanAcceleration = timer2;
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;
        timer3 += Time.deltaTime;
        if (timer >= 5) {
            timer = 5;
        }
        if (timer2 >= 5) {
            timer2 = 5;
        }
        CurFanSpeed = HeliFanSpeed * HeliFanAcceleration;
        HeliFan.transform.Rotate (0, 0, CurFanSpeed);
 
        CurTailFanSpeed = HeliTailFanSpeed * HeliTailFanAcceleration;
        HeliTailFan.transform.Rotate (CurTailFanSpeed, 0, 0);
        }
    }