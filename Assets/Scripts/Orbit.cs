using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class Orbit : MonoBehaviour
    {

        #region PROPERTIES

        public float Radius
        {
            get;
            private set;
        }

        public MeshRenderer MeshRenderer
        {
            get;
            set;
        }
        #endregion

        protected virtual void Awake()
        {
            MeshRenderer = GetComponent<MeshRenderer>();
            Radius = MeshRenderer.bounds.size.x / 2;
        }

        // Use this for initialization
        protected virtual void Start()
        {

        }

        // Update is called once per frame
        protected virtual void Update()
        {
            Radius = MeshRenderer.bounds.size.x / 2;
        }


    }
}