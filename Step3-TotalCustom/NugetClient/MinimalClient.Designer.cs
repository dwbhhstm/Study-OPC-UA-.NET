namespace NugetClient
{
    partial class MinimalClient
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConnect = new System.Windows.Forms.Button();
            this.cbEndpoint = new System.Windows.Forms.ComboBox();
            this.browseTreeCtrl1 = new Opc.Ua.Sample.Controls.BrowseTreeCtrl();
            this.btn01 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbNodeId01 = new System.Windows.Forms.TextBox();
            this.tbResult01 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbErrorMessage = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn02 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbNodeId01 = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lbResult01 = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.Location = new System.Drawing.Point(336, 15);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(80, 21);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_ClickAsync);
            // 
            // cbEndpoint
            // 
            this.cbEndpoint.FormattingEnabled = true;
            this.cbEndpoint.Items.AddRange(new object[] {
            "opc.tcp://192.168.33.195:53530/OPCUA/SimulationServer"});
            this.cbEndpoint.Location = new System.Drawing.Point(11, 15);
            this.cbEndpoint.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.cbEndpoint.Name = "cbEndpoint";
            this.cbEndpoint.Size = new System.Drawing.Size(318, 20);
            this.cbEndpoint.TabIndex = 3;
            // 
            // browseTreeCtrl1
            // 
            this.browseTreeCtrl1.AttributesCtrl = null;
            this.browseTreeCtrl1.EnableDragging = false;
            this.browseTreeCtrl1.Location = new System.Drawing.Point(11, 50);
            this.browseTreeCtrl1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.browseTreeCtrl1.Name = "browseTreeCtrl1";
            this.browseTreeCtrl1.SessionTreeCtrl = null;
            this.browseTreeCtrl1.Size = new System.Drawing.Size(405, 282);
            this.browseTreeCtrl1.TabIndex = 4;
            // 
            // btn01
            // 
            this.btn01.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn01.Location = new System.Drawing.Point(308, 22);
            this.btn01.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btn01.Name = "btn01";
            this.btn01.Size = new System.Drawing.Size(80, 50);
            this.btn01.TabIndex = 5;
            this.btn01.Text = "btn01";
            this.btn01.UseVisualStyleBackColor = true;
            this.btn01.Click += new System.EventHandler(this.btn01_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "NodeId:";
            // 
            // tbNodeId01
            // 
            this.tbNodeId01.Location = new System.Drawing.Point(58, 22);
            this.tbNodeId01.Name = "tbNodeId01";
            this.tbNodeId01.Size = new System.Drawing.Size(243, 21);
            this.tbNodeId01.TabIndex = 7;
            this.tbNodeId01.Text = "ns=3;i=1009";
            // 
            // tbResult01
            // 
            this.tbResult01.Location = new System.Drawing.Point(58, 51);
            this.tbResult01.Name = "tbResult01";
            this.tbResult01.ReadOnly = true;
            this.tbResult01.Size = new System.Drawing.Size(243, 21);
            this.tbResult01.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "Result:";
            // 
            // tbErrorMessage
            // 
            this.tbErrorMessage.Location = new System.Drawing.Point(11, 351);
            this.tbErrorMessage.Multiline = true;
            this.tbErrorMessage.Name = "tbErrorMessage";
            this.tbErrorMessage.ReadOnly = true;
            this.tbErrorMessage.Size = new System.Drawing.Size(405, 280);
            this.tbErrorMessage.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btn01);
            this.groupBox1.Controls.Add(this.tbResult01);
            this.groupBox1.Controls.Add(this.tbNodeId01);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(443, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(410, 96);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // btn02
            // 
            this.btn02.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn02.Location = new System.Drawing.Point(8, 17);
            this.btn02.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btn02.Name = "btn02";
            this.btn02.Size = new System.Drawing.Size(380, 33);
            this.btn02.TabIndex = 10;
            this.btn02.Text = "btn02";
            this.btn02.UseVisualStyleBackColor = true;
            this.btn02.Click += new System.EventHandler(this.btn02_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.lbResult01);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lbNodeId01);
            this.groupBox2.Controls.Add(this.btn02);
            this.groupBox2.Location = new System.Drawing.Point(443, 130);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(410, 501);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // lbNodeId01
            // 
            this.lbNodeId01.BackColor = System.Drawing.SystemColors.Window;
            this.lbNodeId01.FormattingEnabled = true;
            this.lbNodeId01.ItemHeight = 12;
            this.lbNodeId01.Items.AddRange(new object[] {
            "ns=3;i=1009",
            "ns=3;i=1010",
            "ns=3;i=1011",
            "ns=3;i=1012",
            "ns=3;i=1013",
            "ns=3;i=1014",
            "ns=3;i=1015",
            "ns=3;i=1016",
            "ns=3;i=1017",
            "ns=3;i=1018",
            "ns=3;i=1019",
            "ns=3;i=1020",
            "ns=3;i=1021",
            "ns=3;i=1022",
            "ns=3;i=1023",
            "ns=3;i=1024",
            "ns=3;i=1025",
            "ns=3;i=1026",
            "ns=3;i=1027",
            "ns=3;i=1028",
            "ns=3;i=1029",
            "ns=3;i=1030",
            "ns=3;i=1031",
            "ns=3;i=1032",
            "ns=3;i=1033",
            "ns=3;i=1034",
            "ns=3;i=1035",
            "ns=3;i=1036",
            "ns=3;i=1037",
            "ns=3;i=1038"});
            this.lbNodeId01.Location = new System.Drawing.Point(8, 82);
            this.lbNodeId01.Name = "lbNodeId01";
            this.lbNodeId01.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbNodeId01.Size = new System.Drawing.Size(139, 400);
            this.lbNodeId01.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "NodeId:";
            // 
            // lbResult01
            // 
            this.lbResult01.BackColor = System.Drawing.SystemColors.Control;
            this.lbResult01.FormattingEnabled = true;
            this.lbResult01.ItemHeight = 12;
            this.lbResult01.Location = new System.Drawing.Point(180, 82);
            this.lbResult01.Name = "lbResult01";
            this.lbResult01.Size = new System.Drawing.Size(223, 400);
            this.lbResult01.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(178, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "Result:";
            // 
            // MinimalClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(877, 658);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbErrorMessage);
            this.Controls.Add(this.browseTreeCtrl1);
            this.Controls.Add(this.cbEndpoint);
            this.Controls.Add(this.btnConnect);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MinimalClient";
            this.Text = "MinimalClient";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.minimalClient_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cbEndpoint;
        private Opc.Ua.Sample.Controls.BrowseTreeCtrl browseTreeCtrl1;
        private System.Windows.Forms.Button btn01;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbNodeId01;
        private System.Windows.Forms.TextBox tbResult01;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbErrorMessage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn02;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lbNodeId01;
        private System.Windows.Forms.ListBox lbResult01;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

