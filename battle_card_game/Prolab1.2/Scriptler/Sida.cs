using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolab1._2.Scriptler
{
	// Sida.cs
	public class Sida : DenizSınıfı
	{
		public override int Dayaniklilik { get; set; } = 15;
		public override int Vurus { get; set; } = 10;
		public override string Sinif => "Deniz";
		public override int KaraVurusAvantaji => 3;
		public override string AltSinif => "SİDA";
		private int karaVurusAvantaji = 2;

		public Sida(int seviyePuani = 0) : base(seviyePuani) 
		{
			KartTuru = "Sida";
		}

		public override void DurumGuncelle(int saldiriDegeri)
		{
			Dayaniklilik -= saldiriDegeri;
		}
	}
}
