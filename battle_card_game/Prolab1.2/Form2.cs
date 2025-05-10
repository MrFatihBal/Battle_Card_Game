using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Prolab1._2.Scriptler;

namespace Prolab1._2
{
	public partial class Form2 : Form
	{
		private Random random = new Random();
		private List<SavasAraci> oyuncuDeste;
		private List<SavasAraci> bilgisayarDeste;
		private List<SavasAraci> ortadakiKartlar;
		private List<SavasAraci> OyuncukullanilanKartlar = new List<SavasAraci>();
		private List<SavasAraci> BilgisayarkullanilanKartlar = new List<SavasAraci>();
		int oyuncuSeviyePuani = 0;
		int bilgisayarSeviyePuani = 0;
		private bool ilkEl = true;
		int hamleSayisi = 0;
		int maksimumHamleSayisi;
		int oyuncuSkor=0;
		int bilgisayarSkor=0;

		

		private const int MAKSIMUM_ORTA_KART = 3;

		public Form2(int seviyePuani, int maksimumHamle)
		{
			InitializeComponent();
			OyunuBaslat();
			this.oyuncuSeviyePuani = seviyePuani;
			this.bilgisayarSeviyePuani= seviyePuani;
			this.maksimumHamleSayisi = maksimumHamle;
		}

		private void OyunuBaslat()
		{
			ilkEl = true;
			oyuncuDeste = new List<SavasAraci>();
			bilgisayarDeste = new List<SavasAraci>();
			ortadakiKartlar = new List<SavasAraci>();

			for (int i = 0; i < 6; i++)
			{
				oyuncuDeste.Add(RandomKartCek(oyuncuSeviyePuani));
				bilgisayarDeste.Add(RandomKartCek(bilgisayarSeviyePuani));
			}

			KartlariGuncelle();
			btnKartCek.Enabled = false;
			btnSavas.Enabled = false;	
		}
		private void btnSavas_Click(object sender, EventArgs e)
		{
			try
			{
				BilgisayarKartlariniSec();

				if (ortadakiKartlar.Count < 3)
				{
					MessageBox.Show("Lütfen önce tüm kartları orta desteye yerleştirin!");
					return;
				}

				var oyuncuKartlar = new[]
				{
			picOyuncuOrtaKart1.Tag as SavasAraci,
			picOyuncuOrtaKart2.Tag as SavasAraci,
			picOyuncuOrtaKart3.Tag as SavasAraci
		};                                          
				var bilgisayarKartlar = new[]
				{
			picBilgisayarOrtaKart1.Tag as SavasAraci,
			picBilgisayarOrtaKart2.Tag as SavasAraci,
			picBilgisayarOrtaKart3.Tag as SavasAraci
		};
				for (int i = 0; i < 3; i++)
				{
					if (oyuncuKartlar[i] != null && bilgisayarKartlar[i] != null)
					{
						SavasYap(oyuncuKartlar[i], bilgisayarKartlar[i]);
						if (oyuncuKartlar[i].Dayaniklilik > 0)
							OyuncukullanilanKartlar.Add(oyuncuKartlar[i]);
						if (bilgisayarKartlar[i].Dayaniklilik > 0)
							BilgisayarkullanilanKartlar.Add(bilgisayarKartlar[i]);

						bilgisayarDeste.Remove(bilgisayarKartlar[i]);
					}
				}
				DayaniklilikOzetFormuAc(
					oyuncuKartlar[0], oyuncuKartlar[1], oyuncuKartlar[2],
					bilgisayarKartlar[0], bilgisayarKartlar[1], bilgisayarKartlar[2]
				);
				OrtadakiKartlariTemizle();
				oyuncuDeste.RemoveAll(k => OyuncukullanilanKartlar.Contains(k));
				bilgisayarDeste.RemoveAll(k => BilgisayarkullanilanKartlar.Contains(k));
				KartlariGuncelle();
				btnSavas.Enabled = false;
				if (ilkEl)
				{
					ilkEl = false; 
				}
                btnSavas.Enabled = false; 
				btnKartCek.Enabled = true;
				PuanGuncelle();
				HamleGuncelle();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Savaş sırasında hata oluştu: {ex.Message}");
			}
		}
		private int EkstraVurusAvantaji(SavasAraci saldiranKart, SavasAraci hedefKart)
		{
			if (saldiranKart is Siha && hedefKart is Obüs) 
				return 10;
			if (saldiranKart is Sida && hedefKart is Uçak) 
				return 15;
			if (saldiranKart is KFS && hedefKart is Firkateyn) 
				return 20;

			return 0; 
		}

