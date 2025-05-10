using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolab1._2.Scriptler
{
	// Obus.cs
	public class Obüs : KaraSınıfı
	{
		public override int Dayaniklilik { get; set; } = 20;
		public override int Vurus { get; set; } = 15;
		public override string Sinif => "Kara";

		public override string AltSinif => "Obüs";

		public override int HavaVurusAvantaji => throw new NotImplementedException();

		public Obüs(int seviyePuani = 0) : base(seviyePuani) 
		{
			KartTuru = "Obüs";
		}

		public override void DurumGuncelle(int saldiriDegeri)
		{
			Dayaniklilik -= saldiriDegeri;
		}
	}
}
