using GETASIS_Muhasebe.Entities;

namespace GETASIS_Muhasebe.MVC.Models
{
    public class RaporViewModel
    {
        public List<Odeme>? Odeme { get; set; }

        public List<Cari>? Cari { get; set; }
        public int ay { get; set; }

    }
}
