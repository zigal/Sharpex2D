﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SharpexGL.Framework.Rendering;

namespace SharpexGL.Framework.Content.Serialization
{
    public class ColorSerializer : ContentSerializer<Color>
    {
        /// <summary>
        /// Reads a value from the given Reader.
        /// </summary>
        /// <param name="reader">The BinaryReader.</param>
        /// <returns></returns>
        public override Color Read(BinaryReader reader)
        {
            return new Color(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
        }
        /// <summary>
        /// Writes a specified value.
        /// </summary>
        /// <param name="writer">The BinaryWriter.</param>
        /// <param name="value">The Value.</param>
        public override void Write(BinaryWriter writer, Color value)
        {
            writer.Write(value.R);
            writer.Write(value.G);
            writer.Write(value.B);
            writer.Write(value.A);
        }
    }
}
