using Do_an.Models;

namespace Do_an.ViewModel
{
    public class CartItem
    {
        public TbProduct Product { get; set; }
        public int Mahh { get; set; }
        public string Hinh { get; set; }
        public string TenHH { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public double ThanhTien =>SoLuong * DonGia;

	}
		
}
