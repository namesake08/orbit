using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Planet))]
    public class PlanetAttribute : CachedBehaviour
    {
        #region PROPERTIES
        public Planet Planet
        {
            get;
            private set;
        }

        public Orbit Orbit
        {
            get;
            private set;
        }
        #endregion

        protected override void Awake()
        {
            base.Awake();

            Planet = GetComponent<Planet>();
            Orbit = GetComponentInChildren<Orbit>();
        }

    }
}
