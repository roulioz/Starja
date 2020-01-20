using UnityEngine;

public class asteroids_manager : MonoBehaviour
{
    public float distanceDestruction = 10;
    private ParticleSystem ps;        

    void Start()
    {
        ps = gameObject.GetComponent<ParticleSystem>();         
    }

    void Update()
    {
        ParticleSystem.Particle[] particules = new ParticleSystem.Particle[ps.particleCount];
        ps.GetParticles(particules);
        for (int i = 0; i < particules.Length; i++)
        {
            if (Vector2.Distance(particules[i].position, transform.position) > 15)
                particules[i].remainingLifetime = -1;
        }
        ps.SetParticles(particules, particules.Length);
    }
}