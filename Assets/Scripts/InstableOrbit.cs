using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class InstableOrbit : Orbit
    {
        #region FIELDS

        [SerializeField]
        [Range(0.01f, 5)]
        private float _instabilityOffset;
        [SerializeField]
        [Range(0.1f, 5)]
        private float _instabilitySpeed = 1.0f;
        [SerializeField]
        private bool _instabilityActive;

        private bool _ascending;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Стандартный (первоначальный) радиус нестабильной орбиты
        /// </summary>
        public float StandardRadius 
        {
            get;
            private set;
        }

        /// <summary>
        /// Сдвиг нестабильности (т.е. максимальный радиус равен StandardRadius + InstabilityOffset)
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


        protected override void Start()
        {
            base.Start();

            StandardRadius = Radius;

            _ascending = true;

            //StartCoroutine(InstableCourutine(0.02f));
        }

        public void FixedUpdate()
        {
            Instable();
        }

        //IEnumerator InstableCourutine(float interval)
        //{
        //    while (true)
        //    {
        //        if (InstabilityActive)
        //            Instable();

        //        yield return new WaitForSeconds(interval);
        //    }
        //}

        private void Instable()
        {
            Vector3 newScale = transform.localScale;

            if (Radius > (StandardRadius + InstabilityOffset) &&
                _ascending)
                _ascending = false;

            if (Radius < StandardRadius && !_ascending)
                _ascending = true;

            if (_ascending)
                newScale *= 1 + 0.01f * InstabilitySpeed;
            else
                newScale /= 1 + 0.01f * InstabilitySpeed;

            transform.localScale = newScale;
        }

    }


}
