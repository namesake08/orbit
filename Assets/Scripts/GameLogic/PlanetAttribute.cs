using UnityEngine;

namespace Assets.Scripts.GameLogic
{
    [RequireComponent(typeof(PlanetController))]
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

        public PlanetController PlanetController
        {
            get;
            private set;
        }
        #endregion

        protected override void Awake()
        {
            base.Awake();

            PlanetController = GetComponent<PlanetController>();

            Planet = GetComponentInChildren<Planet>(); //PlanetController.Planet;
            Orbit = GetComponentInChildren<Orbit>();
        }

    }
}
