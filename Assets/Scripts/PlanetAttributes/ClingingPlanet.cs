using UnityEngine;
using System.Collections;
using Assets.Scripts.GameLogic;

namespace Assets.Scripts.PlanetAttributes
{
    public class ClingingPlanet : PlanetAttribute
    {
        private Orbit _prevOrbit; //предыдущая орбита, на которой побывала цепляющаяся планета

        // Use this for initialization
        void Start()
        {
            Planet.OnTriggerEnterAction += OnPlanetTriggerEnter;
            Planet.OnTriggerExitAction += OnPlanetTriggerExit;
        }

        void OnPlanetTriggerEnter(Collider collider)
        {
            Orbit colliderOrbit = collider.GetComponent<Orbit>();
            if (colliderOrbit != null && colliderOrbit != _prevOrbit
                && colliderOrbit != PlanetController.AttachOrbit &&
                colliderOrbit != Orbit)
            {
                StartCoroutine(Cling(colliderOrbit));
            }

        }

        void OnPlanetTriggerExit(Collider collider)
        {
            Orbit colliderOrbit = collider.GetComponent<Orbit>();
            if (colliderOrbit != null && colliderOrbit == _prevOrbit)
            {
                _prevOrbit = null;
            }
        }

        IEnumerator Cling(Orbit orbit)
        {
            yield return new WaitForSeconds(0.2f);

            _prevOrbit = PlanetController.AttachOrbit;
            PlanetController.AttachOrbit = orbit;
            PlanetController.SwitchDirection();
        }

    }

}