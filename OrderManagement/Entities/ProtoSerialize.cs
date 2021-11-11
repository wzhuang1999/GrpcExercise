using ProtoBuf;
using System.IO;

namespace Entities
{
    public static class ProtoSerializer
    {
        public static byte[] Serialize<T>(T record) where T : class
        {
            if (null == record)
            {
                return null;
            }

            using (var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, record);
                return stream.ToArray();
            }
        }

        public static T Deserialize<T>(byte[] bytes)
        {
            return Serializer.Deserialize<T>(new MemoryStream(bytes));
        }
    }
}
