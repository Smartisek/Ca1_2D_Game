using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class ThrowingKnifeScript : MonoBehaviour
{

    [SerializeField] private float speed;
    private bool hit;
    private BoxCollider2D boxCollider;
    private Animator anim;
    private float direction;
    private float lifeTime;
    
    private void Awake(){

        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update(){
        if(hit) return;

        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifeTime += Time.deltaTime;
        if(lifeTime > 2){
            gameObject.SetActive(false);
        }
    }

private void OnTriggerEnter2D(Collider2D collision){

    hit = true;
    boxCollider.enabled = false;
    anim.SetTrigger("explode");
}

public void SetDirection(float _direction){
    lifeTime =0;
    gameObject.SetActive(true);
    boxCollider.enabled = true;
    direction = _direction;
    hit = false;

    float localScaleX = transform.localScale.x;
         if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
}

private void Deactivate(){
    gameObject.SetActive(false);
}

}
