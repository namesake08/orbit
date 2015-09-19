using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Assets.Scripts.GameLogic
{
    public class Asteroid : PlanetController
    {
        // Use this for initialization
        protected override void Start()
        {
            base.Start();

            Planet.OnTriggerEnterAction += OnPlanetTriggerEnter;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }

        void OnPlanetTriggerEnter(Collider collider)
        {
            Planet planet = collider.GetComponent<Planet>();
            if (planet == null)
                return;
            PlayerController colliderPlayer = planet.Controller as PlayerController;
            if (colliderPlayer == null)
                return;

            if (colliderPlayer != null && AttachOrbit != colliderPlayer.Orbit)
            {
                colliderPlayer.Asteroids.Add(this);
                AttachOrbit = colliderPlayer.Orbit;

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
    }

}