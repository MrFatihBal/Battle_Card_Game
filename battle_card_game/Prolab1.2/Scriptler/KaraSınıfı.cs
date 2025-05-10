using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolab1._2
{
	// KaraAraci.cs - Kara araçları soyut sınıfı
	public abstract class KaraSınıfı : SavasAraci
	{
		protected string altSinif;

		public abstract int HavaVurusAvantaji { get; }
		public abstract string AltSinif { get; }

		protected KaraSınıfı(int seviyePuani = 0) : base(seviyePuani)
		{
			sinif = "Kara";
		}
	}

}
