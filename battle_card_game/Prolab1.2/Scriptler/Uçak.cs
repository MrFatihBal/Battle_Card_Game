using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolab1._2
{
	// Ucak.cs
	public class Uçak : HavaSınıfı
	{
		public override int Dayaniklilik { get; set; } = 20;
		public override int Vurus { get; set; } = 15;
		public override string Sinif => "Hava";
		public override string AltSinif => "Uçak";

		public override int DenizVurusAvantaji => throw new NotImplementedException();

		public Uçak(int seviyePuani = 0) : base(seviyePuani) 
		{
			KartTuru = "Uçak";
		}

		public override void DurumGuncelle(int saldiriDegeri)
		{
			Dayaniklilik -= saldiriDegeri;
		}
	}
}
