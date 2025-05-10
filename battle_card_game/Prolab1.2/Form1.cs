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
	// Form1.cs
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.StartPosition = FormStartPosition.CenterScreen;
		}

		private void btnOyunaBasla_Click(object sender, EventArgs e)
		{
			if (!int.TryParse(txtSeviyePuani.Text, out int seviyePuani) || seviyePuani < 0)
			{
				MessageBox.Show("Lütfen geçerli bir seviye puanı girin!");
				return;
			}

			if (!int.TryParse(txtMaksimumHamle.Text, out int maksimumHamle) || maksimumHamle <= 0)
			{
				MessageBox.Show("Lütfen geçerli bir maksimum hamle sayısı girin!");
				return;
			}
			Form2 oyunForm = new Form2(seviyePuani, maksimumHamle);
			this.Hide();
			oyunForm.ShowDialog();
			this.Close();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

	}
}