		private void SavasYap(SavasAraci oyuncuKart, SavasAraci bilgisayarKart)
		{
			int oyuncuEkstraVurus = EkstraVurusAvantaji(oyuncuKart, bilgisayarKart);
			int bilgisayarEkstraVurus = EkstraVurusAvantaji(bilgisayarKart, oyuncuKart);

			int oyuncuSonDayaniklilik = oyuncuKart.Dayaniklilik - (bilgisayarKart.Vurus + bilgisayarEkstraVurus);
			int bilgisayarSonDayaniklilik = bilgisayarKart.Dayaniklilik - (oyuncuKart.Vurus + oyuncuEkstraVurus);

			oyuncuKart.Dayaniklilik = Math.Max(oyuncuSonDayaniklilik, 0); 
			bilgisayarKart.Dayaniklilik = Math.Max(bilgisayarSonDayaniklilik, 0);

			if (bilgisayarKart.Dayaniklilik <= 0)
			{
				oyuncuSeviyePuani += 10;
				bilgisayarDeste.Remove(bilgisayarKart);
				oyuncuSkor += Math.Min(10, bilgisayarKart.SeviyePuani);
			}
			if (oyuncuKart.Dayaniklilik <= 0)
			{
				bilgisayarSeviyePuani += 10;
				oyuncuDeste.Remove(oyuncuKart);
				bilgisayarSkor += Math.Min(10, oyuncuKart.SeviyePuani);
			}


			if (bilgisayarKart.Dayaniklilik > oyuncuKart.Dayaniklilik) 
			{
				bilgisayarDeste.Remove(bilgisayarKart);
				bilgisayarSkor += 10;
			}
			else if (bilgisayarKart.Dayaniklilik == oyuncuKart.Dayaniklilik)
			{
				bilgisayarDeste.Remove(bilgisayarKart);
				oyuncuDeste.Remove(oyuncuKart);
			}
			else
			{
				oyuncuDeste.Remove(oyuncuKart);
				oyuncuSkor += 10;
			}
		}
		private void OrtadakiKartlariTemizle()
		{
			PictureBox[] oyuncuOrtaKartlar = { picOyuncuOrtaKart1, picOyuncuOrtaKart2, picOyuncuOrtaKart3 };
			PictureBox[] bilgisayarOrtaKartlar = { picBilgisayarOrtaKart1, picBilgisayarOrtaKart2, picBilgisayarOrtaKart3 };
			foreach (var pb in oyuncuOrtaKartlar)
			{
				pb.Image = null; 
				pb.Tag = null;  
			}
			foreach (var pb in bilgisayarOrtaKartlar)
			{
				pb.Image = null; 
				pb.Tag = null;   
			}

			ortadakiKartlar.Clear();
		}

