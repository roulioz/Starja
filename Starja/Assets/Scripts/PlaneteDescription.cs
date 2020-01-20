using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneteDescription : MonoBehaviour
{
    public string Nom = "Armadia";    
    public int Temperature = 15; // °C
    long Rayon = 6378; //km    
    long Superficie = 510067420; // km2
    long Volume = 1083210; // Millions de km3
    long Masse = 5973600000000; // Milliards de tonnes
    long VitesseRotation = 1674; // km/h
    public string description;

    // Start is called before the first frame update
    void Start()
    {
        Rayon = Convert.ToInt64(gameObject.transform.localScale.x * 2000);
        Superficie = Convert.ToInt64(Rayon * 79972.941361f);
        Volume = Convert.ToInt64(Rayon * 169.8353716f);
        Masse = Convert.ToInt64(Rayon * 936594543.7441204f);
        VitesseRotation = Convert.ToInt64(Rayon / 3.81 - (Rayon - 6378) / 5) + UnityEngine.Random.Range(-Convert.ToInt32(Rayon/10), Convert.ToInt32(Rayon/10));
        description = "Nom : " + Nom + "\n\r";
        description += "Temperature : " + Temperature + " °C\n\r";
        description += "Rayon : " + Rayon + " km\n\r";
        description += "Superficie : " + Superficie + " km2\n\r";
        description += "Volume : " + Volume + " millions de km3\n\r";
        description += "Masse : " + Masse + " milliars de tonnes\n\r";
        description += "Vitesse de rotation : " + VitesseRotation + " km/h\n\r";        
    }

    // Update is called once per frame
    void Update()
    {        
        gameObject.transform.Rotate(0, 0, VitesseRotation * Time.deltaTime / 5000);
    }
}
