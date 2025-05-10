using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolab1._2.Scriptler
{
	// Firkateyn.cs
	public class Firkateyn : DenizSınıfı
	{
		public override int Dayaniklilik { get; set; } = 25;
		public override int Vurus { get; set; } = 15;
		public override string Sinif => "Deniz";
		public override string AltSinif => "Fırkateyn";

		public override int KaraVurusAvantaji => throw new NotImplementedException();

		public Firkateyn(int seviyePuani = 0) : base(seviyePuani) 
		{
			KartTuru = "Firkateyn";
		}

		public override void DurumGuncelle(int saldiriDegeri)
		{
			Dayaniklilik -= saldiriDegeri;
		}
	}
}
