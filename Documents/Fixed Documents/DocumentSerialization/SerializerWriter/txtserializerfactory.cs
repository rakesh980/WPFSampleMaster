using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Documents.Serialization;

namespace DocumentSerialization
{
    class TxtSerializerFactory : ISerializerFactory
    {
        /// <summary>
        /// Create a SerializerWriter on the passed in stream
        /// </summary>
        public SerializerWriter CreateSerializerWriter(Stream stream) => new TxtSerializerWriter(stream);
        /// <summary>
        /// Return the DisplayName of the serializer.
        /// </summary>
        public string DisplayName => "Text Document Writer";
        /// <summary>
        /// Return the ManufacturerName of the serializer.
        /// </summary>
        public string ManufacturerName => "Microsoft";
        /// <summary>
        /// Return the ManufacturerWebsite of the serializer.
        /// </summary>
        public Uri ManufacturerWebsite => new Uri("http://www.microsoft.com");
        /// <summary>
        /// Return the DefaultFileExtension of the serializer.
        /// </summary>
        public string DefaultFileExtension => ".txt";
    }
}