		private void DayaniklilikOzetFormuAc(
	    SavasAraci oyuncuKart1, SavasAraci oyuncuKart2, SavasAraci oyuncuKart3,
	    SavasAraci bilgisayarKart1, SavasAraci bilgisayarKart2, SavasAraci bilgisayarKart3)
		{
			Form ozetForm = new Form
			{
				Text = "Dayanıklılık Özeti",
				Size = new Size(400, 300)
			};

			var kartlar = new[]
			{
		$"Oyuncu Kart 1: {oyuncuKart1?.KartTuru ?? "Yok"} - Dayanıklılık: {oyuncuKart1?.Dayaniklilik ?? 0}",
		$"Oyuncu Kart 2: {oyuncuKart2?.KartTuru ?? "Yok"} - Dayanıklılık: {oyuncuKart2?.Dayaniklilik ?? 0}",
		$"Oyuncu Kart 3: {oyuncuKart3?.KartTuru ?? "Yok"} - Dayanıklılık: {oyuncuKart3?.Dayaniklilik ?? 0}",
		$"Bilgisayar Kart 1: {bilgisayarKart1?.KartTuru ?? "Yok"} - Dayanıklılık: {bilgisayarKart1?.Dayaniklilik ?? 0}",
		$"Bilgisayar Kart 2: {bilgisayarKart2?.KartTuru ?? "Yok"} - Dayanıklılık: {bilgisayarKart2?.Dayaniklilik ?? 0}",
		$"Bilgisayar Kart 3: {bilgisayarKart3?.KartTuru ?? "Yok"} - Dayanıklılık: {bilgisayarKart3?.Dayaniklilik ?? 0}"
	        };

			int y = 10;
			foreach (var kart in kartlar)
			{
				Label lbl = new Label { Text = kart, Location = new Point(10, y), AutoSize = true };
				ozetForm.Controls.Add(lbl);
				y += 30;
			}

			ozetForm.ShowDialog();
		}

		private SavasAraci RandomKartCek(int oyuncuSeviyePuani)
		{
			List<SavasAraci> kartHavuzu = new List<SavasAraci>
			{
				new Obüs(),
				new Firkateyn(),
				new Uçak()
			};

			if (oyuncuSeviyePuani > 20)
			{
				kartHavuzu.Add(new KFS());
				kartHavuzu.Add(new Siha());
				kartHavuzu.Add(new Sida());
			}

			int index = random.Next(kartHavuzu.Count);
			return kartHavuzu[index];
		}

		private void OyuncuKartYerineEkle(SavasAraci yeniKart, PictureBox[] kartlar)
		{
			foreach (var pb in kartlar)
			{
				if (pb.Tag == null)
				{
					pb.Tag = yeniKart;
					KartResmiEkle(pb, yeniKart.KartTuru);
					break;
				}
			}
		}
		private void BilgisayarKartYerineEkle(SavasAraci yeniKart, PictureBox[] kartlar)
		{
			foreach (var pb in kartlar)
			{
				if (pb.Tag == null)
				{
					pb.Tag = yeniKart;
					KapaliKartResmiEkle(pb);
					break;
				}
			}
		}

