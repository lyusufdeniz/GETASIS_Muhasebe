using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GETASIS_Muhasebe.Entities
{
    public class Hesap
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Hesap Adı Boş Bırakılamaz!")]
        public string Ad { get; set; }
        public Doviz? Doviz { get; set; }


        [ForeignKey("DovizId")]
        public int DovizId { get; set; }
        public double? AcilisBakiye { get; set; }
        public string? BankaAdi { get; set; }
        public string? Sube { get; set; }
        public string? No { get; set; }
        public string? IBAN { get; set; }
        //public ICollection<Odeme> Odeme { get; set; }

    }
}
