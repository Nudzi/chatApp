using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chatApp.WinUI.Feedbacks
{
    public partial class frmFeedbackDetails : Form
    {
        private chatModel.Feedbacks _feedback = new chatModel.Feedbacks();
        public frmFeedbackDetails(chatModel.Feedbacks feedback)
        {
            InitializeComponent();
            _feedback = feedback;
        }

        private void frmFeedbackDetails_Load(object sender, EventArgs e)
        {
            txtFeedback.Text = _feedback.Feedback;
            txtReason.Text = _feedback.Reason;
            txtNumber.Text = _feedback.ReportedUserId.ToString();
            if (_feedback.Image.Length != 0)
            {
                pbImage.Image = GetImage(_feedback.Image);
            }
        }

        private static Image GetImage(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return (Image.FromStream(ms));
            }
        }
    }
}
