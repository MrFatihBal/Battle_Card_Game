using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolab1._2
{
	// DenizAraci.cs - Deniz araçları soyut sınıfı
	public abstract class DenizSınıfı : SavasAraci
	{
		protected string altSinif;

		public abstract int KaraVurusAvantaji { get; }
		public abstract string AltSinif { get; }

		protected DenizSınıfı(int seviyePuani = 0) : base(seviyePuani)
		{
			sinif = "Deniz";
		}
	}
}
