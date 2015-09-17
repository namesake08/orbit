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

                // Корректное отображение астероидов игрока (без наложения друг на друга)
                if (colliderPlayer.Asteroids.Count > 1)
                {
                    float stepOrbitingAngle = 2 * Mathf.PI / colliderPlayer.Asteroids.Count;
                    for (int i = 0; i < colliderPlayer.Asteroids.Count; i++)
                    {
                        colliderPlayer.Asteroids[i].OrbitingAngle = i * stepOrbitingAngle;
                        System.Random rnd = new System.Random();
                       // colliderPlayer.Asteroids[i].SelfOrbit.Radius = rnd.Next(1, 10);
                    }

                }
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