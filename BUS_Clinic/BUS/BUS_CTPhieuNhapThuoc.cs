﻿using DAL_Clinic.DAL;
using DTO_Clinic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS_Clinic.BUS
{
    public class BUS_CTPhieuNhapThuoc : BaseBUS
    {
        public BUS_CTPhieuNhapThuoc()
        {

        }
        public void LoadNPThuoc(DTO_CTPhieuNhapThuoc cTPhieuNhapThuoc)
        {
            DALManager.CTPhieuNhapThuocDAL.LoadNPThuoc(cTPhieuNhapThuoc);
        }
        public void LoadNPPhieuNhapThuoc(DTO_CTPhieuNhapThuoc cTPhieuNhapThuoc)
        {
            DALManager.CTPhieuNhapThuocDAL.LoadNPPhieuNhapThuoc(cTPhieuNhapThuoc);
        }
        public override void LoadLocalData()
        {
            DALManager.CTPhieuNhapThuocDAL.LoadLocalData();
        }
        public ObservableCollection<DTO_CTPhieuNhapThuoc> GetListCTPNT()
        {
            return DALManager.CTPhieuNhapThuocDAL.GetListCTPNT();
        }

        public void AddCTPhieuNhapThuoc (DTO_CTPhieuNhapThuoc ctPhieuNhapThuoc)
        {
            DALManager.CTPhieuNhapThuocDAL.AddCTPhieuNhapThuoc(ctPhieuNhapThuoc);
        }
    }
}
