using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    [SerializeField] LineRenderer laser;
    [SerializeField] Transform shootPoint;

    [SerializeField] GameObject deathParticles;

    [SerializeField] bool shootLeft = true;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShootLaser();
    }

    void ShootLaser(){
        RaycastHit2D hit2D = Physics2D.Raycast(shootPoint.position, transform.right);
        if(shootLeft)
            hit2D = Physics2D.Raycast(shootPoint.position, Vector2.left);
        if(hit2D){
            DrawRay2D(shootPoint.position, hit2D.point);
            // print(hit2D.point);
            if(hit2D.collider.CompareTag("Player")){
                GameManager.Instance.Respawn();
            }
        }else{
            DrawRay2D(shootPoint.position, transform.right * 100f);
        }

    }

    void DrawRay2D(Vector2 startPos, Vector2 endPos){
        laser.SetPosition(0, startPos);
        laser.SetPosition(1, endPos);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
        if(rb != null){
            if(rb.velocity.magnitude > Vector2.zero.magnitude){
                Death();
            }
            print(rb.velocity.magnitude);
        }
        if(GetComponent<Rigidbody2D>().velocity.magnitude > Vector2.one.magnitude){
            Death();
        }
    }

    void Death(){
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
