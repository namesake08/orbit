using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PlanetAttributes
{
    public class InstablePlanet : PlanetAttribute
    {
        #region FIELDS

        [SerializeField]
        [Range(0.01f, 5)]
        private float _instabilityOffset = 1.0f;

        [SerializeField]
        [Range(0.1f, 5)]
        private float _instabilitySpeed = 1.0f;

        [SerializeField]
        private bool _instabilityActive = true;

        private bool _ascending;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Стандартный (первоначальный) радиус нестабильной орбиты
        /// </summary>
        public float StandardOrbitRadius 
        {
            get;
            private set;
        }

        /// <summary>
        /// Сдвиг нестабильности (т.е. максимальный радиус орбиты равен StandardRadius + InstabilityOffset)
        /// </summary>
        public float InstabilityOffset
        {
            get {return _instabilityOffset;}
            set {_instabilityOffset = value;}
        }

        /// <summary>
        /// Скорость нестабильности
        /// </summary>
        public float InstabilitySpeed
        {
            get { return _instabilitySpeed; }
            set { _instabilitySpeed = value; }
        }

        /// <summary>
        /// Активна ли нестабильность
        /// </summary>
        public bool InstabilityActive
        {
            get { return _instabilityActive; }
            set { _instabilityActive = value; }
        }

        #endregion

        protected override void Awake()
        {
            base.Awake();

        }

        protected virtual void Start()
        {
            StandardOrbitRadius = Orbit.Radius;
            _ascending = true;
        }

        protected virtual void FixedUpdate()
        {
            Instable();
        }

        private void Instable()
        {
            Vector3 newScale = Orbit.transform.localScale;

            if (Orbit.Radius > (StandardOrbitRadius + InstabilityOffset) &&
                _ascending)
                _ascending = false;

            if (Orbit.Radius < StandardOrbitRadius && !_ascending)
                _ascending = true;

            if (_ascending)
                newScale *= 1 + 0.01f * InstabilitySpeed;
            else
                newScale /= 1 + 0.01f * InstabilitySpeed;

            Orbit.transform.localScale = newScale;
        }

    }


}
