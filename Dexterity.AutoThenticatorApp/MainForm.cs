using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dexterity.AutoThenticatorApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAutoLogin_Click(object sender, EventArgs e)
        {
            LogInAutomatically();
        }

        private async void LogInAutomatically()
        {
            LogActivity("Attempting to contact Dexterity.Login");

            var discoveryClient = new DiscoveryClient("https://localhost:44387/login");

            var doc = await discoveryClient.GetAsync();
            var tokenEndpoint = doc.TokenEndpoint;
            var keys = doc.KeySet.Keys;

            var client = new TokenClient(doc.TokenEndpoint, "Dexterity.AutoThenticator", "autothen");

            var response = await client.RequestResourceOwnerPasswordAsync("snake", "pass");
            var token = response.IdentityToken;

            LogActivity(".END");
        }

        private void LogActivity(string activity)
        {
            txtOutput.AppendText(activity);
            txtOutput.AppendText(Environment.NewLine);
        }
    }
}
