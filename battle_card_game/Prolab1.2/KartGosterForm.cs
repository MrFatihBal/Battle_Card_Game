using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prolab1._2
{
	// Çekilen kartı gösterecek form
	public partial class KartGosterForm : Form
	{
		public KartGosterForm(SavasAraci kart)
		{
			InitializeComponent();

			// Form ayarları
			this.FormBorderStyle = FormBorderStyle.None;
			this.StartPosition = FormStartPosition.CenterParent;
			this.Size = new Size(300, 400);

			// Kart bilgilerini göster
			PictureBox picKart = new PictureBox
			{
				Size = new Size(200, 300),
				Location = new Point(50, 20),
				SizeMode = PictureBoxSizeMode.StretchImage,
				Image = (Image)Properties.Resources.ResourceManager.GetObject(kart.GetType().Name.ToLower())
			};

			Label lblBilgi = new Label
			{
				AutoSize = true,
				Location = new Point(50, 330),
				Text = $"Tür: {kart.Sinif}\nDayanıklılık: {kart.Dayaniklilik}\nVuruş: {kart.Vurus}"
			};

			Button btnKapat = new Button
			{
				Text = "Tamam",
				Location = new Point(100, 360),
				DialogResult = DialogResult.OK
			};

			this.Controls.AddRange(new Control[] { picKart, lblBilgi, btnKapat });
		}

		private void KartGosterForm_Load(object sender, EventArgs e)
		{

		}
	}
}
