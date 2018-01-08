using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//ensure components exist at all time for the player
[RequireComponent(typeof(PlayerShoot))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(playerMovement))]
[RequireComponent(typeof(PlayerSetup))]

//tracks player name, id, colour etc for networking
public class PlayerSetup : MonoBehaviour {

    PlayerHealth m_playerHealth;
    playerMovement m_playerMovement;
    PlayerShoot m_playerShoot;
    PlayerSetup m_playerSetup;

    // Use this for initialization
    void Start () {
        m_playerHealth = GetComponent<PlayerHealth>();
        m_playerMovement = GetComponent<playerMovement>();
        m_playerShoot = GetComponent<PlayerShoot>();
        m_playerSetup = GetComponent<PlayerSetup>();
    }

    //check project input settings  for correct axes
    Vector3 GetInput() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        return new Vector3(h, 0, v); //horizontal = x coordinate, vertical = z coordinate
    }

    //invoke the the playerMovement's MovePlayer method inside fixedUpdate since using rigidbody.velocity
    void FixedUpdate()
    {
        Vector3 inputDirection = GetInput();
        m_playerMovement.MovePlayer(inputDirection); //invoke 
    }

    // Update is called once per frame
    void Update () {
        //rotate the chassis to the input direction
        //rotate the turret to the mouse pointer
        //translate the tank forward towards the front of the chassis
        Vector3 inputDirection = GetInput();
        if (inputDirection.sqrMagnitude > 0.25f) {
            //invoke the playerMovement's rotate chassis method and pass in the input direction
            m_playerMovement.RotateChasis(inputDirection);
        }
    }
}
