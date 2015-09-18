using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class Orbit : CachedBehaviour
    {

        #region PROPERTIES

        public float Radius
        {
            get
            {
                // 0.5 - стандартный размер сферы (примитива) в редакторе
                return 0.5f * transform.lossyScale.x; //Radius = MeshRenderer.bounds.size.x / 2;
            }
        }

        #endregion

        protected override void Awake()
        {
            base.Awake();

        }

        // Use this for initialization
        protected virtual void Start()
        {

        }

        // Update is called once per frame
        protected virtual void Update()
        {

        }

        protected virtual void FixedUpdate()
        {
            
        }

    }
}