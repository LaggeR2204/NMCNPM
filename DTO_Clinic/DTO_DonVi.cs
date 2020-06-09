﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_Clinic
{
    public class DTO_DonVi:BaseModel
    {
        private string _tenDonVi;
        private bool _isDeleted;
        public int Id { get; set; }
        public string TenDonVi { get => _tenDonVi; set { _tenDonVi = value; OnPropertyChanged(); }  }        
        public virtual ICollection<DTO_Thuoc> DSThuoc { get; set; }
        public bool IsDeleted { get => _isDeleted; set { _isDeleted = value; OnPropertyChanged(); } }
    }
}