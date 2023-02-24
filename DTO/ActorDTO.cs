using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryApp.DTO
{
    public class ActorDTO
    {
        public int IdActor { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phono { get; set; }
        public string DocumentId { get; set; }
        public string Email { get; set; }
        public int IdTypeActor { get; set; }
    }
}