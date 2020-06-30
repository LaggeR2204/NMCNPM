﻿using BUS_Clinic.BUS;
using DTO_Clinic;
using GUI_Clinic.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ucDanhSachKhamBenh.xaml
    /// </summary>
    public partial class ucDanhSachKhamBenh : UserControl
    {
        public ucDanhSachKhamBenh()
        {
            InitializeComponent();
            this.DataContext = this;
            InitData();
            InitCommand();
        }
        #region Property
        public ObservableCollection<DTO_BenhNhan> ListBN1 { get; set; }
        public ObservableCollection<DTO_BenhNhan> ListBN2 { get; set; }
        public List<string> MatchBNList { get; set; }
        public ObservableCollection<DTO_BenhNhan> CurSignedList { get; set; }
        public List<string> RegionIDList { get; set; }
        #endregion
        #region Command
        public ICommand AddPatientCommand { get; set; }
        public ICommand SignedCommand { get; set; }
        #endregion
        #region
        public event EventHandler PatientSigned;
        #endregion
        public void InitData()
        {
            RegionIDList = new List<string>();
            //Doc danh sach ma vung so dien thoai
            string line = "";
            StreamReader streamReader = new StreamReader(System.IO.Path.Combine(Environment.CurrentDirectory.Replace("bin\\Debug", ""), "Resource\\MAVUNG.txt"));

            while ((line = streamReader.ReadLine()) != null)
            {
                RegionIDList.Add(line);
            }
            dpkNgayKham.SelectedDate = DateTime.Now;
            //set itemsource cho list view danh sách khám
            ListBN1 = new ObservableCollection<DTO_BenhNhan>(BUSManager.BenhNhanBUS.GetListBN());
            ListBN2 = new ObservableCollection<DTO_BenhNhan>(BUSManager.BenhNhanBUS.GetListBN());
            CurSignedList = new ObservableCollection<DTO_BenhNhan>();
            //set itemsource
            cbxDSBenhNhan.ItemsSource = ListBN2;
            lvDSKham.ItemsSource = CurSignedList;
            //Lọc danh sách khám theo ngày
            CollectionView viewBenhNhan = (CollectionView)CollectionViewSource.GetDefaultView(ListBN1);
            viewBenhNhan.Filter = BenhNhanFilter;
        }
        public void InitCommand()
        {
            AddPatientCommand = new RelayCommand<Window>((p) =>
            {
                if (string.IsNullOrEmpty(tbxHoTen.Text) ||
                    string.IsNullOrEmpty(tbxDiaChi.Text) ||
                    string.IsNullOrEmpty(tbxSDT.Text) ||
                    cbxMaVungSDT.SelectedIndex == -1 ||
                    cbxGioiTinh.SelectedIndex == -1 ||
                    !dpkNgaySinh.SelectedDate.HasValue)
                    return false;
                return true;
            }, (p) =>
            {
                bool gt;
                if (cbxGioiTinh.SelectedIndex == 0)
                    gt = false;
                else
                    gt = true;
                DTO_BenhNhan benhNhan = new DTO_BenhNhan(tbxHoTen.Text, gt, dpkNgaySinh.SelectedDate.Value, tbxDiaChi.Text, cbxMaVungSDT.Text + tbxSDT.Text);
                BUSManager.BenhNhanBUS.AddBenhNhan(benhNhan);
                ListBN1.Add(benhNhan);
                ListBN2.Add(benhNhan);
                if (ckbDangKy.IsChecked.Value)
                    DangKyKham(benhNhan);
                Clear();
            });
            SignedCommand = new RelayCommand<Window>((p) =>
            {
                if (cbxDSBenhNhan.SelectedIndex == -1)
                    return false;
                return true;
            }, (p) =>
            {
                DangKyKham(cbxDSBenhNhan.SelectedItem as DTO_BenhNhan);
                cbxDSBenhNhan.SelectedIndex = -1;
            });
        }
        private bool BenhNhanFilter(Object item)
        {
            if (String.IsNullOrEmpty(dpkNgayKham.Text))
            {
                return false;
            }
            else
            {
                if (MatchBNList != null)
                    return (MatchBNList.Contains((item as DTO_BenhNhan).Id));
                return false;
            }
        }
        private void dpkNgayKham_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpkNgayKham.SelectedDate.HasValue)
            {
                if (dpkNgayKham.SelectedDate.Value.ToString("d") == DateTime.Now.ToString("d"))
                {
                    lvDSKham.ItemsSource = CurSignedList;
                }
                else
                {
                    lvDSKham.ItemsSource = ListBN1;
                    RefreshList();
                }
            }
        }
        private void RefreshList()
        {
            MatchBNList = BUSManager.PhieuKhamBenhBUS.GetListPKB(dpkNgayKham.SelectedDate.Value.ToString("d"));
            CollectionViewSource.GetDefaultView(lvDSKham.ItemsSource).Refresh();
        }
        public void Clear()
        {
            tbxSDT.Clear();
            tbxHoTen.Clear();
            tbxDiaChi.Clear();
            dpkNgaySinh.SelectedDate = null;
            cbxGioiTinh.SelectedIndex = -1;
        }
        private void DangKyKham(DTO_BenhNhan bn)
        {
            if (CurSignedList.Contains(bn))
            {
                MessageBox.Show("Benh nhan da duoc dang ky");
                return;
            }
            CurSignedList.Add(bn);
            if (PatientSigned != null)
                PatientSigned(bn, new EventArgs());
        }

        private void tbxSDT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private static readonly Regex _regex = new Regex(@"([^0-9]+)|\s+", RegexOptions.Singleline); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void tbxSDT_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}
