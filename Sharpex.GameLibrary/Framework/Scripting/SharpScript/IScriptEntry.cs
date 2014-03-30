﻿
namespace SharpexGL.Framework.Scripting.SharpScript
{
    public interface IScriptEntry
    {
        /// <summary>
        /// The Main method of the script.
        /// </summary>
        /// <param name="objects">The Objects.</param>
        void Main(params object[] objects);
    }
}
