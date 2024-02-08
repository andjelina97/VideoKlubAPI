using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoKlubAPI.Models
{
    [Table("VideoKlub")]
    public class VideoKlub
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("Naziv")]
        [MaxLength(100)]
        public string Naziv { get; set; }

        [Column("Adresa")]
        public string Adresa { get; set; }

        //ne radi se o nekoj koloni u tabeli, vec o pokazivacu na drugu tabelu
        public virtual List<Odeljenje> Odeljenja {get; set;}
    }
}