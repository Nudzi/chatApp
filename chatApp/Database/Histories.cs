using System;
using System.Collections.Generic;

namespace chatApp.Database
{
    public partial class Histories
    {
        public int Id { get; set; }
        public int UserIdPrimary { get; set; }
        public int UserIdSecondary { get; set; }
        public string MessagePrimary { get; set; }
        public string MessageSecondary { get; set; }
        public byte[] Image { get; set; }
        public byte[] ImageThumb { get; set; }

        public virtual Users UserIdPrimaryNavigation { get; set; }
        public virtual Users UserIdSecondaryNavigation { get; set; }
    }
}
