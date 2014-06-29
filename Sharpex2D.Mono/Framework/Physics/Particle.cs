﻿using Sharpex2D.Framework.Entities;
using Sharpex2D.Framework.Math;
using Sharpex2D.Framework.Physics.Collision;
using Sharpex2D.Framework.Physics.Shapes;
using Rectangle = Sharpex2D.Framework.Physics.Shapes.Rectangle;

namespace Sharpex2D.Framework.Physics
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class Particle : Entity
    {
        #region IShape Implementation

        /// <summary>
        ///     Gets or sets the type of the shape.
        /// </summary>
        public IShape Shape { set; get; }

        #endregion

        private readonly IPhysicProvider _physicProvider;
        private float _damping = 1f;
        private float _elasticity = 1f;
        private float _inverseMass;
        private Vector2 _velocity = new Vector2(0, 0);

        /// <summary>
        ///     Initializes a new Particle.
        /// </summary>
        public Particle()
        {
            Position = new Vector2(0, 0);
            Shape = new Rectangle();
            Gravity = true;
        }

        /// <summary>
        ///     Initializes a new Particle.
        /// </summary>
        /// <param name="physicProvider">The PhysicProvider.</param>
        public Particle(IPhysicProvider physicProvider)
        {
            _physicProvider = physicProvider;
            if (_physicProvider != null)
            {
                physicProvider.Subscribe(this);
            }
            Position = new Vector2(0, 0);
            Shape = new Rectangle();
            Gravity = true;
        }

        /// <summary>
        ///     Sets or gets the mass of the object.
        /// </summary>
        public float Mass
        {
            set { _inverseMass = 1.0f/value; }
            get { return 1.0f/_inverseMass; }
        }

        /// <summary>
        ///     Sets or gets the elasticity of the object.
        /// </summary>
        public float Elasticity
        {
            set { _elasticity = value; }
            get { return _elasticity; }
        }

        /// <summary>
        ///     Sets or gets the velocity of the object.
        /// </summary>
        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        /// <summary>
        ///     Sets or gets the damping value of the object.
        /// </summary>
        public float Damping
        {
            get { return _damping; }
            set { _damping = value; }
        }

        /// <summary>
        ///     Sets or gets whether gravity should be used.
        /// </summary>
        public bool Gravity { set; get; }

        /// <summary>
        ///     Gets the linked PhysicProvider.
        /// </summary>
        public IPhysicProvider PhysicProvider
        {
            get { return _physicProvider; }
        }

        /// <summary>
        ///     Called after a non-elastic impact.
        /// </summary>
        /// <param name="penetration">The PenetrationParams.</param>
        public virtual void OnPenetration(PenetrationParams penetration)
        {
        }

        /// <summary>
        ///     Called after an elastic impact.
        /// </summary>
        /// <param name="recoil">The RecoilParams.</param>
        public virtual void OnRecoil(RecoilParams recoil)
        {
        }
    }
}