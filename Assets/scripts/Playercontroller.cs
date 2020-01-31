using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    private Rigidbody playerRB;
    private GameObject focalPoint;
    public float speed = 5.0f;
    public bool hasPowerup;
    private float powerupStrength = 15f;
    public GameObject powerupIndicator;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardsInput = Input.GetAxis("Vertical");
        playerRB.AddForce(focalPoint.transform.forward * speed * forwardsInput);
        
        
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromplayer = (collision.gameObject.transform.position - transform.position);
            Debug.Log("player colided with" + collision.gameObject.name + "with powerup set to" + hasPowerup);
            enemyRigidbody.AddForce(awayFromplayer * powerupStrength, ForceMode.Impulse);
        }
    }
}

