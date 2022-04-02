using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDataBaseImplement.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
    }
}