		private void KartResmiEkle(PictureBox pictureBox, string kartTuru)
		{
			string resimYolu = "";
			if(kartTuru == "Obüs")
			resimYolu = @"C:\Users\Fatih Bal\source\repos\Prolab1.2\Prolab1.2\realimage\obus.jpg";
			if (kartTuru == "Uçak")
			resimYolu = @"C:\Users\Fatih Bal\source\repos\Prolab1.2\Prolab1.2\realimage\ucak.jpg";
			if (kartTuru == "Firkateyn")
			resimYolu = @"C:\Users\Fatih Bal\source\repos\Prolab1.2\Prolab1.2\realimage\firkateyn.jpg";
			if (kartTuru == "KFS")
			resimYolu = @"C:\Users\Fatih Bal\source\repos\Prolab1.2\Prolab1.2\realimage\kfs.jpg";
			if (kartTuru == "Siha")
			resimYolu = @"C:\Users\Fatih Bal\source\repos\Prolab1.2\Prolab1.2\realimage\siha.jpg";
			if (kartTuru == "Sida")
			resimYolu = @"C:\Users\Fatih Bal\source\repos\Prolab1.2\Prolab1.2\realimage\sida.jpg";
			try
			{
				if (File.Exists(resimYolu))
				{
					pictureBox.Image = Image.FromFile(resimYolu);
					pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
				}
				else
				{
					MessageBox.Show($"Resim bulunamadı: {resimYolu}");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Resim yüklenirken hata oluştu: {ex.Message}");
			}
		}
		private bool yeniKartCekildi = false; 

		private void OyunAkisKontrolu()
		{
			if (ortadakiKartlar.Count >= MAKSIMUM_ORTA_KART && yeniKartCekildi)
			{
				btnSavas.Enabled = true;
				yeniKartCekildi = false;
			}
			else
			{
				btnSavas.Enabled = false; 
			}
		}
		private void OrtaDesteyeKartEkle(PictureBox oyuncuKart)
		{
			if (ortadakiKartlar.Count >= MAKSIMUM_ORTA_KART)
			{
				MessageBox.Show("Orta deste dolu!");
				return;
			}

			var secilenKart = oyuncuKart.Tag as SavasAraci;
			if (secilenKart == null)
			{
				MessageBox.Show("Geçersiz kart!");
				return;
			}
			PictureBox hedefPictureBox = null;
			if (picOyuncuOrtaKart1.Tag == null) hedefPictureBox = picOyuncuOrtaKart1;
			else if (picOyuncuOrtaKart2.Tag == null) hedefPictureBox = picOyuncuOrtaKart2;
			else if (picOyuncuOrtaKart3.Tag == null) hedefPictureBox = picOyuncuOrtaKart3;

			if (hedefPictureBox != null)
			{
				ortadakiKartlar.Add(secilenKart);
				hedefPictureBox.Tag = secilenKart;
				KartResmiEkle(hedefPictureBox, secilenKart.KartTuru);

				oyuncuDeste.Remove(secilenKart);
				oyuncuKart.Image = null;
				oyuncuKart.Tag = null;

				if (ortadakiKartlar.Count == MAKSIMUM_ORTA_KART)
				{
					BilgisayarKartlariniSec();
					if (ilkEl)
						btnSavas.Enabled = true; 
					else
						OyunAkisKontrolu();
				}
			}
		}
		private void BilgisayarKartlariniSec()
		{
			ortadakiKartlar.Clear(); 

			Random rastgele = new Random();
			List<SavasAraci> karisikDeste = bilgisayarDeste.OrderBy(x => rastgele.Next()).ToList(); 

			for (int i = 0; i < MAKSIMUM_ORTA_KART; i++)
			{
				if (i >= karisikDeste.Count)
					break; 

				var bilgisayarKart = karisikDeste[i]; 
				PictureBox hedefPB = (PictureBox)Controls.Find($"picBilgisayarOrtaKart{i + 1}", true)[0];
				hedefPB.Tag = bilgisayarKart; 
				KartResmiEkle(hedefPB, bilgisayarKart.KartTuru);

				ortadakiKartlar.Add(bilgisayarKart); 
			}
		}
		private void KartlariGuncelle()
		{
			PictureBox[] oyuncuKartlar = { picOyuncuKart1, picOyuncuKart2, picOyuncuKart3, picOyuncuKart4, picOyuncuKart5, picOyuncuKart6 };
			PictureBox[] bilgisayarKartlar = { picBilgisayarKart1, picBilgisayarKart2, picBilgisayarKart3, picBilgisayarKart4, picBilgisayarKart5, picBilgisayarKart6 };
			for (int i = 0; i < oyuncuKartlar.Length; i++)
			{
				if (i < oyuncuDeste.Count)
				{
					oyuncuKartlar[i].Tag = oyuncuDeste[i];
					KartResmiEkle(oyuncuKartlar[i], oyuncuDeste[i].KartTuru);
				}
				else
				{
					oyuncuKartlar[i].Tag = null;
					oyuncuKartlar[i].Image = null;
				}
			}
			for (int i = 0; i < bilgisayarKartlar.Length; i++)
			{
				if (i < bilgisayarDeste.Count)
				{
					bilgisayarKartlar[i].Tag = bilgisayarDeste[i];
					KapaliKartResmiEkle(bilgisayarKartlar[i]);
				}
				else
				{
					bilgisayarKartlar[i].Tag = null;
					bilgisayarKartlar[i].Image = null;
				}
			}

			KapaliKartResmiEkle(picDeste);
			KapaliKartResmiEkle(picOyuncuEskiKart);
			KapaliKartResmiEkle(picBilgisayarOrtaKart1);
			KapaliKartResmiEkle(picBilgisayarOrtaKart2);
			KapaliKartResmiEkle(picBilgisayarOrtaKart3);
		}
		private void picOyuncuKart1_DoubleClick(object sender, EventArgs e) => OrtaDesteyeKartEkle((PictureBox)sender);
		private void picOyuncuKart2_DoubleClick(object sender, EventArgs e) => OrtaDesteyeKartEkle((PictureBox)sender);
		private void picOyuncuKart3_DoubleClick(object sender, EventArgs e) => OrtaDesteyeKartEkle((PictureBox)sender);
		private void picOyuncuKart4_DoubleClick(object sender, EventArgs e) => OrtaDesteyeKartEkle((PictureBox)sender);
		private void picOyuncuKart5_DoubleClick(object sender, EventArgs e) => OrtaDesteyeKartEkle((PictureBox)sender);
		private void picOyuncuKart6_DoubleClick(object sender, EventArgs e) => OrtaDesteyeKartEkle((PictureBox)sender);

		private void btnKartCek_Click(object sender, EventArgs e)
		{
			int oyuncuKartSayisi = oyuncuDeste.Count;
			int bilgisayarKartSayisi = bilgisayarDeste.Count;
			int oyuncuCekilecekKartSayisi = (oyuncuKartSayisi == 1) ? 2 : 1;
			int bilgisayarCekilecekKartSayisi = (bilgisayarKartSayisi == 1) ? 2 : 1;

			for (int i = 0; i < oyuncuCekilecekKartSayisi; i++)
			{
				SavasAraci yeniOyuncuKart = RandomKartCek(oyuncuSeviyePuani);
				oyuncuDeste.Add(yeniOyuncuKart); 
				OyuncuKartYerineEkle(yeniOyuncuKart, new[] { picOyuncuKart1, picOyuncuKart2, picOyuncuKart3, picOyuncuKart4, picOyuncuKart5, picOyuncuKart6 });
			}
			for (int i = 0; i < bilgisayarCekilecekKartSayisi; i++)
			{
				SavasAraci yeniBilgisayarKart = RandomKartCek(bilgisayarSeviyePuani);
				bilgisayarDeste.Add(yeniBilgisayarKart); 
				BilgisayarKartYerineEkle(yeniBilgisayarKart, new[] { picBilgisayarKart1, picBilgisayarKart2, picBilgisayarKart3, picBilgisayarKart4, picBilgisayarKart5, picBilgisayarKart6 });
			}
			MessageBox.Show("Yeni kartlar dağıtıldı!");
			yeniKartCekildi = true;
			btnKartCek.Enabled = false;
			OyunAkisKontrolu();
		}
		private void btnKartGetir_Click(object sender, EventArgs e)
		{
			if (oyuncuDeste.Count < 2)
			{
				int OyuncuBosYerSayisi = 6 - oyuncuDeste.Count;
				int BilgisayarBosYerSayisi = 6 - bilgisayarDeste.Count;

				foreach (var kart in OyuncukullanilanKartlar.ToList())
				{
					if (OyuncuBosYerSayisi <= 0)
						break;

					if (kart.Dayaniklilik > 0)
					{
						oyuncuDeste.Add(kart);
						OyuncukullanilanKartlar.Remove(kart);
						OyuncuBosYerSayisi--;
					}
				}

				foreach (var kart in BilgisayarkullanilanKartlar.ToList())
				{
					if (BilgisayarBosYerSayisi <= 0)
						break;

					if (kart.Dayaniklilik > 0)
					{
						bilgisayarDeste.Add(kart);
						BilgisayarkullanilanKartlar.Remove(kart);
						BilgisayarBosYerSayisi--;
					}
				}
				KartlariGuncelle();

				if (OyuncukullanilanKartlar.Any(k => k.Dayaniklilik > 0) || BilgisayarkullanilanKartlar.Any(k => k.Dayaniklilik > 0))
				{
					MessageBox.Show("Tüm kartlar getirilemedi. Kalan kartları daha sonra 'Kart Getir' butonuna basarak alabilirsiniz.");
				}
				else
				{
					MessageBox.Show("Tüm kullanılabilir kartlar destelere geri getirildi!");
				}

			}
			else 
			{
				MessageBox.Show("daha oynanacak kartın var");			
			}
		}
		void OyunuBitirKontrol()
		{
			if (oyuncuDeste.Count+OyuncukullanilanKartlar.Count == 0 || bilgisayarDeste.Count + BilgisayarkullanilanKartlar.Count == 0)
			{
				KazananBelirle();
			}
			else if (hamleSayisi >= maksimumHamleSayisi)
			{
				SkorlarlaKazananBelirle();
			}
		}
		void KazananBelirle()
		{
			DialogResult sonuc = MessageBox.Show("Oyun bitti kazananı görmek ister misin ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (sonuc == DialogResult.Yes)
			{
				if (oyuncuDeste.Count == 0)
				{
					MessageBox.Show("Bilgisayar kazandı!");
					this.Close();
				}


				else
				{
					MessageBox.Show("Bilgisayar kazandı!");
					this.Close();
				}
			}
			else
				this.Close() ;
		}
		void SkorlarlaKazananBelirle()
		{
			int oyuncuToplamDayaniklilik = oyuncuDeste.Sum(kart => kart.Dayaniklilik);
			int bilgisayarToplamDayaniklilik = bilgisayarDeste.Sum(kart => kart.Dayaniklilik);
			DialogResult sonuc = MessageBox.Show("Oyun bitti kazananı görmek ister misin ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (sonuc == DialogResult.Yes)
            {
				if (oyuncuSkor > bilgisayarSkor) 
				{
				    MessageBox.Show("Oyuncu Skorla kazandı!");
					this.Close();
				}
				else if (bilgisayarSkor > oyuncuSkor) 
				{
					MessageBox.Show("Bilgisayar Skorla kazandı!");
					this.Close();
				}
					
				else
				{
					if (oyuncuToplamDayaniklilik > bilgisayarToplamDayaniklilik)
					{
						MessageBox.Show($"Oyuncu dayanıklılık farkıyla kazandı! Dayanıklılık farkı: {oyuncuToplamDayaniklilik - bilgisayarToplamDayaniklilik}");
						oyuncuSkor += oyuncuToplamDayaniklilik - bilgisayarToplamDayaniklilik;
						this.Close();
					}

					else if (bilgisayarToplamDayaniklilik > oyuncuToplamDayaniklilik)
					{
						MessageBox.Show($"Bilgisayar dayanıklılık farkıyla kazandı! Dayanıklılık farkı: {bilgisayarToplamDayaniklilik - oyuncuToplamDayaniklilik}");
						bilgisayarSkor += bilgisayarToplamDayaniklilik - oyuncuToplamDayaniklilik;
						this.Close();
					}
					else 
					{
						MessageBox.Show("dayanıklılıktada fark yok berabere");
						this.Close();
					}
						
				}
			}
			else
				this.Close();
		}
		private void Form2_Load(object sender, EventArgs e)
		{
			lblBilgisayarPuan.Text = "Bilgisayar Puanı: " + bilgisayarSkor;
			lblOyuncuPuan.Text = "Oyuncu Puanı: " + oyuncuSkor;
			lblMaksimumHamle.Text = "Maksimum Hamle: " + maksimumHamleSayisi;
			lblHamleSayisi.Text = "Hamle: " + hamleSayisi;
		}
		void HamleGuncelle()
		{
			hamleSayisi++;
			lblHamleSayisi.Text = "Hamle: " + hamleSayisi;
			OyunuBitirKontrol();
		}

		void PuanGuncelle()
		{
			lblBilgisayarPuan.Text = "Bilgisayar Puanı: " + bilgisayarSkor;
			lblOyuncuPuan.Text = "Oyuncu Puanı: " + oyuncuSkor;
		}
		private void KapaliKartResmiEkle(PictureBox pictureBox)
		{
			string kapaliKartYolu = Path.Combine(@"C:\Users\Fatih Bal\source\repos\Prolab1.2\Prolab1.2\Images", "kapalıdeste.png");

			if (File.Exists(kapaliKartYolu))
			{
				pictureBox.Image = Image.FromFile(kapaliKartYolu); 
			}
			else
			{
				MessageBox.Show("Kapalı kart resmi bulunamadı.");
			}
		}
	}
}
