using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolab1._2.Scriptler
{// Siha.cs
	public class Siha : HavaSınıfı
	{
		public override int Dayaniklilik { get; set; } = 15;
		public override int Vurus { get; set; } = 10;
		public override string Sinif => "Hava";
		public override int DenizVurusAvantaji => 3;
		public override string AltSinif => "SİHA";

		public Siha(int seviyePuani = 0) : base(seviyePuani) 
		{
			KartTuru = "Siha";
		}

		public override void DurumGuncelle(int saldiriDegeri)
		{
			Dayaniklilik -= saldiriDegeri;
		}
	}

}
