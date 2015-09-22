using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.GameLogic
{
    public class PlayerController : PlanetController
    {
        #region PROPERTIES
        /// <summary>
        /// Список орбит, на которые может перескочить игрок
        /// </summary>
        public List<Orbit> PossibleOrbits;

        /// <summary>
        /// Список астероидов, привязанных к игроку
        /// </summary>
        public List<Asteroid> Asteroids;
        #endregion
        

        protected override void Awake()
        {
            base.Awake();

        }

        // Use this for initialization
        protected override void Start()
        {
            base.Start();
            PossibleOrbits = new List<Orbit>();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                OrbitalJump();
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                SwitchDirection();
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.position.x > Screen.height / 2)
                    OrbitalJump();
                if (touch.position.x < Screen.height / 2)
                    SwitchDirection();
            }
        }

        /// <summary>
        /// Перескочить на другую орбиту
        /// </summary>
        protected void OrbitalJump()
        {
            if (PossibleOrbits.Count > 0)
            {
                AttachOrbit = PossibleOrbits[0];
                PossibleOrbits.Remove(AttachOrbit);
            }
        }

        protected override void OnPlanetTriggerEnter(Collider collider)
        {
            base.OnPlanetTriggerEnter(collider);

            Orbit colliderOrbit = collider.GetComponent<Orbit>();
            if (colliderOrbit != null && colliderOrbit != AttachOrbit)
            {
                PossibleOrbits.Add(colliderOrbit);
            }

            Planet colliderPlanet = collider.GetComponent<Planet>();
            
            // Если это планета, но не астероид, то взрываемся
            if (colliderPlanet != null && 
                !(colliderPlanet.Controller is Asteroid))
            {
                Destroy();
                colliderPlanet.Controller.Destroy();
            }
        }
        protected override void OnPlanetTriggerExit(Collider collider)
        {
            base.OnPlanetTriggerExit(collider);

            Orbit colliderOrbit = collider.GetComponent<Orbit>();
            if (colliderOrbit != null && colliderOrbit != AttachOrbit)
            {
                PossibleOrbits.Remove(colliderOrbit);
            }
        }

        

    }

}