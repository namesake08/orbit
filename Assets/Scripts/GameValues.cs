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

        [SerializeField]
        private Material _invisibleMaterial;

        [SerializeField]
        private GameObject _clocks;
        #endregion

        #region PROPERTIES
        public static Material SplineLineMaterial;
        public static GameObject SplineLineTrail;
        public static Material InvisibleMaterial;
        public static GameObject Clocks;
        #endregion

        void Awake()
        {
            SplineLineMaterial = _splineLineMaterial;
            SplineLineTrail = _splineLineTrail;
            InvisibleMaterial = _invisibleMaterial;
            Clocks = _clocks;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}