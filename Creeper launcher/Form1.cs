using CmlLib.Core;
using CmlLib.Core.Auth;
using CmlLib.Core.ProcessBuilder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Creeper_launcher
{
    public partial class Form1 : Form
    {
        private string nome;
        private string skinpath;
        public Form1()
        {
            InitializeComponent();
            Versoes();
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            skinpath = Path.Combine(appdata, ".minecraft");


            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }


        private async void Versoes()
        {
            var launcher = new MinecraftLauncher();
            var versoez = await launcher.GetAllVersionsAsync();


            foreach (var item in versoez)
            {
                guna2ComboBox1.Items.Add(item.Name);
            }
        }



        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            nome = guna2TextBox1.Text;
        }

        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            string caracterquenaopode = " ";
            
            if (string.IsNullOrEmpty(nome))
            {
                MessageBox.Show("Coloque um nome, por favor");
                return;
            }


            var caminho = MinecraftPath.GetOSDefaultPath(); // instala o minecraft no caminho padrão
            MinecraftLauncher launcher = new MinecraftLauncher(caminho);

            string version = null;

            
            if (guna2ComboBox1.SelectedIndex != -1)
            {
                version = guna2ComboBox1.SelectedItem.ToString();
                await launcher.InstallAsync(version); // instala a versão escolhida
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma versão.");
                return;
            }
            if (guna2TextBox1.Text.Contains(caracterquenaopode))
            {
                MessageBox.Show("Por favor não deixe espaços em branco no seu nickname");
                return;
            }
            
            var process = await launcher.BuildProcessAsync(version, new MLaunchOption
            {
                Session = MSession.CreateOfflineSession(nome),
                GameLauncherName = "CreeperLauncher",
            });

            process.Start();

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", skinpath);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
