using System.ComponentModel.DataAnnotations;

namespace GETASIS_Muhasebe.Entities
{
    public class Cari
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Cari Ad Alanı Boş Bırakılamaz!")]
        public string Ad { get; set; }
        [Required(ErrorMessage = "Cari Ad Soyad Alanı Boş Bırakılamaz!")]
        public string AdSoyad { get; set; }

        public string? Mail { get; set; }
        public string? VergiDairesi { get; set; }
        public string? VergiNo { get; set; }
        public string? Adres { get; set; }
        //public ICollection<Odeme>? Odeme { get; set; }

    }
}
