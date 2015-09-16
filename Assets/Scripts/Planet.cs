using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class Planet : CachedBehaviour
    {

        #region FIELDS

        [SerializeField]
        private Orbit _attachOrbit;

        [SerializeField]
        private float _orbitingSpeed;

        [SerializeField]
        [Range(-1, 1)]
        private int _orbitingDirection = 1;

        private float _orbitingAngle;

        private bool _invisible;

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
            set 
            { 
                _attachOrbit = value;
                OrbitingTime = 0;
                OrbitingAngle = MathHelper.AngleBetween(value.transform.position, transform.position);
            }
        }

        /// <summary>
        /// Направление движения по орбите 
        /// (1: по часовой стрелке; 
        ///  0: отсутствие движения;
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
            get { return _orbitingAngle; }
            set
            {
                if (value < -2 * Mathf.PI || value > 2 * Mathf.PI)
                    _orbitingAngle = 0;
                else
                    _orbitingAngle = value;
            }
        }

        /// <summary>
        /// Время нахождения на орбите
        /// </summary>
        public float OrbitingTime
        {
            get;
            private set;
        }

        /// <summary>
        /// Включает/выключает невидимость планеты
        /// </summary>
        public bool Invisible
        {
            get { return _invisible; }
            set 
            { 
                _invisible = value; 
                SetVisibility(); 
            }
        }

        #endregion


        /// <summary>
        /// Сменить направление движения на противоположное.
        /// </summary>
        public void SwitchDirection()
        {
            OrbitingDirection = -OrbitingDirection;
        }

        protected override void Awake()
        {
            SelfOrbit = GetComponentInChildren<Orbit>();
            base.Awake();

        }

        // Use this for initialization
        protected virtual void Start()
        {
            
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

        private void SetVisibility()
        {
            //Material m_p = Renderer.material;
            //Material m_o = SelfOrbit.Renderer.material;
            //Color c_p = m_p.color;
            //Color c_o = m_o.color;
            //if (Invisible)
            //    c_p.a = c_o.a = 0.5f;
            //else
            //    c_p.a = c_o.a = 1f;
            //m_p.color = c_p;
            //m_o.color = c_o;
            //Renderer.material = m_p;
            //SelfOrbit.Renderer.material = m_o;

            if (Invisible)
            {
                Renderer.material = GameValues.InvisibleMaterial;
                SelfOrbit.Renderer.material = GameValues.InvisibleMaterial;
            }
            else
            {
                Renderer.material = StandardMaterial;
                SelfOrbit.Renderer.material = SelfOrbit.StandardMaterial;
            }

            Collider.enabled = !Invisible;
            SelfOrbit.Collider.enabled = !Invisible;
        }
    }
}