namespace WinForms
{
    using System.Windows.Forms;

    public partial class Modal : Form
    {
        public Modal()
        {
            InitializeComponent();
            this.label1.Text = Resource.Wait;
        }
    }
}