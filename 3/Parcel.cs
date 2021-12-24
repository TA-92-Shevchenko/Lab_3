using System;
using System.Collections.Generic;

#nullable disable

namespace _3
{
    public partial class Parcel
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public string SenderAddress { get; set; }
        public string RecipientAddress { get; set; }
        public string Size { get; set; }
        public double Weight { get; set; }
        public double Price { get; set; }
        public DateTime? SenderDate { get; set; }
    }
}
