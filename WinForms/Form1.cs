namespace WinForms
{
    using System;
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
    }
}