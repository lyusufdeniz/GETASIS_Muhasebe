using GETASIS_Muhasebe.Entities;

namespace GETASIS_Muhasebe.MVC.Models
{
    public class DashViewModel
    {
        public List<Odeme>? Odeme { get; set; }
        public List<Hesap> Hesap { get; set; }
        public int year { get; set; }

    }
}
