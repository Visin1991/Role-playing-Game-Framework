using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{

    Rigidbody rg;
    public float forceMin = 80f;
    public float forceMax = 120f;

    float lifetime = 4;
    float fadetime = 2;

    // Use this for initialization
    void Start()
    {
        rg = transform.GetOrAddComponent<Rigidbody>();
        float force = Random.Range(forceMin, forceMax);
        rg.AddForce(transform.right * force);
        rg.AddTorque(Random.insideUnitSphere * force);
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(lifetime);
        float percent = 0;
        float fadeSpeed = 1 / fadetime;
        Material mat = GetComponent<Renderer>().material;
        Color initialCoulor = mat.color;

        while (percent < 1.0f)
        {
            percent += Time.deltaTime * fadeSpeed;
            mat.color = Color.Lerp(initialCoulor, Color.clear, percent);
            yield return null;
        }

        Destroy(gameObject);
    }
}
