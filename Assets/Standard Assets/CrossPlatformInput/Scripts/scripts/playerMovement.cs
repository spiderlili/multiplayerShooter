using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]

//responsible for moving the player to drive the tank's rotating parts
public class playerMovement : NetworkBehaviour {
    Rigidbody m_rigidbody; //cache a reference to rigidbody => enable game objects to interact with physics.
    public Transform m_chassis;
    public Transform m_turret;
    public float m_playerMoveSpeed = 100f;
    public float m_chassisRotateSpeed = 1f;
    public float m_turretRotateSpeed = 3f;

    // Use this for initialization
    void Start () {
        m_rigidbody = GetComponent<Rigidbody>();
	}

    //method to move the tank => always moving forward in the direction chassis is pointing
    public void MovePlayer(Vector3 dir) {
        Vector3 moveDirection = dir * m_playerMoveSpeed * Time.deltaTime; //Value per frame deltaTime => frame rate independent.
        m_rigidbody.velocity = moveDirection; //do the movement on the rigid body's velocity to use physics on the player and move the tank forward
    }

    //method to take the transform and make it face a desired direction. rotspeed = turning speed towards desired direction
    public void FaceDirection(Transform xTransform, Vector3 dir, float rotSpeed) {
        //check - only run the code if the vector3 direction to face & transform to rotate is not 0
        if (dir != Vector3.zero && xTransform != null)
        {
            //unity uses quarternions to handle rotations => need to convert vector3 dir direction to quarternion
            //converts from the vector3 dir rotation with the specified forward and upwards directions.
            Quaternion desiredRot = Quaternion.LookRotation(dir);
            //if no axis is specified, assumes vector3 up for top down game => only need rotation around y axis
            xTransform.rotation = Quaternion.Slerp(xTransform.rotation, desiredRot, rotSpeed * Time.deltaTime); 
        } 
    }

    //public helper functions for face direction, move the player to rotate the turret & chassis  
    public void RotateChasis(Vector3 dir) {
        FaceDirection(m_chassis, dir, m_chassisRotateSpeed);
    }

    public void RotateTurret(Vector3 dir)
    {
        FaceDirection(m_turret, dir, m_chassisRotateSpeed);
    }

    // Update is called once per frame
    void Update () {
		
	}
}

