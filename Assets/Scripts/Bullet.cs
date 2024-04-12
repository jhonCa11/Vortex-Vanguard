using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision objetWeHit)
    {
        if (objetWeHit.gameObject.tag == "Target")
        {
            print("Hit "+ objetWeHit.gameObject.name +"!");
            CreateBulletImpactEffect(objetWeHit);
            Destroy(gameObject);
        }

        if (objetWeHit.gameObject.tag == "Wall")
        {
            print("Hit a wall!");
            CreateBulletImpactEffect(objetWeHit);
            Destroy(gameObject);
        }
    }

    void CreateBulletImpactEffect(Collision objetWeHit)
    {
        //Create bullet impact effect
        ContactPoint contact = objetWeHit.contacts[0];

        GameObject hole = Instantiate(GlobalReferences.Instance.bulletImpactEffectPrefab, contact.point, Quaternion.LookRotation(contact.normal));

        hole.transform.SetParent(objetWeHit.gameObject.transform);
    }
}
