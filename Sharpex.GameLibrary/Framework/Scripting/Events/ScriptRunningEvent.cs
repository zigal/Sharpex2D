﻿using System;
using SharpexGL.Framework.Events;

namespace SharpexGL.Framework.Scripting.Events
{
    public class ScriptRunningEvent : IEvent
    {
        /// <summary>
        /// Initializes a new ScriptRunningEvent class.
        /// </summary>
        /// <param name="guid">The Guid.</param>
        public ScriptRunningEvent(Guid guid)
        {
            ScriptGuid = guid;
        }

        /// <summary>
        /// Gets the name of the running scrip.t
        /// </summary>
        public Guid ScriptGuid { get; private set; }
    }
}
