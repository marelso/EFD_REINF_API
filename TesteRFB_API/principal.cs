namespace TesteRFB_API
{
    public partial class EFD_Reinf : Form
    {
        string[] requests = new string[] { "POST", "GET" };
        public EFD_Reinf()
        {
            InitializeComponent();
        }

        private void EFD_Reinf_Load(object sender, EventArgs e)
        {
            ddlHttp.Items.Clear();
            ddlHttp.Items.AddRange(requests);
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            string response = string.Empty;

            switch (ddlHttp.SelectedIndex)
            {
                default:
                    break;
            }
        }
    }
}