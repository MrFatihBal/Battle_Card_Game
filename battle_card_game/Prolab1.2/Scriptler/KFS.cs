using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolab1._2.Scriptler
{
	// KFS.cs
	public class KFS : KaraSınıfı
	{
		public override int Dayaniklilik { get; set; } = 10;
		public override int Vurus { get; set; } = 20;
		public override string Sinif => "Kara";
		public override int HavaVurusAvantaji => 3;
		public override string AltSinif => "KFS";
		private int havaVurusAvantaji = 2;

		public KFS(int seviyePuani = 0) : base(seviyePuani) 
		{
			KartTuru = "KFS";
		}

		public override void DurumGuncelle(int saldiriDegeri)
		{
			Dayaniklilik -= saldiriDegeri;
		}
	}
}
