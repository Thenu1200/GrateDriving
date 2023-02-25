using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{

    private float xIn;
    private float yIn;
    private float currSteeringAngle;
    private bool doBreak;
    private float currbreakForce;

    [SerializeField] private float maxSteeringAngle;
    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;
    [SerializeField] private Transform rearRightWheelMesh;
    [SerializeField] private Transform frontRightWheelMesh;
    [SerializeField] private Transform rearLeftWheelMesh;
    [SerializeField] private Transform frontLeftWheelMesh;

    public Rigidbody rig;

    private int totalCheese;

    public GameObject winScreen;

    public GameObject countScreen;

    
    private void Start() {
        rig.centerOfMass = new Vector3(0, -0.9f, 0);
        totalCheese = GameObject.FindGameObjectsWithTag("Cheese").Length;
        winScreen.SetActive(false);
    }
    
    private void Update() {
        if (transform.position.y <= -10f || Input.GetKey(KeyCode.R)){
            SceneManager.LoadScene(0);
        }
    }
    private void FixedUpdate() {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void HandleSteering()
    {
        currSteeringAngle = maxSteeringAngle * xIn;
        frontLeftWheelCollider.steerAngle = currSteeringAngle;
        frontRightWheelCollider.steerAngle = currSteeringAngle;
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = yIn * motorForce;
        frontRightWheelCollider.motorTorque = yIn * motorForce;
        currbreakForce = doBreak ? breakForce : 0f;
        ApplyBrakes();
    }

    private void ApplyBrakes()
    {
        frontLeftWheelCollider.brakeTorque = currbreakForce;
        frontRightWheelCollider.brakeTorque = currbreakForce;
        rearLeftWheelCollider.brakeTorque = currbreakForce;
        rearRightWheelCollider.brakeTorque = currbreakForce;
    }

    private void GetInput()
    {
        xIn = Input.GetAxis("Horizontal");
        yIn = Input.GetAxis("Vertical");
        doBreak = Input.GetKey(KeyCode.LeftShift);
    }

    private void UpdateWheels()
    {
        updateSingleWheel(frontLeftWheelCollider, frontLeftWheelMesh);
        updateSingleWheel(frontRightWheelCollider, frontRightWheelMesh);
        updateSingleWheel(rearLeftWheelCollider, rearLeftWheelMesh);
        updateSingleWheel(rearRightWheelCollider, rearRightWheelMesh);
    }

    private void updateSingleWheel(WheelCollider wheelCollider, Transform wheelMesh)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelMesh.rotation = rot;
        wheelMesh.position = pos;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Cheese"){
            totalCheese--;
            other.gameObject.SetActive(false);
        }

        if (totalCheese == 0){
            WinGame();
        }
    }

    private void WinGame()
    {
        countScreen.SetActive(false);
        winScreen.SetActive(true);
    }

    public int GetCheeseTotal()
    {
        return totalCheese;
    }
}
