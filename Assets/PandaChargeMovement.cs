using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaChargeMovement : MonoBehaviour {

    float timr;
    void Start()
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        timr = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (timr >= 0.416f)
        {
            this.gameObject.SetActive(false);
        }

        timr += Time.deltaTime;
    }
}
