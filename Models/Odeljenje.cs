using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace VideoKlubAPI.Models
{
    [Table("Odeljenje")]
    public class Odeljenje
    {

        [Key]
        [Column("ID")]
        public int  ID { get; set; }

        [Column("Naziv")]
        [MaxLength(100)]
        public string Naziv { get; set; }

        [Column("BrojRedova")]
        public int BrojRedova{ get; set; }

        [Column("BrojPolicaPoRedu")]
        public int BrojPolicaPoRedu { get; set; }

        public virtual List<Film> Filmovi { get; set; }

        [JsonIgnore]
        public VideoKlub Klub { get; set;}
    }

}