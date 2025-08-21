using HoskerBackup.Core;
using System.Windows.Forms;

namespace HoskerBackup
{
	public partial class LogForm : Form
	{
		public LogForm(Backup backup)
		{
			InitializeComponent();
			txtLog.Text = backup.GetLog();
		}
	}
}
