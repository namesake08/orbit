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

        private GameObject _clocks;
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

            _clocks = Instantiate(GameValues.Clocks);
            _clocks.transform.parent = transform;
            _clocks.transform.localPosition = new Vector3(0.0f, 0.0f, -1.0f);
            Vector3 clocksScale = transform.localScale * 0.7f;
            if (clocksScale.magnitude < new Vector3(0.6f, 0.6f, 0.6f).magnitude)
                clocksScale = new Vector3(0.6f, 0.6f, 0.6f);
            _clocks.transform.localScale = clocksScale;
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
