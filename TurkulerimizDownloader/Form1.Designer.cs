namespace TurkulerimizDownloader
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnFetchData = new Button();
            musicList = new ListBox();
            label1 = new Label();
            progressBar1 = new ProgressBar();
            lblProgress = new Label();
            btnStop = new Button();
            complatedList = new ListBox();
            label3 = new Label();
            btnOpenDir = new Button();
            downloadingNow = new Label();
            status = new Label();
            SuspendLayout();
            // 
            // btnFetchData
            // 
            btnFetchData.Enabled = false;
            btnFetchData.Location = new Point(252, 27);
            btnFetchData.Name = "btnFetchData";
            btnFetchData.Size = new Size(301, 49);
            btnFetchData.TabIndex = 0;
            btnFetchData.Text = "Başlat";
            btnFetchData.UseVisualStyleBackColor = true;
            btnFetchData.Click += btnFetchData_Click;
            // 
            // musicList
            // 
            musicList.FormattingEnabled = true;
            musicList.ItemHeight = 15;
            musicList.Location = new Point(12, 27);
            musicList.Name = "musicList";
            musicList.Size = new Size(203, 409);
            musicList.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(79, 15);
            label1.TabIndex = 2;
            label1.Text = "Tüm Müzikler";
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(221, 397);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(567, 39);
            progressBar1.TabIndex = 3;
            // 
            // lblProgress
            // 
            lblProgress.AutoSize = true;
            lblProgress.Location = new Point(221, 379);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(116, 15);
            lblProgress.TabIndex = 2;
            lblProgress.Text = "0/16545 Tamamlandı";
            // 
            // btnStop
            // 
            btnStop.Enabled = false;
            btnStop.Location = new Point(252, 82);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(301, 49);
            btnStop.TabIndex = 0;
            btnStop.Text = "Durdur";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // complatedList
            // 
            complatedList.FormattingEnabled = true;
            complatedList.ItemHeight = 15;
            complatedList.Location = new Point(585, 27);
            complatedList.Name = "complatedList";
            complatedList.Size = new Size(203, 364);
            complatedList.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(585, 9);
            label3.Name = "label3";
            label3.Size = new Size(47, 15);
            label3.TabIndex = 2;
            label3.Text = "İndirildi";
            // 
            // btnOpenDir
            // 
            btnOpenDir.Enabled = false;
            btnOpenDir.Location = new Point(252, 137);
            btnOpenDir.Name = "btnOpenDir";
            btnOpenDir.Size = new Size(301, 49);
            btnOpenDir.TabIndex = 0;
            btnOpenDir.Text = "İndirilen Dizini Aç";
            btnOpenDir.UseVisualStyleBackColor = true;
            btnOpenDir.Click += btnOpenDir_Click;
            // 
            // downloadingNow
            // 
            downloadingNow.AutoSize = true;
            downloadingNow.Location = new Point(221, 226);
            downloadingNow.Name = "downloadingNow";
            downloadingNow.Size = new Size(0, 15);
            downloadingNow.TabIndex = 2;
            // 
            // status
            // 
            status.AutoSize = true;
            status.Location = new Point(321, 265);
            status.Name = "status";
            status.Size = new Size(162, 15);
            status.TabIndex = 2;
            status.Text = "Liste alınıyor lütfen bekleyiniz";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(progressBar1);
            Controls.Add(downloadingNow);
            Controls.Add(lblProgress);
            Controls.Add(label3);
            Controls.Add(status);
            Controls.Add(label1);
            Controls.Add(complatedList);
            Controls.Add(musicList);
            Controls.Add(btnOpenDir);
            Controls.Add(btnStop);
            Controls.Add(btnFetchData);
            Name = "Form1";
            Text = "Türkü İndirici";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnFetchData;
        private ListBox musicList;
        private Label label1;
        private ProgressBar progressBar1;
        private Label lblProgress;
        private Button btnStop;
        private ListBox complatedList;
        private Label label3;
        private Button btnOpenDir;
        private Label downloadingNow;
        private Label status;
    }
}
