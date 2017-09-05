using UnityEngine;
using System.Collections;

public class FireBallParticleScript : MonoBehaviour {

    string tagIgnore;
    ParticleSystem fireBallParticl;
    public float speed = 0.5f;
    Vector3 movedirection;
    public float lifeTime = 5.0f;
    public Transform target;
    Vector3 moveDir;
	// Use this for initialization
	void Start () {
        fireBallParticl = gameObject.GetComponent<ParticleSystem>();
        fireBallParticl.Play();
	}

	// Update is called once per frame
	void Update () {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
            return;
        }
        if (target == null) { return; }
        moveDir = (target.position - transform.position).normalized;
        transform.position += moveDir * speed * Time.deltaTime;        
    }

    public void SetUp(int layer)
    {
        gameObject.layer = layer;
    }

    public void SetMoveDirection(Vector3 movedir,string _tagIgnore) {
        movedirection = movedir;
        tagIgnore = _tagIgnore;
    }

    void OnTriggerStay(Collider other)
    {
        IDamageable idamageAble = other.GetComponent<IDamageable>();
        if (idamageAble != null)
        {
            idamageAble.GetDamage(20);
            Destroy(gameObject);
        }
        
    }
}
