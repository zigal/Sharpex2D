﻿using System;
using System.Collections.Generic;
using Sharpex2D.Framework.Game;
using Sharpex2D.Framework.Rendering.Devices;

namespace Sharpex2D.Framework.Entities
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class EntityEnvironment
    {
        private readonly Dictionary<int, Entity> _entities;

        /// <summary>
        ///     Initializes a new EntityManager class.
        /// </summary>
        public EntityEnvironment()
        {
            _entities = new Dictionary<int, Entity>();
        }

        /// <summary>
        ///     Adds a new Entity to the Container.
        /// </summary>
        /// <param name="entity">The Entity.</param>
        public void Add(Entity entity)
        {
            if (!_entities.ContainsKey(entity.Id))
            {
                _entities.Add(entity.Id, entity);
            }
            else
            {
                throw new ArgumentException("The entity id is already in use.");
            }
        }

        /// <summary>
        ///     Removes a Entity from the Container.
        /// </summary>
        /// <param name="entity">The Entity.</param>
        public void Remove(Entity entity)
        {
            if (_entities.ContainsValue(entity))
            {
                _entities.Remove(entity.Id);
            }
        }

        /// <summary>
        ///     Gets all Entities.
        /// </summary>
        /// <returns>Entity Array</returns>
        public Entity[] GetEntities()
        {
            var entities = new Entity[_entities.Values.Count];
            _entities.Values.CopyTo(entities, 0);
            return entities;
        }

        /// <summary>
        ///     Gets a special Entity.
        /// </summary>
        /// <typeparam name="T">The Type.</typeparam>
        /// <returns>Entity</returns>
        public T Get<T>() where T : Entity
        {
            for (int i = 0; i <= _entities.Count - 1; i++)
            {
                if (_entities[i].GetType() == typeof (T))
                {
                    return (T) _entities[i];
                }
            }

            throw new InvalidOperationException("Entity not found (" + typeof (T).FullName + ").");
        }

        /// <summary>
        ///     Gets a special Entity.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>Entity.</returns>
        public Entity GetEntityById(int id)
        {
            for (int i = 0; i <= _entities.Count - 1; i++)
            {
                if (_entities[i].Id == id)
                {
                    return _entities[i];
                }
            }

            throw new InvalidOperationException("Entity not found (" + id + ").");
        }

        /// <summary>
        ///     Clears all Entities.
        /// </summary>
        public void Clear()
        {
            _entities.Clear();
        }

        /// <summary>
        ///     Updates the object.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        public void Update(GameTime gameTime)
        {
            foreach (Entity entity in GetEntities())
            {
                if (entity.IsDestroyed)
                {
                    if (_entities.ContainsValue(entity))
                    {
                        _entities.Remove(entity.Id);
                    }
                }
                else
                {
                    entity.Update(gameTime);
                }
            }
        }

        /// <summary>
        ///     Processes a Render.
        /// </summary>
        /// <param name="renderer">The Renderer.</param>
        /// <param name="gameTime">The GameTime.</param>
        public void Render(RenderDevice renderer, GameTime gameTime)
        {
            foreach (Entity entity in GetEntities())
            {
                if (!entity.IsDestroyed)
                {
                    entity.Render(renderer, gameTime);
                }
            }
        }
    }
}