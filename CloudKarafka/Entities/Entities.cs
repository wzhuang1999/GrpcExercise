using Confluent.Kafka;
using ProtoBuf;
using System;
using System.IO;

namespace Entities
{
    [ProtoContract]
    public class DummyString
    {
        [ProtoMember(1)]
        public int Version { get; set; }

        [ProtoMember(2)]
        public string Fieldname { get; set; }

        public override string ToString()
        {
            return $"{{Version={Version} Fieldname={Fieldname}}}";
        }

        public DummyString Clone()
        {
            return new DummyString
            {
                Version = this.Version,
                Fieldname = this.Fieldname
            };
        }
    }

    [ProtoContract]
    public class Person
    {
        [ProtoMember(1)]
        public int Id { get; set; }

        [ProtoMember(2)]
        public DummyString Fullname { get; set; }

        [ProtoMember(3)]
        public DummyString Email { get; set; }

        public override string ToString()
        {
            return $"Id={Id}, Fullname={Fullname}, Email={Email}";
        }

        public Person Clone()
        {
            return new Person
            {
                Id = this.Id,
                Fullname = this.Fullname.Clone(),
                Email = this.Email.Clone()               
            };
        }
    }   

    public class ProtoBufSerializer : ISerializer<Person>
    {
        public byte[] Serialize(Person data, Confluent.Kafka.SerializationContext context)
        {
            using (var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, data);

                return stream.ToArray();
            }
        }
    }

    public class ProtoDeserializer : IDeserializer<Person>
    {
        public Person Deserialize(ReadOnlySpan<byte> data, bool isNull, Confluent.Kafka.SerializationContext context)
        {
            using (var stream = new MemoryStream(data.ToArray()))
            {
                return Serializer.Deserialize<Person>(stream);
            }
        }
    }
}
