using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.GameLogic
{
    public class Orbit : SpaceBody
    {
        /// <summary>
        /// Планета, к которой привязана орбита
        /// </summary>
        public Planet SelfPlanet
        {
            get;
            private set;
        }

        /// <summary>
        /// Привязанные к орбите планеты
        /// </summary>
        public List<Planet> AttachedPlanets
        {
            get;
            private set;
        }

        protected override void Awake()
        {
            base.Awake();

            AttachedPlanets = new List<Planet>();

            if (_parentTransform != null)
                SelfPlanet = _parentTransform.GetComponentInChildren<Planet>();
        }

        // Use this for initialization
        protected virtual void Start()
        {

        }

        // Update is called once per frame
        protected virtual void Update()
        {

        }

        protected virtual void FixedUpdate()
        {
            
        }

    }
}