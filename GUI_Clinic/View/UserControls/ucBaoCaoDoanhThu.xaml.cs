﻿using BUS_Clinic.BUS;
using DTO_Clinic;
using GUI_Clinic.View.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GUI_Clinic.Command;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI_Clinic.View.UserControls
{
    /// <summary>
    /// Interaction logic for ucBaoCaoDoanhThu.xaml
    /// </summary>
    public partial class ucBaoCaoDoanhThu : UserControl
    {
        public ucBaoCaoDoanhThu()
        {
            InitializeComponent();
            this.DataContext = this;

            InitData();
            InitCommand();
        }

        #region Property
        private ObservableCollection<DTO_BCDoanhThu> ListBCDT { get; set; }
        private ICollection<DTO_CTBaoCaoDoanhThu> ListCTBCDT { get; set; }
        private DTO_BCDoanhThu bCDoanhThu { get; set; }
        private List<int> ListThang { get; set; }
        private List<int> ListNam { get; set; }

        #endregion

        #region Command
        public ICommand FilterBaoCaoCommand { get; set; }
        public ICommand InBaoCaoCommand { get; set; }
        #endregion

        public void InitData()
        {

            ListThang = Enumerable.Range(1, 12).ToList();
            cbxThang.ItemsSource = ListThang;
            cbxThang.SelectedIndex = DateTime.Now.Month - 1;
            ListNam = Enumerable.Range(1950, 100).ToList();
            cbxNam.ItemsSource = ListNam;
            cbxNam.SelectedIndex = DateTime.Now.Year - 1950;

            ListBCDT = BUSManager.BCDoanhThuBUS.GetListBCDoanhThu();


            foreach (DTO_BCDoanhThu item in ListBCDT)
            {
                if (item.Thang == int.Parse(cbxThang.Text) && item.Nam == int.Parse(cbxNam.Text))
                {
                    bCDoanhThu = item;
                }

            }
            BUSManager.BCDoanhThuBUS.LoadNPCTBaoCaoDoanhThu(bCDoanhThu);
            ListCTBCDT = bCDoanhThu.DS_CTBaoCaoDoanhThu;
            lvCTBaoCaoDoanhThu.ItemsSource = ListCTBCDT;
            tblTongDoanhThu.Text = bCDoanhThu.TongDoanhThu.ToString();

        }

        public void InitCommand()
        {
            FilterBaoCaoCommand = new RelayCommand<Window>((p) =>
            {
                if (String.IsNullOrEmpty(cbxThang.Text) ||
                    String.IsNullOrEmpty(cbxNam.Text))
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
               
                bCDoanhThu = null;
                ListCTBCDT = null;
                foreach (DTO_BCDoanhThu item in ListBCDT)
                {
                    if(item.Thang == int.Parse(cbxThang.Text) && item.Nam == int.Parse(cbxNam.Text))
                    {
                        bCDoanhThu = item;
                    }

                }
                if(bCDoanhThu!=null)
                {
                    BUSManager.BCDoanhThuBUS.LoadNPCTBaoCaoDoanhThu(bCDoanhThu);
                    ListCTBCDT = bCDoanhThu.DS_CTBaoCaoDoanhThu;
                    lvCTBaoCaoDoanhThu.ItemsSource = ListCTBCDT;
                    tblTongDoanhThu.Text = bCDoanhThu.TongDoanhThu.ToString();
                }
                else
                {
                    lvCTBaoCaoDoanhThu.ItemsSource = ListCTBCDT;
                    tblTongDoanhThu.Text = "0";
                }
               
            });
            InBaoCaoCommand = new RelayCommand<Window>((p) =>
            {
                if (String.IsNullOrEmpty(cbxThang.Text) ||
                    String.IsNullOrEmpty(cbxNam.Text))
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                //wdInBaoCaoSuDungThuoc baoCaoSuDungThuoc = new wdInBaoCaoSuDungThuoc(BUSManager.BCSuDungThuocBUS.GetBaoCaoByMonth(cbxThang.SelectedIndex + 1, cbxNam.SelectedIndex + 1950), cbxThang.SelectedIndex + 1, cbxNam.SelectedIndex + 1950);
                //baoCaoSuDungThuoc.ShowDialog();
            });
        }


    }
}
