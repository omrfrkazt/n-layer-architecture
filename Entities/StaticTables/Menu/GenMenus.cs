using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.StaticTables.Menu
{
    public class GenMenus : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public new int Id { get; set; }
        public string Name { get; set; }
    }
}
