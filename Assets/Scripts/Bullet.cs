using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            print("Hit "+ collision.gameObject.name +"!");
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Wall")
        {
            print("Hit a wall!");
            Destroy(gameObject);
        }
    }
}
