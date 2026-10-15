using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace lab3
{
    public partial class Form1 : Form
    {
        private List<string> Blockedw = new List<string>
        {
            "facebook",
            "youtube",
            "instagram",
            "twitter",
            "reddit",
            "tiktok",
        };
        public Form1()
        {
            InitializeComponent();
            Trace.Listeners.Add(new TextWriterTraceListener("events.txt"));
            Trace.AutoFlush = true;
            foreach (string keyword in Blockedw)
            {
                toolStripComboBox1.Items.Add(keyword);
            }
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //home
            _ = LogEventAsync("User clicked Home");
            webBrowser1.GoHome();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //previous
            _ = LogEventAsync("User clicked Back");
            if (webBrowser1.CanGoBack)
            {
                
                webBrowser1.GoBack();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //next
            _ = LogEventAsync("User clicked Forward");
            if(webBrowser1.CanGoForward)
            {
                webBrowser1.GoForward();
            }
        }

        private async void toolStripButton4_Click(object sender, EventArgs e)
        {
            string url = toolStripTextBox1.Text;
            _ = LogEventAsync("User clicked Navigate/pressed Enter");

            bool blocked = await IsBlockedAsync(url);

            if (!blocked)
            {
                webBrowser1.Navigate(url);
            }
            else
            {
                MessageBox.Show("This website is blocked.");
            }
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            string url = toolStripTextBox1.Text;
            _ = LogEventAsync("User clicked Navigate/pressed Enter");

            bool blocked = Blockedw.Any(x => url.Contains(x));

            if (!blocked)
            {
                webBrowser1.Navigate(url);
            }
            else
            {
                MessageBox.Show("This website is blocked.");
            }
        }
        private async Task<bool> IsBlockedAsync(string url)
        {
            return await Task.Run(() =>
                Blockedw.Any(x => url.Contains(x))
            );
        }
        private async Task LogEventAsync(string message)
        {
            await Task.Run(() =>
            {
                Trace.WriteLine(
                    $"{DateTime.Now:dd.MM.yyyy HH:mm:ss.fff} - {message}"
                );
            });
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string keyword = textBox1.Text;

            bool exists = Blockedw.Any(x => x == keyword);

            if (!exists)
            {
                Blockedw.Add(keyword);
                toolStripComboBox1.Items.Add(keyword);

                MessageBox.Show("Keyword added.");
            }
            else
            {
                MessageBox.Show("This keyword already exists.");
            }

            textBox1.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
