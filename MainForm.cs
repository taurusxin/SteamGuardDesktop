using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteamGuardDesktop
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private string code = string.Empty;
        private SharedSecret secret = new SharedSecret();

        private void timer1_Tick(object sender, EventArgs e)
        {
            long timestamp = Utils.GetSystemUnixTime();
            int timeLeft = 30 - (int)(Math.Floor((decimal)timestamp) % 30);
            this.progressBar1.Value = timeLeft;
            if (timeLeft < 5)
            {
                progressBar1.setColor(Brushes.Red);
            }else if (timeLeft < 10)
            {
                progressBar1.setColor(Brushes.Orange);
            }
            else if (timeLeft == 30)
            {
                RefreshCode();
                progressBar1.setColor(Brushes.LimeGreen);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Initialize(sender, e);
        }

        private void Initialize(object sender, EventArgs e)
        {
            // Check the file

            if (!File.Exists("config.json"))
            {
                linkLabel1.Text = "Config first!";
                linkLabel1.Enabled = false;
                timer1.Enabled = false;
                Utils.FileWrite(Application.StartupPath + "\\config.json", Json.stringify(new SharedSecret()));
                return;
            }

            // Check whether the code is valid

            secret = Json.parse<SharedSecret>(Utils.FileRead(Application.StartupPath + "\\config.json"));
            string code = string.Empty;
            long timestamp = Utils.GetSystemUnixTime();
            try
            {
                code = SteamAuth.GenerateSteamGuardCodeForTime(secret.SharedSecretString, timestamp);
            }
            catch (Exception)
            {
                code = "Secret invalid!";
                this.linkLabel1.Text = code;
                timer1.Enabled = false;
                linkLabel1.Enabled = false;
                progressBar1.Value = 0;
                return;
            }

            // All the data check passed
            RefreshCode();
            timer1_Tick(sender, e);
        }

        private void RefreshCode()
        {
            long timestamp = Utils.GetSystemUnixTime();
            code = SteamAuth.GenerateSteamGuardCodeForTime(secret.SharedSecretString, timestamp);
            this.linkLabel1.Text = code;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string tmp = linkLabel1.Text;
            Clipboard.SetText(tmp);
            linkLabel1.Text = "Copied!";
            Delay(1000);
            linkLabel1.Text = tmp;
        }

        private static void Delay(int milliSecond)
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)//毫秒
            {
                Application.DoEvents();//可执行某无聊的操作
            }
        }
    }
}
