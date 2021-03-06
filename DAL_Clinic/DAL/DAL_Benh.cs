﻿using DTO_Clinic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Clinic.DAL
{
    public class DAL_Benh : BaseDAL
    {
        public DAL_Benh()
        {
        }
        public void AddBenh(DTO_Benh benh)
        {
            SQLServerDBContext.Instant.Benh.Local.Add(benh);
        }
        //public void DelBenh(DTO_Benh benh)
        //{
        //    SQLServerDBContext.Instant.Benh.Local.Where(x => x.Id == benh.Id).FirstOrDefault().IsDeleted=true;
        //}
        public override void LoadLocalData()
        {
            SQLServerDBContext.Instant.Benh.Load();
        }
        public ObservableCollection<DTO_Benh> GetListBenh()
        {
            return SQLServerDBContext.Instant.Benh.Local;
        }
    }
}
