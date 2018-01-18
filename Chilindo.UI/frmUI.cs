using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chilindo.UI
{
    public partial class frmUI : Form
    {
        public frmUI()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;
            if (txtAccountNumber.Text.Trim().Length > 0)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:64556/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("X-WEBAPI-KEY", "SPA");
                HttpResponseMessage response = client.GetAsync("api/Customer/Get?AccountNumber=" + txtAccountNumber.Text).Result;  // Blocking call!  
                if (response.IsSuccessStatusCode)
                {
                    txtAddress.Text = "GET :" + response.RequestMessage.RequestUri.OriginalString;
                    var products = response.Content.ReadAsStringAsync().Result;
                    txtResponse.Text = products.ToString();
                }
            }
            else
            {
                MessageBox.Show("Please Enter Account Number");
                txtAccountNumber.Focus();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtAccountNumber.Text.Trim().Length > 0)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:64556/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("X-WEBAPI-KEY", "SPA"); 
                HttpResponseMessage response = client.GetAsync("api/Customer/GetAccountDetails?AccountNumber=" + txtAccountNumber.Text).Result;  // Blocking call!  
                if (response.IsSuccessStatusCode)
                {
                    txtAddress.Text = "GET :" + response.RequestMessage.RequestUri.OriginalString;
                    var products = response.Content.ReadAsStringAsync().Result;
                    txtResponse.Text = products;
                }
                button5.Enabled = true;
            }
            else
            {
                MessageBox.Show("Please Enter Account Number");
                txtAccountNumber.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;
            POSTreq();
        }

        public async void POSTreq()
        {
            Uri requestUri = new Uri("http://localhost:64556/api/Customer/Deposit");
            txtAddress.Text = requestUri.AbsolutePath.ToString();
            var myDetails = JsonConvert.DeserializeObject<WithdrawRequest>(txtRequest.Text);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(myDetails);
            var objClint = new System.Net.Http.HttpClient();
            objClint.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            objClint.DefaultRequestHeaders.Add("X-WEBAPI-KEY", "SPA");
            System.Net.Http.HttpResponseMessage respon = await objClint.PostAsync(requestUri, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
            string responJsonText = await respon.Content.ReadAsStringAsync();
            txtResponse.Text = responJsonText;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;
            POSTWithdraw();
        }

        public async void POSTWithdraw()
        {
            Uri requestUri = new Uri("http://localhost:64556/api/Customer/Withdraw");
            txtAddress.Text = requestUri.AbsolutePath.ToString();
            var myDetails = JsonConvert.DeserializeObject<WithdrawRequest>(txtRequest.Text);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(myDetails);
            var objClint = new System.Net.Http.HttpClient();
            objClint.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            objClint.DefaultRequestHeaders.Add("X-WEBAPI-KEY", "SPA");
            System.Net.Http.HttpResponseMessage respon = await objClint.PostAsync(requestUri, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
            string responJsonText = await respon.Content.ReadAsStringAsync();
            txtResponse.Text = responJsonText;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string json = "[{'AccountId':100,'AccountNumber':100,'Currency':'USD','TransactionType':'Deposit','Amount':100.0,'LastUpdateOn':'2018-01-17T15:24:47.7765063+05:30'},{'AccountId':200,'AccountNumber':200,'Currency':'USD','TransactionType':'Withdraw','Amount':200.0,'LastUpdateOn':'2018-01-17T15:24:48.2360027+05:30'}]";

            //AccountHistory obj = new AccountHistory {
            //    AccountId = 100, AccountNumber = 100, Amount = 100, Currency = "USD", LastUpdateOn = DateTime.Now, TransactionType = "Deposit"
            //};


            //List<AccountHistory> lst = new List<AccountHistory>();
            //lst.Add(new AccountHistory { AccountId = 100, AccountNumber = 100, Amount = 100, Currency = "USD", LastUpdateOn = DateTime.Now, TransactionType = "Deposit" });
            //lst.Add(new AccountHistory { AccountId = 200, AccountNumber = 200, Amount = 200, Currency = "USD", LastUpdateOn = DateTime.Now, TransactionType = "Withdraw" });

           // string output = JsonConvert.SerializeObject(json);

            List<AccountHistory> deserializedProduct = JsonConvert.DeserializeObject<List<AccountHistory>>(txtResponse.Text);
            //DataTable dt = new DataTable();
            //dt.Columns.Add("AccountId", typeof(string));
            //dt.Columns.Add("AccountNumber", typeof(int));
            //dt.Columns.Add("Amount", typeof(int));
            //dt.Columns.Add("Currency", typeof(int));
            //dt.Columns.Add("TransactionType", typeof(int));
            //dt.Columns.Add("LastUpdateOn", typeof(int));

            //foreach(AccountHistory his in deserializedProduct)
            //{
            //    dt[]
            //}

            dataGridView1.DataSource = deserializedProduct;

        }
    }
}
