using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GETASIS_Muhasebe.Entities
{
    public class Odeme
    {
        [Key]
        public int Id { get; set; }
        public decimal? Alacak { get; set; }
        public decimal? Borc { get; set; }
        [Required(ErrorMessage = "Ödeme Tarihi Boş Bırakılamaz!")]
        public DateTime? Tarih { get; set; }
        public string? BelgeNo { get; set; }
        [DefaultValue(null)]
        public DateTime? VadeTarihi { get; set; }
        [Required(ErrorMessage = "Ödeme Açıklaması Boş Bırakılamaz!")]
        public string Aciklama { get; set; }

        public Cari? Cari { get; set; }
        [Required(ErrorMessage = "Cari Boş Bırakılamaz!")]
        [ForeignKey("CariId")]
        public int CariId { get; set; }
        public Hesap? Hesap { get; set; }
        [Required(ErrorMessage = "Hesap Boş Bırakılamaz!")]
        [ForeignKey("HesapId")]
        public int HesapId { get; set; }

        public OdemeTip? OdemeTip { get; set; }

        [Required(ErrorMessage = "Ödeme Tipi Boş Bırakılamaz!")]
        [ForeignKey("OdemeTipId")]
        public int OdemeTipId { get; set; }
        public int Silindi { get; set; }




    }
}
