using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Personenverwaltung
{
    [Table("persons", Schema = "ef")]
    public class Person
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }

        [Column("full_name")]
        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [Column("email")]
        [JsonProperty("email")]
        public string Email { get; set; }

        [Column("address")]
        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("hobbies")]
        public List<Hobby> Hobbies { get; set; }
    }

    [Table("addresses", Schema = "ef")]
    public class Address
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }

        [Column("street")]
        [JsonProperty("street")]
        public string Street { get; set; }

        [Column("city")]
        [JsonProperty("city")]
        public string City { get; set; }

        [Column("postal")]
        [JsonProperty("postal")]
        public string Postal { get; set; }
    }

    [Table("hobbies", Schema = "ef")]
    public class Hobby
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Column("name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonIgnore]
        public List<Person> Persons { get; set; }
    }
}
