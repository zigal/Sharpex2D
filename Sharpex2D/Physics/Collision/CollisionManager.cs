// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the 'Software'), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using Sharpex2D.Math;
using Sharpex2D.Physics.Shapes;
using Circle = Sharpex2D.Physics.Shapes.Circle;
using Rectangle = Sharpex2D.Physics.Shapes.Rectangle;

namespace Sharpex2D.Physics.Collision
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    [Obsolete("The old physic system will be removed in the future. Please use alternatives.")]
    public class CollisionManager : ICollision, IComponent
    {
        #region IComponent Implementation

        /// <summary>
        /// Sets or gets the Guid of the Component.
        /// </summary>
        public Guid Guid
        {
            get { return new Guid("8B822A26-67AC-456E-BA37-0978C9F9697B"); }
        }

        #endregion

        #region ICollision Implementation

        /// <summary>
        /// Indicates whether the particles intersect with each other.
        /// </summary>
        /// <param name="particle1">The first Particle.</param>
        /// <param name="particle2">The second Particle.</param>
        public bool IsIntersecting(Particle particle1, Particle particle2)
        {
            return InternalIsIntersecting(particle1, particle2);
        }

        #endregion

        #region CollisionManager Internal

        /// <summary>
        /// Initializes a new CollisionManager class.
        /// </summary>
        public CollisionManager()
        {
            SGL.Components.Add(this);
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~CollisionManager()
        {
            SGL.Components.Remove(this);
        }

        /// <summary>
        /// Indicates whether the particles intersect with each other.
        /// </summary>
        /// <param name="particle1">The first Particle.</param>
        /// <param name="particle2">The second Particle.</param>
        private bool InternalIsIntersecting(Particle particle1, Particle particle2)
        {
            //Check out the particles base types, if one of them is not a known shape throw exception
            //Check particle 1
            if (!(particle1.Shape is Circle || particle1.Shape is Rectangle))
            {
                throw new UnknownShapeException("Unknown shape in " + particle1.GetType().Name);
            }
            //Check particle 2
            if (!(particle2.Shape is Circle || particle2.Shape is Rectangle))
            {
                throw new UnknownShapeException("Unknown shape in " + particle2.GetType().Name);
            }

            // Particle shapes are correct, continue

            //Check if both particles are rectangles
            if (particle1.Shape is Rectangle && particle2.Shape is Rectangle)
            {
                return RectangleIntersectsRectangle(particle1, particle2);
            }

            //Check if both particles are circles
            if (particle2.Shape is Circle && particle1.Shape is Circle)
            {
                return CircleIntersectsCircle(particle1, particle2);
            }

            //Check if the second particle is a circle
            if (particle1.Shape is Rectangle)
            {
                return RectangleIntersectsCircle(particle1, particle2);
            }

            //check if the second particle is a rectangle
            return RectangleIntersectsCircle(particle2, particle1);
        }

        /// <summary>
        /// Indicates whether the rectangle intersects with another rectangle.
        /// </summary>
        /// <param name="particle1">The first Particle.</param>
        /// <param name="particle2">The second Particle.</param>
        /// <returns>True on intersecting</returns>
        private bool RectangleIntersectsRectangle(Particle particle1, Particle particle2)
        {
            /*/RectA.X1 < RectB.X2 && RectA.X2 > RectB.X1 &&
               RectA.Y1 < RectB.Y2 && RectA.Y2 > RectB.Y1/*/

            var rect1 = (Rectangle) particle1.Shape;
            var rect2 = (Rectangle) particle2.Shape;

            return particle1.Position.X < (particle2.Position.X + rect2.Width) &&
                   (particle1.Position.X + rect1.Width) > particle2.Position.X &&
                   particle1.Position.Y < (particle2.Position.Y + rect2.Height) &&
                   (particle1.Position.Y + rect1.Height) > particle2.Position.Y;
        }

        /// <summary>
        /// Indicates whether the rectangle intersects with a circle.
        /// </summary>
        /// <param name="particle1">The first Particle.</param>
        /// <param name="particle2">The second Particle.</param>
        /// <returns>True on intersecting</returns>
        private bool RectangleIntersectsCircle(Particle particle1, Particle particle2)
        {
            var rect = (Rectangle) particle1.Shape;
            var circle = (Circle) particle2.Shape;

            var textureCircle = circle as TextureBasedCircle;
            Vector2 circleDistance;

            if (textureCircle == null)
            {
                circleDistance =
                    Vector2.Abs(particle2.Position -
                                new Vector2(particle1.Position.X + rect.Width*0.5f,
                                    particle1.Position.Y + rect.Height*0.5f));
            }
            else
            {
                var txCenter = new Vector2(particle2.Position.X + ((float) textureCircle.Texture.Width/2),
                    particle2.Position.Y + ((float) textureCircle.Texture.Height/2));

                circleDistance =
                    Vector2.Abs(txCenter -
                                new Vector2(particle1.Position.X + rect.Width*0.5f,
                                    particle1.Position.Y + rect.Height*0.5f));
            }
            Vector2 boxSize = new Vector2(rect.Width, rect.Height)/2f;

            if (circleDistance.X > boxSize.X + circle.Radius ||
                circleDistance.Y > boxSize.Y + circle.Radius)
                return false;

            if (circleDistance.X <= boxSize.X ||
                circleDistance.Y <= boxSize.Y)
                return true;

            return (circleDistance - boxSize).LengthSquared <= circle.Radius*circle.Radius;
        }

        /// <summary>
        /// Indicates whether the circle intersects with a circle.
        /// </summary>
        /// <param name="particle1">The first Particle.</param>
        /// <param name="particle2">The second Particle.</param>
        /// <returns>True on intersecting</returns>
        private bool CircleIntersectsCircle(Particle particle1, Particle particle2)
        {
            var circle1 = (Circle) particle1.Shape;
            var circle2 = (Circle) particle2.Shape;

            var txCircle1 = circle1 as TextureBasedCircle;
            var txCircle2 = circle2 as TextureBasedCircle;

            if (txCircle1 != null && txCircle2 == null)
            {
                var pos = new Vector2(particle1.Position.X + ((float) txCircle1.Texture.Width/2),
                    particle1.Position.Y + ((float) txCircle1.Texture.Height/2));
                return (pos - particle2.Position).Length <
                       (circle1.Radius + circle2.Radius);
            }

            if (txCircle2 != null && txCircle1 == null)
            {
                var pos = new Vector2(particle2.Position.X + ((float) txCircle2.Texture.Width/2),
                    particle2.Position.Y + ((float) txCircle2.Texture.Height/2));
                return (particle1.Position - pos).Length <
                       (circle1.Radius + circle2.Radius);
            }

            if (txCircle1 != null)
            {
                var pos1 = new Vector2(particle1.Position.X + ((float) txCircle1.Texture.Width/2),
                    particle1.Position.Y + ((float) txCircle1.Texture.Height/2));

                var pos2 = new Vector2(particle2.Position.X + ((float) txCircle2.Texture.Width/2),
                    particle2.Position.Y + ((float) txCircle2.Texture.Height/2));

                return (pos1 - pos2).Length <
                       (circle1.Radius + circle2.Radius);
            }

            return (particle1.Position - particle2.Position).Length <
                   (circle1.Radius + circle2.Radius);
        }

        #endregion
    }
}