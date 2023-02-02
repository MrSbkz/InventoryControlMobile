using System;

namespace InventoryControlMobile.Models
{
    public class DeviceResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime RegisterDate { get; set; }

        public DateTime? DecommissionDate { get; set; }

        public User AssignedTo { get; set; }
    }
}