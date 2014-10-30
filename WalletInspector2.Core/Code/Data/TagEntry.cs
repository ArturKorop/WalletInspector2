using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletInspector2.Core.Code.Data
{
    public class TagEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public Guid UserId { get; set; }

        public TagEntry()
        {

        }

        public TagEntry(string name, Guid userId) : this()
        {
            this.Name = name;
            this.UserId = userId;
        }
    }
}
