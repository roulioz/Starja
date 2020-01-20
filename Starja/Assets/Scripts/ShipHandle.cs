using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class ShipHandle : MonoBehaviour
{
    Rigidbody2D rb;
    float h, v;
    float turnspeed = 14;
    float speed = 10;
    GameObject fond, etoiles1, etoiles2, reacteur, maincamera, mapcamera, pointeur, elements, infobulle;
    float afterburner = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fond = GameObject.Find("fond");
        etoiles1 = GameObject.Find("etoiles1");
        etoiles2 = GameObject.Find("etoiles2");
        reacteur = GameObject.Find("reacteur");
        pointeur = GameObject.Find("pointeur");
        maincamera = GameObject.Find("MainCamera");
        mapcamera = GameObject.Find("MapCamera");
        elements = GameObject.Find("elements");
        infobulle = GameObject.Find("Infobulle");
        mapcamera.SetActive(false);
        pointeur.SetActive(false);
        infobulle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Lorsqu'on appuie sur le bouton de la carte
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            if (mapcamera.activeInHierarchy)
            {
                elements.SetActive(true);
                maincamera.SetActive(true);
                pointeur.SetActive(false);
                mapcamera.SetActive(false);
            }
            else
            {
                mapcamera.SetActive(true);
                pointeur.transform.position = gameObject.transform.position;
                maincamera.SetActive(false);
                elements.SetActive(false);
                pointeur.SetActive(true);
            }
        }
        // Pour utiliser le super réacteur
        if (CrossPlatformInputManager.GetButton("Fire3"))
        {
            afterburner = 10f;
        }
        else
        {
            afterburner = 0f;
        }
        // Si on est sur la carte on ne peut plus bouger
        if (!mapcamera.activeInHierarchy)
        {
            h = CrossPlatformInputManager.GetAxis("Horizontal");
            v = CrossPlatformInputManager.GetAxis("Vertical");
        }
        else
        {
            h = 0;
            v = 0;
        }
    }

    private void FixedUpdate()
    {
        // Mouvement du vaisseau
        rb.velocity = gameObject.transform.right * (speed * v + afterburner);
        rb.angularVelocity = -h * (turnspeed * (16 - afterburner));
        // Déplacement des fonds d'écran (parallaxe)
        fond.GetComponent<Renderer>().material.mainTextureOffset += rb.velocity / 3000;
        etoiles1.GetComponent<Renderer>().material.mainTextureOffset += rb.velocity / 1500;
        etoiles2.GetComponent<Renderer>().material.mainTextureOffset += rb.velocity / 500 + new Vector2(0.003f, 0.001f) * Time.fixedDeltaTime;
        // Dimensionnement des flames des réacteurs en fonction de la vitesse
        reacteur.transform.localScale = new Vector2(reacteur.transform.localScale.x, 0.3f + v * (speed / 1.5f + afterburner / 1.5f) / 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Active les infos sur les planètes
        if (collision.gameObject.name.Contains("Planete"))
        {
            infobulle.SetActive(true);
            infobulle.transform.Find("Canvas").transform.Find("Panel").transform.Find("Description").GetComponent<Text>().text = collision.gameObject.GetComponent<PlaneteDescription>().description;
            infobulle.transform.position = collision.gameObject.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Désactive les infos sur les planètes
        infobulle.SetActive(false);
    }

    private void OnParticleCollision(GameObject other)
    {
        // Collision avec asteroids

    }
}
