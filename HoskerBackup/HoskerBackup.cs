using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HoskerBackup
{
	public partial class HoskerBackup : Form
	{
		public HoskerBackup()
		{
			InitializeComponent();
			LoadConfig();

			// Create the tray icon
			trayIcon = new NotifyIcon();
			trayIcon.Text = "Hosker Backup";

			// Create a context menu for the tray icon
			trayMenu = new ContextMenu();
			trayMenu.MenuItems.Add("Open", OnOpen);
			trayMenu.MenuItems.Add("View Log", OnViewLog);
			trayMenu.MenuItems.Add("Exit", OnExit);

			// Assign the context menu to the tray icon
			trayIcon.ContextMenu = trayMenu;

			SetIcon(Config.IsValid);

			// Handle the FormClosing event to hide the form instead of closing
			this.FormClosing += HoskerBackup_FormClosing;

			StartSchedule();

			if (!Config.IsValid)
			{
				ShowForm();
			}
		}

		void SetIcon(bool status, string statusMessage = "")
		{
			if (status != currentStatus)
			{
				trayIcon.Icon = status ? Properties.Resources.Green : Properties.Resources.Red;
				currentStatus = status;
			}

			if (statusMessage != trayIcon.Text)
			{
				trayIcon.Text = statusMessage.Substring(0, Math.Min(63, statusMessage.Length));
			}

			if (trayIcon.Text == "")
			{
				trayIcon.Text = "Hosker Backup";
			}
		}
		bool currentStatus;

		private void HoskerBackup_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = true; // Cancel the form closing event
				this.ShowInTaskbar = false;
				this.Hide(); // Hide the form instead of closing
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			trayIcon.Visible = true; // Show the tray icon
		}

		private void OnOpen(object sender, EventArgs e)
		{
			ShowForm();
		}

		void ShowForm()
		{
			this.WindowState = FormWindowState.Normal;
			this.ShowInTaskbar = true;
			this.Show(); // Show the form when "Open" is clicked
			LoadConfig();
		}

		private void OnExit(object sender, EventArgs e)
		{
			Backup.Cancel();
			trayIcon.Visible = false; // Hide the tray icon before exiting
			Application.Exit(); // Terminate the application
		}

		void LoadConfig()
		{
			PopulateListBox(listIncludeFolders, Config.IncludeFolders);
			PopulateListBox(listExcludeFolders, Config.ExcludeFolders);
			PopulateListBox(listExcludePatterns, Config.ExcludePatterns);

			txtDestination.Text = Config.Destination;
			txtHour.Text = Config.ScheduleHour.ToString();
			txtMinute.Text = Config.ScheduleMinute.ToString();
			txtKeepDeletedFilesFor.Text = Config.KeepDeletedFilesFor.ToString();
		}

		void PopulateListBox(ListBox listBox, IEnumerable<string> items)
		{
			listBox.Items.Clear();

			foreach (string item in items)
			{
				listBox.Items.Add(item);
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			Config.IncludeFolders = Helper.ConvertToSet(listIncludeFolders.Items.Cast<string>().Where(i => i != ""));
			Config.ExcludeFolders = Helper.ConvertToSet(listExcludeFolders.Items.Cast<string>().Where(i => i != ""));
			Config.ExcludePatterns = Helper.ConvertToSet(listExcludePatterns.Items.Cast<string>().Where(i => i != ""));

			Config.Destination = txtDestination.Text;

			if (int.TryParse(txtHour.Text, out _))
			{
				Config.ScheduleHour = int.Parse(txtHour.Text);
			}
			
			if (int.TryParse(txtMinute.Text, out _))
			{
				Config.ScheduleMinute = int.Parse(txtMinute.Text);
			}

			if (int.TryParse(txtKeepDeletedFilesFor.Text, out _))
			{
				Config.KeepDeletedFilesFor = int.Parse(txtKeepDeletedFilesFor.Text);
			}

			Config.SaveConfig();

			StartSchedule();
		}

		void StartSchedule()
		{
			if (Config.IsValid)
			{
				tmrSchedule.Enabled = true;
				SetIcon(true);
			}
			else
			{
				tmrSchedule.Enabled = false;
				SetIcon(false, "Invalid Config");
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			config = null;
			this.Hide();
		}

		Config Config => config ?? (config = new Config());
		Config config;

		private void btnIncludeFolders_Click(object sender, EventArgs e)
		{
			AddFolder(listIncludeFolders);
		}

		private void btnExcludeFolders_Click(object sender, EventArgs e)
		{
			AddFolder(listExcludeFolders);
		}

		public void AddFolder(ListBox listBox)
		{
			using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
			{
				DialogResult result = folderBrowserDialog.ShowDialog();

				if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
				{
					listBox.Items.Add(folderBrowserDialog.SelectedPath);
				}
			}
		}

		private void btnDeleteIncludeFolder_Click(object sender, EventArgs e)
		{
			RemoveSelected(listIncludeFolders);
		}

		private void btnDeleteExcludeFolder_Click(object sender, EventArgs e)
		{
			RemoveSelected(listExcludeFolders);
		}

		private void btnDeleteExcludePattern_Click(object sender, EventArgs e)
		{
			RemoveSelected(listExcludePatterns);
		}

		void RemoveSelected(ListBox listBox)
		{
			foreach (int selectedIndex in listBox.SelectedIndices.Cast<int>().OrderByDescending(i => i))
			{
				listBox.Items.RemoveAt(selectedIndex);
			}
		}

		private void OnViewLog(object sender, EventArgs e)
		{
			new LogForm(Backup).ShowDialog();
		}

		private void btnAddExcludePattern_Click(object sender, EventArgs e)
		{
			var pattern = Interaction.InputBox("Enter exclude pattern");
			if (pattern != "")
			{
				listExcludePatterns.Items.Add(pattern);
			}
		}

		private void btnBackupTo_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
			{
				DialogResult result = folderBrowserDialog.ShowDialog();

				if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
				{
					txtDestination.Text = folderBrowserDialog.SelectedPath;
				}
			}
		}

		private void tmrSchedule_Tick(object sender, EventArgs e)
		{
			SetIcon(Backup.Status, Backup.StatusMessage);
			
			if (Backup.IsRunning)
			{
				lblProgress.Text = Backup.ProgressMessage;
				return;
			}
			else
			{
				lblProgress.Text = "";
			}

			if (!Backup.HasRunToday
				&& DateTime.Now.TimeOfDay >= new TimeSpan(Config.ScheduleHour, Config.ScheduleMinute, 0))
			{
				Task.Run(() => Backup.RunBackup());				
			}
		}

		NotifyIcon trayIcon;
		ContextMenu trayMenu;

		Backup Backup => backup ?? (backup = new Backup());
		Backup backup;
	}
}
