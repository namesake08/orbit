using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.GameLogic;

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

        private Clocks _clocks;
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
            GameObject clocks = Instantiate(GameValues.Clocks);
            _clocks = clocks.GetComponent<Clocks>();
            _clocks.transform.parent = transform;
            _clocks.transform.localPosition = new Vector3(0.0f, 0.0f, -1.0f);
            Vector3 clocksScale = transform.localScale * 0.7f;
            if (clocksScale.magnitude < new Vector3(0.6f, 0.6f, 0.6f).magnitude)
                clocksScale = new Vector3(0.6f, 0.6f, 0.6f);
            _clocks.transform.localScale = clocksScale;

            _clocks.OnTick += Blink;
        }

        protected virtual void Update()
        {
            _clocks.MaxSeconds = BlinkingInterval;
        }

        private void Blink()
        {
            PlanetController.Invisible = !PlanetController.Invisible;

            //List<Planet> planets = Orbit.AttachedPlanets;
            RecursionBlink(Orbit.AttachedPlanets);
            //while (planets.Count > 0)
            //{
            //    foreach (var item in planets)
            //    {
            //        item.Controller.Invisible = !item.Controller.Invisible;
            //    }
            //    planets = 
            //}

            //if (current->child)
            //{
            //    if (current->next) push(current->next);
            //    current = current->child;
            //    continue;
            //}
            //if (current->next)
            //{
            //    current = current->next;
            //    continue;
            //}
            //if (stack_not_empty)
            //{
            //    current = pop();
            //    continue;
            //}
            //else break;

        }

        private void RecursionBlink(List<Planet> planets)
        {
            if (planets.Count == 0)
                return;
            foreach (var item in planets)
            {
                item.Controller.Invisible = !item.Controller.Invisible;
                if (item.SelfOrbit != null)
                    RecursionBlink(item.SelfOrbit.AttachedPlanets);
            }
        }
    }
}
