namespace WinForms
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Net;
    using System.Windows.Forms;
    using WinForms.localhost;

    public partial class Form1 : Form
    {
        private Modal modal;

        public Form1()
        {
            this.InitializeComponent();
            this.Text = Resource.Title;
            OkButton.Text = Resource.OK;
            label1.Text = Resource.ValueInput;
            this.modal = new Modal { StartPosition = FormStartPosition.CenterParent };
            this.backgroundWorker1.DoWork += this.BackgroundWorker1DoWork;
            backgroundWorker1.RunWorkerCompleted += this.BackgroundWorker1Completed;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            WebService proxy = new WebService();
            proxy.FibonacciCompleted += this.Proxy_FibonacciCompleted;
            var value = (int)this.numericUpDown1.Value;
            proxy.FibonacciAsync(value);
            this.OkButton.Enabled = false;
            this.modal.ShowDialog();
        }

        private void Proxy_FibonacciCompleted(object sender, FibonacciCompletedEventArgs e)
        {
            this.ResultLabel.Text = string.Format("{0} : {1}", Resource.Result, e.Result);
            // MessageBox.Show(string.Format("{0} : {1}", Resource.Result, e.Result));
            this.OkButton.Enabled = true;
            this.modal.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker1DoWork(object sender, DoWorkEventArgs e)
        {
            var value = (int)this.numericUpDown1.Value;
            this.JsonCall(value);
        }

        private void BackgroundWorker1Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            button1.Enabled = true;
        }
        
        private void JsonCall(int value)
        {
            string url;
            using (WebService proxy = new WebService())
            {
                url = proxy.Url + "/Fibonacci";
            }

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = string.Format("{{\"n\":\"{0}\"}}", value);
                streamWriter.Write(json);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                MessageBox.Show(string.Format("{0} : {1}", Resource.Result, result));
            }
        }
    }
}