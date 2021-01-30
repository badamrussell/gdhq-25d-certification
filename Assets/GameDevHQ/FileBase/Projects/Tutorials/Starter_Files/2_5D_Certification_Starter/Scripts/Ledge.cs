using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    [SerializeField]
    private Vector3 _handPos = new Vector3(0.53f, 67.43f, 123.5f);

    [SerializeField]
    private Vector3 _standPos = new Vector3(0.53f, 74.36f, 125.13f);

    [SerializeField]
    private Transform _standTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LedgeGrabChecker"))
        {
            Player player = other.transform.parent.GetComponent<Player>();
            if (player)
            {
                player.GrabLedge(_handPos, this);
            }
        }
    }

    public Vector3 GetStandPos()
    {
        return _standTransform.position;
        //return _standPos;
    }
}
