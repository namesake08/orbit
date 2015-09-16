using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Assets.Scripts
{
    public class Asteroid : Planet
    {

        void OnTriggerEnter(Collider collider)
        {
            Player colliderPlayer = collider.GetComponent<Player>();
            if (colliderPlayer != null && AttachOrbit != colliderPlayer.SelfOrbit)
            {
                colliderPlayer.Asteroids.Add(this);
                AttachOrbit = colliderPlayer.SelfOrbit;
            }
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}