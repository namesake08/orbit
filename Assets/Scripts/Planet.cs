using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class Planet : MonoBehaviour
    {

        #region FIELDS

        [SerializeField]
        private Orbit _attachOrbit;

        [SerializeField]
        private float _orbitingSpeed;

        [SerializeField]
        [Range(-1, 1)]
        private int _orbitingDirection;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Собственная орбита планеты.
        /// </summary>
        public Orbit SelfOrbit
        {
            get;
            set;
        }

        /// <summary>
        /// Орбита, к которой планета привязана.
        /// </summary>
        public Orbit AttachOrbit
        {
            get { return _attachOrbit; }
            set { _attachOrbit = value; }
        }

        /// <summary>
        /// Направление движения по орбите 
        /// (1: по часовой стрелке; 
        /// -1: против часовой стрелки)
        /// </summary>
        public int OrbitingDirection
        {
            get { return _orbitingDirection; }
            set { _orbitingDirection = Mathf.Clamp(value, -1, 1); }
        }

        /// <summary>
        /// Скорость движения по орбите (количество полных совершаемых кругов в секунду)
        /// </summary>
        public float OrbitingSpeed
        {
            get { return _orbitingSpeed; }
            set { _orbitingSpeed = value; }
        }

        /// <summary>
        /// Угол положения на орбите 
        /// </summary>
        public float OrbitingAngle
        {
            get;
            set;
        }

        /// <summary>
        /// Время нахождения на орбите
        /// </summary>
        public float OrbitingTime
        {
            get;
            private set;
        }

        #endregion


        public void ChangeAttachOrbit(Orbit newOrbit)
        {
            Vector2 thisPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 orbitPos = new Vector2(newOrbit.transform.position.x, newOrbit.transform.position.y);
            OrbitingAngle = MathHelper.AngleBetween(orbitPos, thisPos);
            AttachOrbit = newOrbit;
            OrbitingTime = 0;
        }

        protected virtual void Awake()
        {

        }

        // Use this for initialization
        protected virtual void Start()
        {
            SelfOrbit = GetComponentInChildren<Orbit>();
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            // if (AttachOrbit != null)
            //     OrbitingSpeed = 1 / AttachOrbit.Radius;
        }

        protected virtual void FixedUpdate()
        {
            OrbitalMotion();
        }

        private void OrbitalMotion()
        {
            if (AttachOrbit == null)
                return;

            OrbitingTime += Time.fixedDeltaTime;
            OrbitingSpeed = 0.5f / AttachOrbit.Radius;

            Vector3 orbitPos = AttachOrbit.transform.position;
            Vector3 targetPos = new Vector3(orbitPos.x, orbitPos.y, transform.position.z);
            float speed = (2 * Mathf.PI) * OrbitingSpeed; //количество кругов, совершаемых в секунду

            OrbitingAngle += OrbitingDirection * speed * Time.deltaTime;
            targetPos.x += Mathf.Cos(OrbitingAngle) * AttachOrbit.Radius;
            targetPos.y += Mathf.Sin(OrbitingAngle) * AttachOrbit.Radius;
            transform.position = targetPos;
        }
    }
}