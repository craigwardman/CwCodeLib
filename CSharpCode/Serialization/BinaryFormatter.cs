namespace CwCodeLib.Serialization
{
    public static class BinaryFormatter<T>
    {
        public static byte[] Serialize(T instance)
        {
            if (instance != null)
            {
                var binarySerializer = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                byte[] serializedObject;
                using (System.IO.MemoryStream memStream = new System.IO.MemoryStream())
                {
                    binarySerializer.Serialize(memStream, instance);
                    serializedObject = memStream.ToArray();
                }

                return serializedObject;
            }
            else
            {
                return null;
            }
        }

        public static T Deserialize(byte[] bytes)
        {
            if (bytes != null)
            {
                var binarySerializer = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                object instance;
                using (System.IO.MemoryStream memStream = new System.IO.MemoryStream(bytes))
                {
                    instance = binarySerializer.Deserialize(memStream);
                }

                return (T)instance;
            }
            else
            {
                return default(T);
            }
        }
    }
}