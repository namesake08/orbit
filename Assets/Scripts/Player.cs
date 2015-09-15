using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class Player : Planet
    {
        /// <summary>
        /// Список орбит, на которые может перескочить игрок
        /// </summary>
        public List<Orbit> PossibleOrbits;

        void OnTriggerEnter(Collider collider)
        {
            Orbit colliderOrbit = collider.GetComponent<Orbit>();
            if (colliderOrbit != null && colliderOrbit != AttachOrbit)
            {
                PossibleOrbits.Add(colliderOrbit);
            }
        }

        void OnTriggerExit(Collider collider)
        {
            Orbit colliderOrbit = collider.GetComponent<Orbit>();
            if (colliderOrbit != null && colliderOrbit != AttachOrbit)
            {
                PossibleOrbits.Remove(colliderOrbit);
            }
        }

        protected override void Awake()
        {

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
                if (PossibleOrbits.Count > 0)
                {
                    ChangeAttachOrbit(PossibleOrbits[0]);
                    PossibleOrbits.Remove(AttachOrbit);
                }
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                OrbitingDirection = -OrbitingDirection;
            }
        }
    }

}