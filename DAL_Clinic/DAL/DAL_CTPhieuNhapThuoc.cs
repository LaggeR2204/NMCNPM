﻿using DTO_Clinic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Clinic.DAL
{
    public class DAL_CTPhieuNhapThuoc : BaseDAL
    {
        public DAL_CTPhieuNhapThuoc()
        {

        }
        public void LoadNPThuoc(DTO_CTPhieuNhapThuoc cTPhieuNhapThuoc)
        {
            var entry = SQLServerDBContext.Instant.Entry(cTPhieuNhapThuoc);
            entry.Reference(c => c.Thuoc).Load();
        }
        public void LoadNPPhieuNhapThuoc(DTO_CTPhieuNhapThuoc cTPhieuNhapThuoc)
        {
            var entry = SQLServerDBContext.Instant.Entry(cTPhieuNhapThuoc);
            entry.Reference(c => c.PhieuNhapThuoc).Load();
        }
        public override void LoadLocalData()
        {
            SQLServerDBContext.Instant.CTPhieuNhapThuoc.Load();
        }
        public ObservableCollection<DTO_CTPhieuNhapThuoc> GetListCTPNT()
        {
            return SQLServerDBContext.Instant.CTPhieuNhapThuoc.Local;
        }

        public void AddCTPhieuNhapThuoc(DTO_CTPhieuNhapThuoc ctPhieuNhapThuoc)
        {
            SQLServerDBContext.Instant.CTPhieuNhapThuoc.Local.Add(ctPhieuNhapThuoc);
        }
    }
}
