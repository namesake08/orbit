using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class Orbit : CachedBehaviour
    {

        #region PROPERTIES

        public float Radius
        {
            get;
            private set;
        }

        #endregion

        protected override void Awake()
        {
            base.Awake();

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

        protected virtual void FixedUpdate()
        {
            
        }


    }
}