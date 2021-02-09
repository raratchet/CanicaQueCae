using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    Animator anim;
    Rigidbody rb;
    [SerializeField]
    float bouncePOWAA;
    public BallLogic Logic { get; set;}

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        Logic.NotifyCollisionEnter(collision);
    }
    void OnCollisionExit(Collision collision)
    {
        Logic.NotifyCollisionExit(collision);
    }

    void OnTriggerEnter(Collider other)
    {
        Logic.NotifyTriggerEnter(other);
    }

    void OnTriggerStay(Collider other)
    {
        Logic.NotifyTriggerStay(other);
    }

    void OnTriggerExit(Collider other)
    {
        Logic.NotifyTriggerExit(other);
    }

    public void FreezeBall(int frameCount)
    {
        rb.velocity = Vector3.zero;
        StartCoroutine(UnfreezeBall(frameCount));
    }

    IEnumerator UnfreezeBall(int frameCount)
    {
        for(int i = 0; i < frameCount; i++)
        {
            rb.useGravity = false; // Esto lo pongo aqui por si atraviesa mas de 1 plataforma, pero he de revisar
            yield return new WaitForEndOfFrame();
        }

        rb.useGravity = true;
    }

    public void PlayAnimation(string animation)
    {
        anim.Play(animation);
    }

    public void AddBounceForce()
    {
        rb.AddForce(Vector3.up * 100 * bouncePOWAA, ForceMode.Acceleration);
    }

}
