// Copyright (C) 2016 to the present, Crestron Electronics, Inc.
// All rights reserved.
// No part of this software may be reproduced in any form, machine
// or natural, without the express written consent of Crestron Electronics.

namespace TnClient
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
            if (br != null)
            {
                br.Close();
            }
            if (log_file_created)
            {
                logfile.Close();
            }
            if (client != null)
            {
                client.Close();
            }
         }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FileOpen = new System.Windows.Forms.Button();
            this.Path = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.hostname = new System.Windows.Forms.TextBox();
            this.port = new System.Windows.Forms.TextBox();
            this.hostname_label = new System.Windows.Forms.Label();
            this.Port_label = new System.Windows.Forms.Label();
            this.DiagLabel = new System.Windows.Forms.Label();
            this.OpenTCP = new System.Windows.Forms.Button();
            this.LoadStart = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.DevID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SleepyDevice = new System.Windows.Forms.CheckBox();
            this.ProgramDevice = new System.Windows.Forms.Button();
            this.fw_id = new System.Windows.Forms.TextBox();
            this.fw_id_label = new System.Windows.Forms.Label();
            this.fw_version = new System.Windows.Forms.TextBox();
            this.fw_version_label = new System.Windows.Forms.Label();
            this.log_file = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.log = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Identify_Timeout = new System.Windows.Forms.TextBox();
            this.Device_Short_Address_label = new System.Windows.Forms.Label();
            this.Device_Short_Address = new System.Windows.Forms.TextBox();
            this.Identity_Timeout_label = new System.Windows.Forms.Label();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.radioButton8 = new System.Windows.Forms.RadioButton();
            this.radioButton9 = new System.Windows.Forms.RadioButton();
            this.radioButton10 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FileOpen
            // 
            this.FileOpen.Location = new System.Drawing.Point(560, 38);
            this.FileOpen.Name = "FileOpen";
            this.FileOpen.Size = new System.Drawing.Size(78, 41);
            this.FileOpen.TabIndex = 0;
            this.FileOpen.Text = "File Open";
            this.FileOpen.UseVisualStyleBackColor = true;
            this.FileOpen.Click += new System.EventHandler(this.FileOpen_Click);
            // 
            // Path
            // 
            this.Path.AutoSize = true;
            this.Path.Location = new System.Drawing.Point(52, 38);
            this.Path.Name = "Path";
            this.Path.Size = new System.Drawing.Size(29, 13);
            this.Path.TabIndex = 1;
            this.Path.Text = "Path";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // hostname
            // 
            this.hostname.Location = new System.Drawing.Point(52, 145);
            this.hostname.Name = "hostname";
            this.hostname.Size = new System.Drawing.Size(108, 20);
            this.hostname.TabIndex = 2;
            this.hostname.TextChanged += new System.EventHandler(this.hostname_TextChanged);
            // 
            // port
            // 
            this.port.AcceptsReturn = true;
            this.port.Location = new System.Drawing.Point(217, 145);
            this.port.MaxLength = 10;
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(97, 20);
            this.port.TabIndex = 3;
            this.port.TextChanged += new System.EventHandler(this.port_TextChanged);
            // 
            // hostname_label
            // 
            this.hostname_label.AutoSize = true;
            this.hostname_label.Location = new System.Drawing.Point(53, 119);
            this.hostname_label.Name = "hostname_label";
            this.hostname_label.Size = new System.Drawing.Size(123, 13);
            this.hostname_label.TabIndex = 4;
            this.hostname_label.Text = "Host name or IP address";
            // 
            // Port_label
            // 
            this.Port_label.AutoSize = true;
            this.Port_label.Location = new System.Drawing.Point(216, 121);
            this.Port_label.Name = "Port_label";
            this.Port_label.Size = new System.Drawing.Size(26, 13);
            this.Port_label.TabIndex = 5;
            this.Port_label.Text = "Port";
            // 
            // DiagLabel
            // 
            this.DiagLabel.AutoSize = true;
            this.DiagLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DiagLabel.Location = new System.Drawing.Point(67, 354);
            this.DiagLabel.Name = "DiagLabel";
            this.DiagLabel.Size = new System.Drawing.Size(2, 15);
            this.DiagLabel.TabIndex = 6;
            this.DiagLabel.Click += new System.EventHandler(this.DiagLabel_Click);
            // 
            // OpenTCP
            // 
            this.OpenTCP.Location = new System.Drawing.Point(422, 132);
            this.OpenTCP.Name = "OpenTCP";
            this.OpenTCP.Size = new System.Drawing.Size(99, 33);
            this.OpenTCP.TabIndex = 7;
            this.OpenTCP.Text = "TCP connect\r\n";
            this.OpenTCP.UseVisualStyleBackColor = true;
            this.OpenTCP.Click += new System.EventHandler(this.OpenTCP_Click);
            // 
            // LoadStart
            // 
            this.LoadStart.Location = new System.Drawing.Point(582, 135);
            this.LoadStart.Name = "LoadStart";
            this.LoadStart.Size = new System.Drawing.Size(67, 39);
            this.LoadStart.TabIndex = 8;
            this.LoadStart.Text = "Load File";
            this.LoadStart.UseVisualStyleBackColor = true;
            this.LoadStart.Click += new System.EventHandler(this.LoadStart_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(52, 77);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(326, 23);
            this.progressBar1.TabIndex = 9;
            // 
            // DevID
            // 
            this.DevID.Location = new System.Drawing.Point(367, 217);
            this.DevID.Name = "DevID";
            this.DevID.Size = new System.Drawing.Size(77, 20);
            this.DevID.TabIndex = 10;
            this.DevID.TextChanged += new System.EventHandler(this.DevID_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(364, 201);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Device ID";
            // 
            // SleepyDevice
            // 
            this.SleepyDevice.AutoSize = true;
            this.SleepyDevice.Location = new System.Drawing.Point(21, 253);
            this.SleepyDevice.Name = "SleepyDevice";
            this.SleepyDevice.Size = new System.Drawing.Size(192, 17);
            this.SleepyDevice.TabIndex = 12;
            this.SleepyDevice.Text = "SleepyDevice(not yet implemented)";
            this.SleepyDevice.UseVisualStyleBackColor = true;
            this.SleepyDevice.CheckedChanged += new System.EventHandler(this.SleepyDevice_CheckedChanged);
            // 
            // ProgramDevice
            // 
            this.ProgramDevice.Location = new System.Drawing.Point(582, 205);
            this.ProgramDevice.Name = "ProgramDevice";
            this.ProgramDevice.Size = new System.Drawing.Size(75, 43);
            this.ProgramDevice.TabIndex = 13;
            this.ProgramDevice.Text = "Program Device";
            this.ProgramDevice.UseVisualStyleBackColor = true;
            this.ProgramDevice.Click += new System.EventHandler(this.ProgramDevice_Click);
            // 
            // fw_id
            // 
            this.fw_id.Location = new System.Drawing.Point(21, 217);
            this.fw_id.MaxLength = 24;
            this.fw_id.Name = "fw_id";
            this.fw_id.Size = new System.Drawing.Size(170, 20);
            this.fw_id.TabIndex = 14;
            this.fw_id.TextChanged += new System.EventHandler(this.fw_id_TextChanged);
            // 
            // fw_id_label
            // 
            this.fw_id_label.AutoSize = true;
            this.fw_id_label.Location = new System.Drawing.Point(18, 194);
            this.fw_id_label.Name = "fw_id_label";
            this.fw_id_label.Size = new System.Drawing.Size(63, 13);
            this.fw_id_label.TabIndex = 15;
            this.fw_id_label.Text = "Firmware ID";
            // 
            // fw_version
            // 
            this.fw_version.Location = new System.Drawing.Point(197, 217);
            this.fw_version.MaxLength = 20;
            this.fw_version.Name = "fw_version";
            this.fw_version.Size = new System.Drawing.Size(151, 20);
            this.fw_version.TabIndex = 16;
            this.fw_version.TextChanged += new System.EventHandler(this.fw_version_TextChanged);
            // 
            // fw_version_label
            // 
            this.fw_version_label.AutoSize = true;
            this.fw_version_label.Location = new System.Drawing.Point(194, 194);
            this.fw_version_label.Name = "fw_version_label";
            this.fw_version_label.Size = new System.Drawing.Size(86, 13);
            this.fw_version_label.TabIndex = 17;
            this.fw_version_label.Text = "Firmware version";
            // 
            // log_file
            // 
            this.log_file.Location = new System.Drawing.Point(280, 287);
            this.log_file.Name = "log_file";
            this.log_file.Size = new System.Drawing.Size(267, 20);
            this.log_file.TabIndex = 18;
            this.log_file.TextChanged += new System.EventHandler(this.log_file_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(277, 271);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "log file path";
            // 
            // log
            // 
            this.log.Location = new System.Drawing.Point(587, 286);
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(70, 21);
            this.log.TabIndex = 20;
            this.log.Text = "log";
            this.log.UseVisualStyleBackColor = false;
            this.log.Click += new System.EventHandler(this.log_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Identify_Timeout);
            this.groupBox1.Controls.Add(this.Device_Short_Address_label);
            this.groupBox1.Controls.Add(this.Device_Short_Address);
            this.groupBox1.Controls.Add(this.Identity_Timeout_label);
            this.groupBox1.Controls.Add(this.radioButton6);
            this.groupBox1.Controls.Add(this.radioButton7);
            this.groupBox1.Controls.Add(this.radioButton8);
            this.groupBox1.Controls.Add(this.radioButton9);
            this.groupBox1.Controls.Add(this.radioButton10);
            this.groupBox1.Controls.Add(this.radioButton4);
            this.groupBox1.Controls.Add(this.radioButton5);
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(280, 321);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(292, 259);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "LBI Commands Simulation (click to send)";
            // 
            // Identify_Timeout
            // 
            this.Identify_Timeout.Location = new System.Drawing.Point(148, 207);
            this.Identify_Timeout.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Identify_Timeout.Name = "Identify_Timeout";
            this.Identify_Timeout.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Identify_Timeout.Size = new System.Drawing.Size(108, 20);
            this.Identify_Timeout.TabIndex = 15;
            this.Identify_Timeout.Text = "300";
            this.Identify_Timeout.TextChanged += new System.EventHandler(this.Identify_Timeout_TextChanged);
            // 
            // Device_Short_Address_label
            // 
            this.Device_Short_Address_label.AutoSize = true;
            this.Device_Short_Address_label.Location = new System.Drawing.Point(142, 117);
            this.Device_Short_Address_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Device_Short_Address_label.Name = "Device_Short_Address_label";
            this.Device_Short_Address_label.Size = new System.Drawing.Size(113, 26);
            this.Device_Short_Address_label.TabIndex = 14;
            this.Device_Short_Address_label.Text = "Device Short Address \r\n( 0 - 65535)";
            // 
            // Device_Short_Address
            // 
            this.Device_Short_Address.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.Device_Short_Address.Location = new System.Drawing.Point(145, 148);
            this.Device_Short_Address.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Device_Short_Address.Name = "Device_Short_Address";
            this.Device_Short_Address.Size = new System.Drawing.Size(110, 20);
            this.Device_Short_Address.TabIndex = 13;
            this.Device_Short_Address.Text = "0";
            this.Device_Short_Address.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Device_Short_Address.TextChanged += new System.EventHandler(this.Device_Short_Address_TextChanged);
            // 
            // Identity_Timeout_label
            // 
            this.Identity_Timeout_label.AutoSize = true;
            this.Identity_Timeout_label.Location = new System.Drawing.Point(146, 175);
            this.Identity_Timeout_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Identity_Timeout_label.Name = "Identity_Timeout_label";
            this.Identity_Timeout_label.Size = new System.Drawing.Size(82, 26);
            this.Identity_Timeout_label.TabIndex = 12;
            this.Identity_Timeout_label.Text = "Identity Timeout\r\n(0-65535 Sec)";
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new System.Drawing.Point(5, 230);
            this.radioButton6.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(90, 17);
            this.radioButton6.TabIndex = 9;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "Join Network ";
            this.radioButton6.UseVisualStyleBackColor = true;
            this.radioButton6.Click += new System.EventHandler(this.JoinNetwork_CheckedChanged);
            // 
            // radioButton7
            // 
            this.radioButton7.AutoSize = true;
            this.radioButton7.Location = new System.Drawing.Point(5, 207);
            this.radioButton7.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(97, 17);
            this.radioButton7.TabIndex = 8;
            this.radioButton7.TabStop = true;
            this.radioButton7.Text = "Form Network  ";
            this.radioButton7.UseVisualStyleBackColor = true;
            this.radioButton7.Click += new System.EventHandler(this.FormNetwork_CheckedChanged);
            // 
            // radioButton8
            // 
            this.radioButton8.AutoSize = true;
            this.radioButton8.Location = new System.Drawing.Point(5, 185);
            this.radioButton8.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton8.Name = "radioButton8";
            this.radioButton8.Size = new System.Drawing.Size(62, 17);
            this.radioButton8.TabIndex = 7;
            this.radioButton8.TabStop = true;
            this.radioButton8.Text = "Is Alive ";
            this.radioButton8.UseVisualStyleBackColor = true;
            this.radioButton8.Click += new System.EventHandler(this.IsAlive_CheckedChanged);
            // 
            // radioButton9
            // 
            this.radioButton9.AutoSize = true;
            this.radioButton9.Location = new System.Drawing.Point(5, 162);
            this.radioButton9.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton9.Name = "radioButton9";
            this.radioButton9.Size = new System.Drawing.Size(83, 17);
            this.radioButton9.TabIndex = 6;
            this.radioButton9.TabStop = true;
            this.radioButton9.Text = "Device Info ";
            this.radioButton9.UseVisualStyleBackColor = true;
            this.radioButton9.Click += new System.EventHandler(this.DeviceInfo_CheckedChanged);
            // 
            // radioButton10
            // 
            this.radioButton10.AutoSize = true;
            this.radioButton10.Location = new System.Drawing.Point(5, 140);
            this.radioButton10.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton10.Name = "radioButton10";
            this.radioButton10.Size = new System.Drawing.Size(121, 17);
            this.radioButton10.TabIndex = 5;
            this.radioButton10.TabStop = true;
            this.radioButton10.Text = "Device Stop Identify";
            this.radioButton10.UseVisualStyleBackColor = true;
            this.radioButton10.Click += new System.EventHandler(this.DeviceStopIdentity_CheckedChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(5, 117);
            this.radioButton4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(99, 17);
            this.radioButton4.TabIndex = 4;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "Device Identify ";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.Click += new System.EventHandler(this.DeviceIdentity_CheckedChanged);
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(5, 94);
            this.radioButton5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(112, 17);
            this.radioButton5.TabIndex = 3;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "Device Discovery ";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.Click += new System.EventHandler(this.DeviceDiscovery_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(5, 72);
            this.radioButton3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(85, 17);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Disable Join ";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.Click += new System.EventHandler(this.DisableJoin_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(5, 50);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(79, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Permit Join ";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.Click += new System.EventHandler(this.PermitJoin_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(5, 27);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(70, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Get BCD ";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            this.radioButton1.Click += new System.EventHandler(this.GetBCD_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 590);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.log);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.log_file);
            this.Controls.Add(this.fw_version_label);
            this.Controls.Add(this.fw_version);
            this.Controls.Add(this.fw_id_label);
            this.Controls.Add(this.fw_id);
            this.Controls.Add(this.ProgramDevice);
            this.Controls.Add(this.SleepyDevice);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DevID);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.LoadStart);
            this.Controls.Add(this.OpenTCP);
            this.Controls.Add(this.DiagLabel);
            this.Controls.Add(this.Port_label);
            this.Controls.Add(this.hostname_label);
            this.Controls.Add(this.port);
            this.Controls.Add(this.hostname);
            this.Controls.Add(this.Path);
            this.Controls.Add(this.FileOpen);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FileOpen;
        private System.Windows.Forms.Label Path;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox hostname;
        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.Label hostname_label;
        private System.Windows.Forms.Label Port_label;
        private System.Windows.Forms.Button OpenTCP;
        private System.Windows.Forms.Button LoadStart;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox DevID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox SleepyDevice;
        private System.Windows.Forms.Button ProgramDevice;
        private System.Windows.Forms.TextBox fw_id;
        private System.Windows.Forms.Label fw_id_label;
        private System.Windows.Forms.TextBox fw_version;
        private System.Windows.Forms.Label fw_version_label;
        private System.Windows.Forms.TextBox log_file;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button log;
        public System.Windows.Forms.Label DiagLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox Identify_Timeout;
        private System.Windows.Forms.Label Device_Short_Address_label;
        private System.Windows.Forms.TextBox Device_Short_Address;
        private System.Windows.Forms.Label Identity_Timeout_label;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.RadioButton radioButton8;
        private System.Windows.Forms.RadioButton radioButton9;
        private System.Windows.Forms.RadioButton radioButton10;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}

