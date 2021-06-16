using chatModel.Requests.Feedbacks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chatApp.WinUI.Feedbacks
{
    public partial class frmFeedbacksAll : Form
    {
        APIService _feedbackService = new APIService("feedbacks");
        public frmFeedbacksAll()
        {
            InitializeComponent();
        }

        private async void frmFeedbacksAll_Load(object sender, EventArgs e)
        {
            await LoadFeedbacks();
        }
        private async Task LoadFeedbacks()
        {
            int telephone;
            var success = int.TryParse(txtSearch.Text, out telephone);
            FeedbacksSearchRequest request = new FeedbacksSearchRequest();
            if (success) request.ReportedUserId = telephone;
            else request = null;

            var list = await _feedbackService.Get<List<chatModel.Feedbacks>>(request);
            dgvFeedbacks.AutoGenerateColumns = false;

            dgvFeedbacks.DataSource = list;
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await LoadFeedbacks();
        }

        private async void dgvFeedbacks_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var item = dgvFeedbacks.SelectedRows[0].DataBoundItem;
            var feed = await _feedbackService.GetById<chatModel.Feedbacks>((item as chatModel.Feedbacks).Id);
            frmFeedbackDetails frm = new frmFeedbackDetails(feed);
            frm.WindowState = FormWindowState.Normal;
            frm.Show();
        }
    }
}
