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
    public partial class frmReportAlert : Form
    {
        APIService _usersService = new APIService("users");
        private chatModel.Users item = new chatModel.Users();
        public frmReportAlert(chatModel.Users user)
        {
            InitializeComponent();
            item = user;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var success = MessageBox.Show("Are you sure that you want to report this user?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (success == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    List<int> userTypes = new List<int>();
                    userTypes.Add(2);
                    UsersInsertRequest usersInsertRequest = new UsersInsertRequest();
                    usersInsertRequest.Email = item.Email;
                    usersInsertRequest.Telephone = item.Telephone;
                    usersInsertRequest.Id = item.Id;
                    usersInsertRequest.UserAddressId = item.UserAddressId;
                    usersInsertRequest.LastName = item.LastName;
                    usersInsertRequest.FirstName = item.FirstName;
                    usersInsertRequest.UserName = item.UserName;
                    usersInsertRequest.Status = false;
                    usersInsertRequest.Password = txtPass.Text;
                    usersInsertRequest.PasswordConfirmation = txtConf.Text;
                    usersInsertRequest.UserTypes = userTypes;
                    await _usersService.Update<chatModel.Users>(item.Id, usersInsertRequest);
                    MessageBox.Show("Success", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
