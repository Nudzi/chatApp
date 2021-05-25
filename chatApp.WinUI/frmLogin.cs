using chatApp.WinUI.Index;
using chatModel.Enums;
using System;
using System.Windows.Forms;

namespace chatApp.WinUI
{
    public partial class frmLogin : Form
    {
        protected APIService _usersService = new APIService("users");
        protected APIService _userTypesService = new APIService("userTypes");
        chatModel.UserTypes role = null;
        public frmLogin()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            APIService.userName = txtUserName.Text;
            APIService.password = txtPassword.Text;
            try
            {
                if (string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPassword.Text))
                {
                    MessageBox.Show("All fields are required! Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                else
                {
                    chatModel.Users user = await _usersService.Authentication<chatModel.Users>(txtUserName.Text, txtPassword.Text);

                    checkUserType(user);
                    MessageBox.Show("Welcome:\n " + user.FirstName + " " + user.LastName);
                    DialogResult = DialogResult.OK;
                    this.Hide();
                    if (Global.Admin)
                    {
                        frmIndex frm = new frmIndex();
                        frm.Show();
                    }
                    if (Global.User)
                    {
                        MessageBox.Show("Wrong username or password", "You do not have permissions!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Wrong username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void checkUserType(chatModel.Users user)
        {
            int userTypeFirst = 0;
            if (user != null)
            {
                Global.LoggedUser = user;

                foreach (var item in Global.LoggedUser.UserTypes)
                {
                    if (item.IsActive)
                        userTypeFirst = item.UserTypeId;
                }
                role = await _userTypesService.GetById<chatModel.UserTypes>(userTypeFirst);
                if (role.Id == (int)UserTypes.Admin)
                    Global.Admin = true;
                if (role.Id == (int)UserTypes.Employee)
                    Global.Employee = true;
                if (role.Id == (int)UserTypes.User)
                    Global.User = true;
            }
        }
    }
}
