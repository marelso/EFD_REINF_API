using System.Net;
using System.Xml;

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
                case 0:
                    response = POST(txtAdress.Text, txtContent.Text);
                    break;
                case 1:
                    response = GET(txtAdress.Text, txtContent.Text);
                    break;
                default:
                    MessageBox.Show("Selecione alguma das requisi��es v�lidas.");
                    break;
            }

            if (string.IsNullOrEmpty(response))
                txtResponse.Text = string.Format("Erro ao receber a resposta da opera��o, refa�a a requisi��o.\n");
            else
                txtResponse.Text = response;

            return;
        }

        private string POST(string adress, string content)
        {
            string response = string.Empty;

            try
            {
                #region Send request
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(adress);
                request.Method = "POST";
                request.Timeout = 2400000;
                request.Accept = "application/xml";
                request.ContentType = "application/xml";
                
                using(var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(content);
                }
                #endregion

                #region Get response
                HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }
                #endregion

                #region XML Desserialization
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(response);
                XmlNode root = doc.DocumentElement;
                response = string.Format("{0}\n", root.SelectSingleNode("cdStatus").InnerXml);

                foreach (XmlNode retorno in root.SelectSingleNode("retornoEventos").ChildNodes)
                {
                    response += string.Format("{0} - {1}\n", retorno.SelectSingleNode("evento").Attributes["id"].Value,
                        retorno.SelectSingleNode("cdRetorno").InnerXml);
                }

                response = root.SelectSingleNode("descRetorno").InnerXml;
                response = string.Format("XML Desserializado meramente ilustrativo");
                #endregion

                return response;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Erro:\n {0}", ex.Message));
                return string.Empty;
            }
        }
        private string GET(string adress, string content)
        {
            string response = string.Empty;

            return response;
        }
    }
}