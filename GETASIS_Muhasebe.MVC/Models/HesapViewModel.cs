using GETASIS_Muhasebe.Entities;
using GETASIS_Muhasebe.MVC.Filters;

namespace GETASIS_Muhasebe.MVC.Models
{
	public class HesapViewModel

	{

		public List<Hesap> Hesap { get; set; }
		public HesapFilter HesapFilter { get; set; }
	}
}
