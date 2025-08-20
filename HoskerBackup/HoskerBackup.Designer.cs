
namespace HoskerBackup
{
	partial class HoskerBackup
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
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.grpInclude = new System.Windows.Forms.GroupBox();
			this.btnDeleteIncludeFolder = new System.Windows.Forms.Button();
			this.listIncludeFolders = new System.Windows.Forms.ListBox();
			this.btnIncludeFolders = new System.Windows.Forms.Button();
			this.listExcludeFolders = new System.Windows.Forms.ListBox();
			this.btnExcludeFolders = new System.Windows.Forms.Button();
			this.groupExcludeFolders = new System.Windows.Forms.GroupBox();
			this.btnDeleteExcludeFolder = new System.Windows.Forms.Button();
			this.listExcludePatterns = new System.Windows.Forms.ListBox();
			this.btnAddExcludePattern = new System.Windows.Forms.Button();
			this.grpExcludePatterns = new System.Windows.Forms.GroupBox();
			this.btnDeleteExcludePattern = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.txtDestination = new System.Windows.Forms.TextBox();
			this.btnBackupTo = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.txtHour = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtMinute = new System.Windows.Forms.TextBox();
			this.btnLog = new System.Windows.Forms.Button();
			this.tmrSchedule = new System.Windows.Forms.Timer(this.components);
			this.lblProgress = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtKeepDeletedFilesFor = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.grpInclude.SuspendLayout();
			this.groupExcludeFolders.SuspendLayout();
			this.grpExcludePatterns.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpInclude
			// 
			this.grpInclude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.grpInclude.Controls.Add(this.btnDeleteIncludeFolder);
			this.grpInclude.Controls.Add(this.listIncludeFolders);
			this.grpInclude.Controls.Add(this.btnIncludeFolders);
			this.grpInclude.Location = new System.Drawing.Point(4, 5);
			this.grpInclude.Margin = new System.Windows.Forms.Padding(1);
			this.grpInclude.Name = "grpInclude";
			this.grpInclude.Padding = new System.Windows.Forms.Padding(1);
			this.grpInclude.Size = new System.Drawing.Size(1023, 197);
			this.grpInclude.TabIndex = 1;
			this.grpInclude.TabStop = false;
			this.grpInclude.Text = "Include Folders";
			// 
			// btnDeleteIncludeFolder
			// 
			this.btnDeleteIncludeFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDeleteIncludeFolder.Location = new System.Drawing.Point(895, 163);
			this.btnDeleteIncludeFolder.Margin = new System.Windows.Forms.Padding(1);
			this.btnDeleteIncludeFolder.Name = "btnDeleteIncludeFolder";
			this.btnDeleteIncludeFolder.Size = new System.Drawing.Size(121, 23);
			this.btnDeleteIncludeFolder.TabIndex = 3;
			this.btnDeleteIncludeFolder.Text = "Delete";
			this.btnDeleteIncludeFolder.UseVisualStyleBackColor = true;
			this.btnDeleteIncludeFolder.Click += new System.EventHandler(this.btnDeleteIncludeFolder_Click);
			// 
			// listIncludeFolders
			// 
			this.listIncludeFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			| System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.listIncludeFolders.FormattingEnabled = true;
			this.listIncludeFolders.Location = new System.Drawing.Point(6, 19);
			this.listIncludeFolders.Margin = new System.Windows.Forms.Padding(1);
			this.listIncludeFolders.Name = "listIncludeFolders";
			this.listIncludeFolders.Size = new System.Drawing.Size(1010, 134);
			this.listIncludeFolders.TabIndex = 2;
			// 
			// btnIncludeFolders
			// 
			this.btnIncludeFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnIncludeFolders.Location = new System.Drawing.Point(769, 163);
			this.btnIncludeFolders.Margin = new System.Windows.Forms.Padding(1);
			this.btnIncludeFolders.Name = "btnIncludeFolders";
			this.btnIncludeFolders.Size = new System.Drawing.Size(121, 23);
			this.btnIncludeFolders.TabIndex = 1;
			this.btnIncludeFolders.Text = "Select Folder";
			this.btnIncludeFolders.UseVisualStyleBackColor = true;
			this.btnIncludeFolders.Click += new System.EventHandler(this.btnIncludeFolders_Click);
			// 
			// listExcludeFolders
			// 
			this.listExcludeFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			| System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.listExcludeFolders.FormattingEnabled = true;
			this.listExcludeFolders.Location = new System.Drawing.Point(6, 19);
			this.listExcludeFolders.Margin = new System.Windows.Forms.Padding(1);
			this.listExcludeFolders.Name = "listExcludeFolders";
			this.listExcludeFolders.Size = new System.Drawing.Size(1013, 121);
			this.listExcludeFolders.TabIndex = 2;
			// 
			// btnExcludeFolders
			// 
			this.btnExcludeFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExcludeFolders.Location = new System.Drawing.Point(769, 163);
			this.btnExcludeFolders.Margin = new System.Windows.Forms.Padding(1);
			this.btnExcludeFolders.Name = "btnExcludeFolders";
			this.btnExcludeFolders.Size = new System.Drawing.Size(121, 23);
			this.btnExcludeFolders.TabIndex = 1;
			this.btnExcludeFolders.Text = "Select Folder";
			this.btnExcludeFolders.UseVisualStyleBackColor = true;
			this.btnExcludeFolders.Click += new System.EventHandler(this.btnExcludeFolders_Click);
			// 
			// groupExcludeFolders
			// 
			this.groupExcludeFolders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.groupExcludeFolders.Controls.Add(this.btnDeleteExcludeFolder);
			this.groupExcludeFolders.Controls.Add(this.listExcludeFolders);
			this.groupExcludeFolders.Controls.Add(this.btnExcludeFolders);
			this.groupExcludeFolders.Location = new System.Drawing.Point(4, 209);
			this.groupExcludeFolders.Margin = new System.Windows.Forms.Padding(1);
			this.groupExcludeFolders.Name = "groupExcludeFolders";
			this.groupExcludeFolders.Padding = new System.Windows.Forms.Padding(1);
			this.groupExcludeFolders.Size = new System.Drawing.Size(1023, 191);
			this.groupExcludeFolders.TabIndex = 2;
			this.groupExcludeFolders.TabStop = false;
			this.groupExcludeFolders.Text = "Exclude Folders";
			// 
			// btnDeleteExcludeFolder
			// 
			this.btnDeleteExcludeFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDeleteExcludeFolder.Location = new System.Drawing.Point(895, 163);
			this.btnDeleteExcludeFolder.Margin = new System.Windows.Forms.Padding(1);
			this.btnDeleteExcludeFolder.Name = "btnDeleteExcludeFolder";
			this.btnDeleteExcludeFolder.Size = new System.Drawing.Size(121, 23);
			this.btnDeleteExcludeFolder.TabIndex = 4;
			this.btnDeleteExcludeFolder.Text = "Delete";
			this.btnDeleteExcludeFolder.UseVisualStyleBackColor = true;
			this.btnDeleteExcludeFolder.Click += new System.EventHandler(this.btnDeleteExcludeFolder_Click);
			// 
			// listExcludePatterns
			// 
			this.listExcludePatterns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			| System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.listExcludePatterns.FormattingEnabled = true;
			this.listExcludePatterns.Location = new System.Drawing.Point(6, 19);
			this.listExcludePatterns.Margin = new System.Windows.Forms.Padding(1);
			this.listExcludePatterns.Name = "listExcludePatterns";
			this.listExcludePatterns.Size = new System.Drawing.Size(1013, 134);
			this.listExcludePatterns.TabIndex = 2;
			// 
			// btnAddExcludePattern
			// 
			this.btnAddExcludePattern.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddExcludePattern.Location = new System.Drawing.Point(769, 163);
			this.btnAddExcludePattern.Margin = new System.Windows.Forms.Padding(1);
			this.btnAddExcludePattern.Name = "btnAddExcludePattern";
			this.btnAddExcludePattern.Size = new System.Drawing.Size(121, 23);
			this.btnAddExcludePattern.TabIndex = 1;
			this.btnAddExcludePattern.Text = "Add";
			this.btnAddExcludePattern.UseVisualStyleBackColor = true;
			this.btnAddExcludePattern.Click += new System.EventHandler(this.btnAddExcludePattern_Click);
			// 
			// grpExcludePatterns
			// 
			this.grpExcludePatterns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.grpExcludePatterns.Controls.Add(this.btnDeleteExcludePattern);
			this.grpExcludePatterns.Controls.Add(this.listExcludePatterns);
			this.grpExcludePatterns.Controls.Add(this.btnAddExcludePattern);
			this.grpExcludePatterns.Location = new System.Drawing.Point(4, 413);
			this.grpExcludePatterns.Margin = new System.Windows.Forms.Padding(1);
			this.grpExcludePatterns.Name = "grpExcludePatterns";
			this.grpExcludePatterns.Padding = new System.Windows.Forms.Padding(1);
			this.grpExcludePatterns.Size = new System.Drawing.Size(1023, 197);
			this.grpExcludePatterns.TabIndex = 3;
			this.grpExcludePatterns.TabStop = false;
			this.grpExcludePatterns.Text = "Exclude Patterns";
			// 
			// btnDeleteExcludePattern
			// 
			this.btnDeleteExcludePattern.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDeleteExcludePattern.Location = new System.Drawing.Point(895, 163);
			this.btnDeleteExcludePattern.Margin = new System.Windows.Forms.Padding(1);
			this.btnDeleteExcludePattern.Name = "btnDeleteExcludePattern";
			this.btnDeleteExcludePattern.Size = new System.Drawing.Size(121, 23);
			this.btnDeleteExcludePattern.TabIndex = 5;
			this.btnDeleteExcludePattern.Text = "Delete";
			this.btnDeleteExcludePattern.UseVisualStyleBackColor = true;
			this.btnDeleteExcludePattern.Click += new System.EventHandler(this.btnDeleteExcludePattern_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Location = new System.Drawing.Point(773, 654);
			this.btnSave.Margin = new System.Windows.Forms.Padding(1);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(121, 23);
			this.btnSave.TabIndex = 4;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(899, 654);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(1);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(121, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// txtDestination
			// 
			this.txtDestination.Location = new System.Drawing.Point(12, 624);
			this.txtDestination.Name = "txtDestination";
			this.txtDestination.Size = new System.Drawing.Size(224, 20);
			this.txtDestination.TabIndex = 6;
			// 
			// btnBackupTo
			// 
			this.btnBackupTo.Location = new System.Drawing.Point(240, 622);
			this.btnBackupTo.Margin = new System.Windows.Forms.Padding(1);
			this.btnBackupTo.Name = "btnBackupTo";
			this.btnBackupTo.Size = new System.Drawing.Size(121, 23);
			this.btnBackupTo.TabIndex = 7;
			this.btnBackupTo.Text = "Backup To";
			this.btnBackupTo.UseVisualStyleBackColor = true;
			this.btnBackupTo.Click += new System.EventHandler(this.btnBackupTo_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(365, 627);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(16, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "at";
			// 
			// txtHour
			// 
			this.txtHour.Location = new System.Drawing.Point(387, 624);
			this.txtHour.Name = "txtHour";
			this.txtHour.Size = new System.Drawing.Size(37, 20);
			this.txtHour.TabIndex = 9;
			this.txtHour.Text = "00";
			this.txtHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(430, 627);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(10, 13);
			this.label2.TabIndex = 10;
			this.label2.Text = ":";
			// 
			// txtMinute
			// 
			this.txtMinute.Location = new System.Drawing.Point(446, 624);
			this.txtMinute.Name = "txtMinute";
			this.txtMinute.Size = new System.Drawing.Size(37, 20);
			this.txtMinute.TabIndex = 11;
			this.txtMinute.Text = "00";
			// 
			// btnLog
			// 
			this.btnLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnLog.Location = new System.Drawing.Point(10, 654);
			this.btnLog.Margin = new System.Windows.Forms.Padding(1);
			this.btnLog.Name = "btnLog";
			this.btnLog.Size = new System.Drawing.Size(121, 23);
			this.btnLog.TabIndex = 12;
			this.btnLog.Text = "View Log";
			this.btnLog.UseVisualStyleBackColor = true;
			this.btnLog.Click += new System.EventHandler(this.OnViewLog);
			// 
			// tmrSchedule
			// 
			this.tmrSchedule.Interval = 1000;
			this.tmrSchedule.Tick += new System.EventHandler(this.tmrSchedule_Tick);
			// 
			// lblProgress
			// 
			this.lblProgress.Location = new System.Drawing.Point(135, 659);
			this.lblProgress.Name = "lblProgress";
			this.lblProgress.Size = new System.Drawing.Size(634, 23);
			this.lblProgress.TabIndex = 13;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(546, 627);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(106, 13);
			this.label3.TabIndex = 14;
			this.label3.Text = "Keep deleted files for";
			// 
			// txtKeepDeletedFilesFor
			// 
			this.txtKeepDeletedFilesFor.Location = new System.Drawing.Point(658, 624);
			this.txtKeepDeletedFilesFor.Name = "txtKeepDeletedFilesFor";
			this.txtKeepDeletedFilesFor.Size = new System.Drawing.Size(33, 20);
			this.txtKeepDeletedFilesFor.TabIndex = 15;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(697, 627);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(29, 13);
			this.label4.TabIndex = 16;
			this.label4.Text = "days";
			// 
			// HoskerBackup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1030, 687);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtKeepDeletedFilesFor);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lblProgress);
			this.Controls.Add(this.btnLog);
			this.Controls.Add(this.txtMinute);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtHour);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnBackupTo);
			this.Controls.Add(this.txtDestination);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.grpExcludePatterns);
			this.Controls.Add(this.groupExcludeFolders);
			this.Controls.Add(this.grpInclude);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "HoskerBackup";
			this.ShowInTaskbar = false;
			this.Text = "HoskerBackup";
			this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
			this.grpInclude.ResumeLayout(false);
			this.groupExcludeFolders.ResumeLayout(false);
			this.grpExcludePatterns.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox grpInclude;
		private System.Windows.Forms.ListBox listIncludeFolders;
		private System.Windows.Forms.Button btnIncludeFolders;
		private System.Windows.Forms.ListBox listExcludeFolders;
		private System.Windows.Forms.Button btnExcludeFolders;
		private System.Windows.Forms.GroupBox groupExcludeFolders;
		private System.Windows.Forms.ListBox listExcludePatterns;
		private System.Windows.Forms.Button btnAddExcludePattern;
		private System.Windows.Forms.GroupBox grpExcludePatterns;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnDeleteIncludeFolder;
		private System.Windows.Forms.Button btnDeleteExcludeFolder;
		private System.Windows.Forms.Button btnDeleteExcludePattern;
		private System.Windows.Forms.TextBox txtDestination;
		private System.Windows.Forms.Button btnBackupTo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtHour;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtMinute;
		private System.Windows.Forms.Button btnLog;
		private System.Windows.Forms.Timer tmrSchedule;
		private System.Windows.Forms.Label lblProgress;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtKeepDeletedFilesFor;
		private System.Windows.Forms.Label label4;
	}
}

