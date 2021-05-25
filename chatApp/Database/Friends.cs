using System;
using System.Collections.Generic;

namespace chatApp.Database
{
    public partial class Friends
    {
        public int Id { get; set; }
        public int UserIdprimary { get; set; }
        public int UserIdsecondary { get; set; }
        public DateTime DateAdded { get; set; }

        public virtual Users UserIdprimaryNavigation { get; set; }
        public virtual Users UserIdsecondaryNavigation { get; set; }
    }
}
