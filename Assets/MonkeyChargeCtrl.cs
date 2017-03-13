using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyChargeCtrl : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {
        yield return new WaitForSeconds(1.8f);
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f,1.0f,1.0f,0.0f);
        GameObject par = GameObject.Instantiate(C_GAMEMANAGER.GetInstance().GetObjectMgr().GetObject("Monkey_Bullet_Particle"));
        par.transform.position = this.gameObject.transform.position;

        CircleCollider2D col = this.gameObject.AddComponent<CircleCollider2D>();

        col.radius = 15.0f;
        col.isTrigger = true;
    }
	
	// Update is called once per frame
	void Update () {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Destroy(collision.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
