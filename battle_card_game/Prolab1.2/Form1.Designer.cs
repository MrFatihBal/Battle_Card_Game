namespace Prolab1._2
{
	partial class Form1
	{
		/// <summary>
		///Gerekli tasarımcı değişkeni.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///Kullanılan tüm kaynakları temizleyin.
		/// </summary>
		///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer üretilen kod

		/// <summary>
		/// Tasarımcı desteği için gerekli metot - bu metodun 
		///içeriğini kod düzenleyici ile değiştirmeyin.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnOyunaBasla = new System.Windows.Forms.Button();
			this.txtSeviyePuani = new System.Windows.Forms.TextBox();
			this.txtMaksimumHamle = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnOyunaBasla
			// 
			this.btnOyunaBasla.Location = new System.Drawing.Point(484, 383);
			this.btnOyunaBasla.Name = "btnOyunaBasla";
			this.btnOyunaBasla.Size = new System.Drawing.Size(271, 120);
			this.btnOyunaBasla.TabIndex = 1;
			this.btnOyunaBasla.Text = "oyuna başla";
			this.btnOyunaBasla.UseVisualStyleBackColor = true;
			this.btnOyunaBasla.Click += new System.EventHandler(this.btnOyunaBasla_Click);
			// 
			// txtSeviyePuani
			// 
			this.txtSeviyePuani.Location = new System.Drawing.Point(427, 265);
			this.txtSeviyePuani.Name = "txtSeviyePuani";
			this.txtSeviyePuani.Size = new System.Drawing.Size(100, 20);
			this.txtSeviyePuani.TabIndex = 2;
			// 
			// txtMaksimumHamle
			// 
			this.txtMaksimumHamle.Location = new System.Drawing.Point(719, 265);
			this.txtMaksimumHamle.Name = "txtMaksimumHamle";
			this.txtMaksimumHamle.Size = new System.Drawing.Size(100, 20);
			this.txtMaksimumHamle.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(424, 249);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(66, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "seviye puani";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(716, 240);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "hamle sayisi";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1236, 652);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtMaksimumHamle);
			this.Controls.Add(this.txtSeviyePuani);
			this.Controls.Add(this.btnOyunaBasla);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOyunaBasla;
		private System.Windows.Forms.TextBox txtSeviyePuani;
		private System.Windows.Forms.TextBox txtMaksimumHamle;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}

