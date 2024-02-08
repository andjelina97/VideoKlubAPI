using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace VideoKlubAPI.Models
{
    [Table("Film")]
    public class Film
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("NazivFilma")]
        [MaxLength(50)]
        public string NazivFilma { get; set; }

        [Column("Reziser")]
        [MaxLength(50)]
        public string Reziser { get; set; }

        [Column("Trajanje")]
        public string Trajanje { get; set; }

        [Column("GodIzlaska")]
        public int GodIzlaska { get; set; }

        [Column("Kolicina")]
        public int Kolicina { get; set; }

        [Column("Red")]
        public int Red { get; set; }

        [Column("PozURedu")]
        public int PozURedu { get; set; }

        [JsonIgnore]
        public Odeljenje Odeljenje { get; set; }
    }
}