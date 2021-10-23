using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    ParticleSystem effect;
    float time;

    void Start()
    {
        effect = GetComponent<ParticleSystem>();
    }


    void Update()
    {
        if(time > effect.main.duration){
            Destroy(this.gameObject);
        }

        time += Time.deltaTime;
    }

}
