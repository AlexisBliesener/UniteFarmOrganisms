using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField]
    Drop nextDrop;


    private GameObject spawnParent;

    public enum Type
    {
        chicken,
        rabbit,
        dog,
        sheep,
        pig,
        cow
    };

    [SerializeField]
    private Type type = Type.chicken;

    public Type GetDropType() { return type; }

    public void SetDropType(Type newType) { type = newType; }

    private void Start()
    {
        spawnParent = GameObject.Find("Spawn Parent");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Drop>())
        {
            Drop drop = collision.gameObject.GetComponent<Drop>();

            if (drop.GetDropType() == type)
            {
                if(drop.GetComponent<Rigidbody2D>().velocity.magnitude > GetComponent<Rigidbody2D>().velocity.magnitude)
                {
                    Drop newDrop = Instantiate(nextDrop, transform.position, transform.rotation, spawnParent.transform);

                    newDrop.SetDropType(++type);

                    Destroy(drop.gameObject);
                    Destroy(this.gameObject);
                }
            }
        }
    }


}
