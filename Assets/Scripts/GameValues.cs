using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class GameValues : MonoBehaviour
    {
        #region FIELDS
        [SerializeField]
        private Material _splineLineMaterial = null;

        [SerializeField]
        private GameObject _splineLineTrail = null;

        [SerializeField]
        private Material _invisibleMaterial = null;

        [SerializeField]
        private GameObject _clocks = null;

        [SerializeField]
        private GameObject _destroyEffect = null;
        #endregion

        #region PROPERTIES
        public static Material SplineLineMaterial;
        public static GameObject SplineLineTrail;
        public static Material InvisibleMaterial;
        public static GameObject Clocks;
        public static GameObject DestroyEffect;
        #endregion

        void Awake()
        {
            SplineLineMaterial = _splineLineMaterial;
            SplineLineTrail = _splineLineTrail;
            InvisibleMaterial = _invisibleMaterial;
            Clocks = _clocks;
            DestroyEffect = _destroyEffect;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}