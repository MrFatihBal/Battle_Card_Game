using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Prolab1._2.Form2;

namespace Prolab1._2
{
	// SavasAraci.cs - Ana soyut sınıf
	
	public abstract class SavasAraci
	{
		protected int dayaniklilik;
		protected int seviyePuani;
		protected int vurus;
		protected string sinif;
		public virtual int Dayaniklilik
		{
			get => Dayaniklilik;
			set => Dayaniklilik = value;
		}
		public int SeviyePuani { get; set; }
		public virtual int Vurus
		{
			get => Vurus;
			set => Vurus = value;
		}
		public abstract string Sinif { get; }
		public string KartTuru { get; set; }
		public Image Image { get; internal set; }

		protected SavasAraci(int dayaniklilik, int vurus)
		{
			Dayaniklilik = dayaniklilik;
			Vurus = vurus;
		}
		public SavasAraci(int seviyePuani = 0)
		{
			this.seviyePuani = seviyePuani;
		}
		public virtual void KartPuaniGoster()
		{
			// Kartın puanlarını gösterme mantığı
		}

		public abstract void DurumGuncelle(int saldiriDegeri);
	}
}
