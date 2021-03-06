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
    public class BUS_DonVi : BaseBUS
    {
        private const string _idPrefix = "DV";
        public BUS_DonVi()
        {

        }
        public bool AddDonVi(DTO_DonVi donVi)
        {
            ObservableCollection<DTO_DonVi> donvis = DALManager.DonViDAL.GetListDV();

            if (donvis.Any(d => d.TenDonVi.Equals(donVi.TenDonVi, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }
            donVi.Id = AutoGenerateID();
            DALManager.DonViDAL.AddDonVi(donVi);
            return true;
        }
        public bool UpdateDonVi(DTO_DonVi donVi, string tenDonViMoi)
        {
            ObservableCollection<DTO_DonVi> donvis = DALManager.DonViDAL.GetListDV();

            if (donVi.TenDonVi == tenDonViMoi || donvis.Any(d => d.TenDonVi.Equals(tenDonViMoi, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            donVi.TenDonVi = tenDonViMoi;
            return true;
        }
        //public bool DelDonVi(DTO_DonVi donVi)
        //{
        //    ObservableCollection<DTO_DonVi> donvis = DALManager.DonViDAL.GetListDV();
        //    if (donVi != null)
        //    {
        //        if (donvis.Any(d => d.TenDonVi.Equals(donVi.TenDonVi, StringComparison.OrdinalIgnoreCase)))
        //        {
        //            DALManager.DonViDAL.DelDonVi(donVi);
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        public DTO_DonVi GetDonViById(string maDonVi)
        {
            ObservableCollection<DTO_DonVi> donvis = DALManager.DonViDAL.GetListDV();

            var dv = donvis.Where(c => c.Id == maDonVi).FirstOrDefault();
            
            return dv;
        }
        public override void LoadLocalData()
        {
            DALManager.DonViDAL.LoadLocalData();
        }
        public ObservableCollection<DTO_DonVi> GetListDV()
        {
            return DALManager.DonViDAL.GetListDV();
        }
        public int DemSoDonVi()
        {
            int re = DALManager.DonViDAL.GetListDV().Count;
            return re;
        }
        public string AutoGenerateID()
        {
            return _idPrefix + (DemSoDonVi() + 1).ToString("D5");
        }
    }
}
