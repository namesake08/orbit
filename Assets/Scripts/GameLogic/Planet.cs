using UnityEngine;
using System;
using System.Collections;

namespace Assets.Scripts.GameLogic
{
    public class Planet : SpaceBody
    {

        #region PROPERTIES

        /// <summary>
        /// Собственная орбита планеты.
        /// </summary>
        public Orbit SelfOrbit
        {
            get;
            private set;
        }

        public PlanetController Controller
        {
            get;
            private set;
        }

        #endregion

        #region EVENTS
        public event Action<Collider> OnTriggerEnterAction;
        public event Action<Collider> OnTriggerExitAction;
        #endregion

        protected override void Awake()
        {
            base.Awake();

            if (_parentTransform != null)
                Controller = _parentTransform.GetComponent<PlanetController>();
        }

        protected virtual void OnTriggerEnter(Collider collider)
        {
            if (OnTriggerEnterAction != null)
                OnTriggerEnterAction(collider);
        }

        protected virtual void OnTriggerExit(Collider collider)
        {
            if (OnTriggerExitAction != null)
                OnTriggerExitAction(collider);
        }

        // Use this for initialization
        protected virtual void Start()
        {
            if (_parentTransform != null)
                SelfOrbit = Controller.Orbit;
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
