using System;
using System.Collections.Generic;

namespace chatApp.Database
{
    public partial class UserImages
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public byte[] Image { get; set; }
        public byte[] ImageThumb { get; set; }

        public virtual Users User { get; set; }
    }
}
