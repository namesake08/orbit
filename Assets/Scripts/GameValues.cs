using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class GameValues : MonoBehaviour
    {
        #region FIELDS
        [SerializeField]
        private Material _splineLineMaterial;
        #endregion

        #region PROPERTIES
        public static Material SplineLineMaterial;
        #endregion

        void Awake()
        {
            SplineLineMaterial = _splineLineMaterial;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}