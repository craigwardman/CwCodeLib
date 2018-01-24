namespace CwCodeLib.Serialization
{
    /// <summary>
    /// Wraps the functionality of the XmlSerializer class.
    /// </summary>
    /// <remarks></remarks>
    public static class XmlFormatter<T>
    {
        /// <summary>
        /// Serialize the object to an XML string.
        /// </summary>
        /// <param name="instance">The instance to serialize</param>
        /// <returns>The XML formatted string serialization</returns>
        public static string Serialize(T instance)
        {
            var xmlFmtr = new System.Xml.Serialization.XmlSerializer(typeof(T));
            var output = new System.Text.StringBuilder();

            using (var outputStrm = new System.IO.StringWriter(output))
            {
                xmlFmtr.Serialize(outputStrm, instance);
            }

            return output.ToString();
        }

        /// <summary>
        /// Deserialize the XML string back to an instance of the given type.
        /// </summary>
        /// <param name="xml"></param>
        /// <returns>The deserialized object</returns>
        public static T Deserialize(string xml)
        {
            var xmlFmtr = new System.Xml.Serialization.XmlSerializer(typeof(T));
            T instance = default(T);

            using (var inputStrm = new System.IO.StringReader(xml))
            {
                instance = (T)xmlFmtr.Deserialize(inputStrm);
            }

            return instance;
        }
    }
}