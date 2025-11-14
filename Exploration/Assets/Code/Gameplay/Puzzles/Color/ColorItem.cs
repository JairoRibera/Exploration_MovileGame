using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class ColorItem : MonoBehaviour
{
    public ColorType colorType;
    private Rigidbody rb;
    private Collider col;
    public float followSpeed = 10f;
    public Transform followTarget; // Punto al que seguir
    private bool isFollowing = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        //posPickItem = GameObject.FindWithTag("Enemy");
    }
    private void FixedUpdate()
    {
        //if (isFollowing && followTarget != null)
        //{
        //    Vector3 direction = (followTarget.position - transform.position);
        //    rb.velocity = direction * followSpeed;

        //    // Si quieres que rote también:
        //    Quaternion targetRotation = followTarget.rotation;
        //    rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * followSpeed));
        //}
    }

    public void Pick(Transform _player)
    {
        col.enabled = true; // NO desactivar el collider
        rb.isKinematic = false; // Dejar que las físicas funcionen
        followTarget = _player;
        isFollowing = true;
        gameObject.layer = LayerMask.NameToLayer("PickItem");
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("PickItem"), true);
    }

    //public void Drop()
    //{
    //    isFollowing = false;
    //    followTarget = null;
    //    rb.velocity = Vector3.zero;
    //    rb.angularVelocity = Vector3.zero;
    //    gameObject.layer = LayerMask.NameToLayer("Default");
    //    Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("PickItem"), false);
    //}
}
