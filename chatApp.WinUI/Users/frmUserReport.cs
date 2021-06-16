using chatModel.Requests.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chatApp.WinUI.Users
{
    public partial class frmUserReport : Form
    {
        APIService _usersService = new APIService("users");
        public frmUserReport()
        {
            InitializeComponent();
        }

        private async void frmUserReport_Load(object sender, EventArgs e)
        {
            await LoadUsers();
        }
        private async Task LoadUsers()
        {
            UsersSearchRequest request = new UsersSearchRequest();
            if (txtSearch.Text != "") request.PhoneNumber = txtSearch.Text;
            else request = null;

            var list = await _usersService.Get<List<chatModel.Users>>(request);
            dgvUsers.DataSource = list;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await LoadUsers();
        }

        private void dgvUsers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            chatModel.Users item = (chatModel.Users)dgvUsers.SelectedRows[0].DataBoundItem;
            frmReportAlert frm = new frmReportAlert(item);
            frm.Show();
        }
    }
}
