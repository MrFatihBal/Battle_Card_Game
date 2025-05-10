using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolab1._2
{
	// HavaAraci.cs - Hava araçları soyut sınıfı
	public abstract class HavaSınıfı : SavasAraci
	{
		protected string altSinif;

		public abstract int DenizVurusAvantaji { get; }
		public abstract string AltSinif { get; }

		protected HavaSınıfı(int seviyePuani = 0) : base(seviyePuani)
		{
			sinif = "Hava";
		}
	}
}
