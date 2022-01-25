using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Model
{
    [Table("tb_m_categories")]
    public class ProblemCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
