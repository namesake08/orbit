using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class GameValues : MonoBehaviour
    {
        #region FIELDS
        [SerializeField]
        private Material _splineLineMaterial;

        [SerializeField]
        private GameObject _splineLineTrail;
        #endregion

        #region PROPERTIES
        public static Material SplineLineMaterial;
        public static GameObject SplineLineTrail;
        #endregion

        void Awake()
        {
            SplineLineMaterial = _splineLineMaterial;
            SplineLineTrail = _splineLineTrail;

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}