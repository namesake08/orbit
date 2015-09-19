using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.GameLogic
{
    public class SpaceBody : CachedBehaviour
    {
        public float Radius
        {
            get
            {
                // 0.5 - стандартный размер сферы (примитива) в редакторе
                return 0.5f * transform.lossyScale.x;
            }
        }

        public Material StandardMaterial
        {
            get;
            private set;
        }

        protected Transform _parentTransform;

        protected override void Awake()
        {
            base.Awake();

            _parentTransform = transform.parent;
            StandardMaterial = Renderer.material;
        }
    }
}
