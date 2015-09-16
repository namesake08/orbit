using UnityEngine;

namespace Assets.Scripts
{
    public class CachedBehaviour : MonoBehaviour
    {
        public MeshRenderer MeshRenderer
        {
            get;
            private set;
        }

        public Renderer Renderer
        {
            get;
            private set;
        }

        public Collider Collider
        {
            get;
            private set;
        }

        public Material StandardMaterial
        {
            get;
            private set;
        }


        protected virtual void Awake()
        {
            MeshRenderer = GetComponent<MeshRenderer>();
            Renderer = GetComponent<Renderer>();
            Collider = GetComponent<Collider>();
            StandardMaterial = Renderer.material;
        }

    }
}
