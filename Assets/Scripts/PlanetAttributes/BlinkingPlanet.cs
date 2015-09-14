using UnityEngine;
using System.Collections;

namespace Assets.Scripts.PlanetAttributes
{
    public class BlinkingPlanet : PlanetAttribute
    {
        #region FIELDS
        [SerializeField]
        [Range(0.01f, 10)]
        private float _blinkingInterval = 1.0f;

        [SerializeField]
        private bool _blinkingActive = true;
        #endregion

        #region PROPERTIES
        /// <summary>
        /// Временной интервал мигания в секундах
        /// </summary>
        public float BlinkingInterval
        {
            get { return _blinkingInterval; }
            set { _blinkingInterval = value; }
        }

        /// <summary>
        /// Активно ли мигание
        /// </summary>
        public bool BlinkingActive
        {
            get { return _blinkingActive; }
            set { _blinkingActive = value; }
        }
        #endregion

        protected override void Awake()
        {
            base.Awake();

        }

        protected virtual void Start()
        {
            StartCoroutine(InstableCourutine());   
        }

        IEnumerator InstableCourutine()
        {
            while (true)
            {
                if (BlinkingActive)
                    Blink();

                yield return new WaitForSeconds(BlinkingInterval);
            }
        }

        private void Blink()
        {
            Planet.Invisible = !Planet.Invisible;
        }
    }
}
