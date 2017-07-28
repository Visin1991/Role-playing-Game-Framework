using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

    bool isFalling = false;
    float downSpeed = 0;
    float yPosition = 2.23f;
    public float seconds = 3f;


    void OnTriggerEnter(Collider collider)
    {
       
        if (collider.gameObject.tag == "Enemy")
        {
            
            
            //gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
            isFalling = true;
            StartCoroutine(Respawn());
        }
    }
    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {

           
            isFalling = true;
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        
        yield return new WaitForSeconds(seconds);
        isFalling = false;
        downSpeed = 0;
        transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (isFalling)
        {

            downSpeed += Time.deltaTime / 10;
            transform.position = new Vector3(transform.position.x,
                transform.position.y - downSpeed, transform.position.z);
        }

    }
    
}
