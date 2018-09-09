using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEffect : MonoBehaviour
{
    [SerializeField] private float projectileSpeed = 0.1f;
    [SerializeField]
    private float projectileRange = 10f;
    private float face;
    private Vector3 startPos;
    private Player player;

    public void Start()
    {
        player = FindObjectOfType<Player>();
        startPos = this.transform.localPosition;

        FacePlayer();
    }

    public void Update()
    { 
        this.transform.position+= new Vector3(face,0f,0f);

        if(Vector3.Distance(startPos,transform.localPosition)>projectileRange)
        { Destroy(this.gameObject);}
    }

    private void FacePlayer()
    {
        Vector3 distance = player.transform.localPosition - transform.localPosition;
        if (distance.x > 0)
        {
            face = Mathf.Abs(projectileSpeed);
            face = projectileSpeed;
        }
        else if (distance.x < 0)
        {
            face = Mathf.Abs(projectileSpeed);
            face = -projectileSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(this.gameObject);
    }
}
