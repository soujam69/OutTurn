using Caliburn.Micro;
using OutTurn.Models;
using OutTurn.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace OutTurn.ViewModels
{
    public class NewViewModel: Screen, IDataErrorInfo
    {
        public NewViewModel()
        {
            //Runs Method to populate Taken By combobox
            PopulateAddPerson();

            //Loads a list of adhesives from BVP and stores them in an observable collection
            //for use in the Adhesive Type combobox
            BVPAdhesives = new ObservableCollection<BVPAdhesiveModel>();
            foreach (var adh in DataAccess.GetAdhesives())
            {
                BVPAdhesives.Add(adh);
            }

            //Loads a list of rollstands from BVP and stores them in an observable collection
            //for use in Stand combobox
            RollStand = new ObservableCollection<RollStandModel>();
            foreach (var ars in DataAccess.GetAllRollStands())
            {
                RollStand.Add(ars);
            }

            //Creates a list of R,L for use in Side combobox
            RollStandSide = new ObservableCollection<RollStandSideModel>();
            RollStandSide.Add(new RollStandSideModel
            {
                RollStandSide = "R"
            });

            RollStandSide.Add(new RollStandSideModel
            {
                RollStandSide = "L"
            });           
        }

        private ObservableCollection<BVPOrder> BVPOrder = new ObservableCollection<BVPOrder>();

        private ObservableCollection<XMLRollStand> XMLRollStand = new ObservableCollection<XMLRollStand>();

        private ObservableCollection<BVPRollStock> BVPRollStock = new ObservableCollection<BVPRollStock>();

        private XMLLineSpeed XMLLineSpeed = new XMLLineSpeed();


        SampleFullModel sf = new SampleFullModel();

        //Property for storing sorted rollstands
        private ICollectionView _SortedRollStands;

        public ICollectionView SortedRollStands
        {
            get { return _SortedRollStands; }
            set
            {
                _SortedRollStands = value;
                NotifyOfPropertyChange();
            }
        }

        public void PopulateAddPerson()
        {
            //Loads a list of persons that take samples and stores them in an observable collection
            //for use in the Taken By combobox


            Persons = new ObservableCollection<PersonModel>();
            foreach (var p in DataAccess.GetAllPersons())
            {
                Persons.Add(p);
            }
            
            //var update = DataAccess.GetAllPersons();
            //Persons.Clear();
            //foreach (var item in update)
            //{
            //    Persons.Add(item);
            //}
        }

        public void AddPerson()
        {
            var manager = new WindowManager();
            manager.ShowDialog(new AddPersonViewModel());
        }


        //public bool CanSaveOutTurn(bool isValid)
        //{
        //    return ValidateSampleInfo();
        //}

        public void Cancel()
        {
            var manager = new WindowManager();
            //manager.ShowDialog(new EditViewModel());
            manager.ShowWindow(new EditViewModel());          
        }


        public void Delete()
        {
            var manager = new WindowManager();
            manager.ShowPopup(new EditView());
        }

        public bool CanDelete
        {
            get
            {
                return SampleNo != null;
            }
        }

        public void SaveOutTurn()
        {

            IsValid = ValidateSampleInfo();
            

            if (IsValid == true)
            {
                CreateDateTime = DateTime.Now;

                sf.SampleInfo.OrderNo = Convert.ToInt32(OrderNo);
                sf.SampleInfo.CreateDateTime = Convert.ToDateTime(CreateDateTime);
                sf.SampleInfo.PersonId = Convert.ToInt32(PersonId);
                sf.SampleInfo.Customer = Customer;
                sf.SampleInfo.Grade = Grade;
                sf.SampleInfo.RunWidth = Convert.ToDouble(RunWidth);
                sf.SampleInfo.RunLength = Convert.ToDouble(RunLength);
                sf.SampleInfo.NoOut = Convert.ToInt32(NoOut);
                sf.SampleInfo.FPM = Convert.ToInt32(FPM);
                sf.SampleInfo.AdhesiveType = AdhesiveType;
                sf.SampleInfo.AdhesiveTemp = Convert.ToInt32(AdhesiveTemp);

                sf.SampleInfo.Caliper1 = Caliper1;
                sf.SampleInfo.Caliper2 = Caliper2;
                sf.SampleInfo.Caliper3 = Caliper3;
                sf.SampleInfo.Caliper4 = Caliper4;
                sf.SampleInfo.Caliper5 = Caliper5;
                sf.SampleInfo.CaliperAvg = CaliperAvg;
                
                sf.SampleInfo.Burst1 = Burst1;
                sf.SampleInfo.Burst2 = Burst2;
                sf.SampleInfo.Burst3 = Burst3;
                sf.SampleInfo.Burst4 = Burst4;
                sf.SampleInfo.Burst5 = Burst5;
                sf.SampleInfo.Burst6 = Burst6;
                sf.SampleInfo.BurstAvg = BurstAvg;

                sf.SampleInfo.BasisWt = BasisWt;
                sf.SampleInfo.FinWidth = FinWidth;
                sf.SampleInfo.FinLengh = FinLength;

                sf.SampleInfo.ZDT1 = ZDT1;
                sf.SampleInfo.ZDT2 = ZDT2;
                sf.SampleInfo.ZDT3 = ZDT3;
                sf.SampleInfo.ZDTAvg = ZDTAvg;

                sf.SampleInfo.MDTensile1 = MDTensile1;
                sf.SampleInfo.MDTensile2 = MDTensile2;
                sf.SampleInfo.MDTensile3 = MDTensile3;
                sf.SampleInfo.MDTensileAvg = MDTensileAvg;
                sf.SampleInfo.CDTensile1 = CDTensile1;
                sf.SampleInfo.CDTensile2 = CDTensile2;
                sf.SampleInfo.CDTensile3 = CDTensile3;
                sf.SampleInfo.CDTensileAvg = CDTensileAvg;

                sf.SampleInfo.FMCBefore = FMCBefore;
                sf.SampleInfo.FMCAfter = FMCAfter;
                sf.SampleInfo.FMCAvg = FMCAvg;
                sf.SampleInfo.MMCBefore = MMCBefore;
                sf.SampleInfo.MMCAfter = MMCAfter;
                sf.SampleInfo.MMCAvg = MMCAvg;
                sf.SampleInfo.BMCBefore = BMCBefore;
                sf.SampleInfo.BMCAfter = BMCAfter;
                sf.SampleInfo.BMCAvg = BMCAvg;
                sf.SampleInfo.MCTotalAvg = MCTotalAvg;

                sf.SampleInfo.FWIBefore = FWIBefore;
                sf.SampleInfo.FWIAfter = FWIAfter;
                sf.SampleInfo.FWIAvg = FWIAvg;
                sf.SampleInfo.MWIBefore = MWIBefore;
                sf.SampleInfo.MWIAfter = MWIAfter;
                sf.SampleInfo.MWIAvg = MWIAvg;
                sf.SampleInfo.BWIBefore = BWIBefore;
                sf.SampleInfo.BWIAfter = BWIAfter;
                sf.SampleInfo.BWIAvg = BWIAvg;
                sf.SampleInfo.WITotalAvg = WITotalAvg;

                if (SampleRollStands != null)
                {
                    if (SampleRollStands.Count < 2)
                    {
                        MessageBox.Show("Roll Stand Info cannot be less than two plies!");
                    }
                    else
                    {
                        foreach (var item in SampleRollStands)
                        {
                            sf.RollStandInfo.Add(item);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Roll Stand Info is empty!");
                }
                sf = DataAccess.CreateFullSample(sf);
                SampleNo = Convert.ToInt32(sf.SampleInfo.DailySampleNo);
            }
            else
            {
                MessageBox.Show("Form Data is not valid!");
            }

        }

        public void SearchRollNo(string rollNo)
        {
            BVPRollStock = new ObservableCollection<BVPRollStock>();
            foreach (var item in DataAccess.GetRollStock(rollNo))
            {
                BVPRollStock.Add(item);
                RollDesc = item.Roll_Desc;
                RollWidth = Convert.ToDouble(item.Roll_Width);
            }

            //DO NOT USE
            //SampleRollStands = new ObservableCollection<SampleRollStandModel>();
            //foreach (var item in BVPRollStock)
            //{
            //    SampleRollStands.Add(new SampleRollStandModel()
            //    {
            //        RollNo = item.Ticket_No,
            //        RollDesc = item.Roll_Desc
            //    });
            //}

        }

#region Reset all fields
        public void Reset()
        {
            var manager = new WindowManager();
            //manager.ShowDialog(new EditViewModel());
            manager.ShowWindow(new ReportsViewModel());

            if (IsCaliperDirty == true)
            {
                MessageBox.Show("Caliper Changed!");
            }
            else
            {
                MessageBox.Show("Not Changed!");
            }

            //CreateDateTime = null;
            //SampleNo = null;
            //Customer = null;
            //OrderNo = null;
            //TrialNo = null;
            //Caliper = null;
            //Grade = null;
            //RunWidth = null;
            //RunLength = null;
            //NoOut = null;
            //AdhesiveType = null;
            //AdhesiveTemp = null;
            //FPM = null;
            //SampleRollStands.Clear();
            //Caliper1 = null;
            //Caliper2 = null;
            //Caliper3 = null;
            //Caliper4 = null;
            //Caliper5 = null;
            //CaliperAvg = null;
            //Burst1 = null;
            //Burst2 = null;
            //Burst3 = null;
            //Burst4 = null;
            //Burst5 = null;
            //Burst6 = null;
            //BurstAvg = null;
            //ZDT1 = null;
            //ZDT2 = null;
            //ZDT3 = null;
            //ZDTAvg = null;
            //BasisWt = null;
            //FinWidth = null;
            //FinLength = null;
            //CDTensile1 = null;
            //CDTensile2 = null;
            //CDTensile3 = null;
            //CDTensileAvg = null;
            //MDTensile1 = null;
            //MDTensile2 = null;
            //MDTensile3 = null;
            //MDTensileAvg = null;
            //FMCBefore = null;
            //FMCAfter = null;
            //FMCAvg = null;
            //MMCBefore = null;
            //MMCAfter = null;
            //MMCAvg = null;
            //BMCBefore = null;
            //BMCAfter = null;
            //BMCAvg = null;
            //FWIBefore = null;
            //FWIAfter = null;
            //FWIAvg = null;
            //MWIBefore = null;
            //MWIAfter = null;
            //MWIAvg = null;
            //BWIBefore = null;
            //BWIAfter = null;
            //BWIAvg = null;
            //WITotalAvg = null;
            //Notes = null;
        }
        #endregion

        public void CalcCaliperAvg()
        {
            CaliperAvg = (Convert.ToDouble(Caliper1) + Convert.ToDouble(Caliper2) + Convert.ToDouble(Caliper3) + Convert.ToDouble(Caliper4) + Convert.ToDouble(Caliper5)) / 5;
        }


        public void SearchOrderNoClick(int orderNo)
        {
            //CurrentViewModel = IoC.Get<SampleInfoViewModel>();

            int count = 0;

            BVPOrder = new ObservableCollection<BVPOrder>();
            foreach (var ord in DataAccess.GetOrder(orderNo))
            {
                BVPOrder.Add(ord);
                count = BVPOrder.Count;

                Customer = ord.Cust_Name;
                OrderNo = ord.OrderNo;
                Caliper = ord.Caliper;
                Grade = ord.Grade;
                RunWidth = ord.RunWid;
                RunLength = ord.RunLen;
                NoOut = ord.No_Out;
                AdhesiveType = ord.Adhesive_Type;
            }

            if (count == 0)
            {
                MessageBox.Show("Order No not found.");
                Customer = null;
                OrderNo = null;
                Caliper = null;
                Grade = null;
                RunWidth = null;
                RunLength = null;
                NoOut = null;
                AdhesiveType = null;
            }
        }

        #region Button AddStandInfoClick
        public void AddStandInfoClick()
        {
            IsValid = ValidateRollStand();

            if (IsValid == true)
            {
                SampleRollStandModel vmRollStand = new SampleRollStandModel();
                
                vmRollStand.RollStandId = Stand;
                vmRollStand.Side = Side;
                vmRollStand.RollNo = RollNo;
                vmRollStand.RollDesc = RollDesc;
                vmRollStand.RollWidth = Convert.ToDouble(RollWidth);
                vmRollStand.Tension = Convert.ToInt32(Tension);
                vmRollStand.Pressure = Convert.ToInt32(Pressure);

                SampleRollStands.Add(vmRollStand);

                //Calls Method to sort SampleRollStands by RollStand in descending order
                SortRollStands();


                Stand = null;
                Side = null;
                RollNo = null;
                RollDesc = null;
                RollWidth = null;
                Tension = null;
                Pressure = null;
            }
            else
            {
                MessageBox.Show("All required fields are not filled in.");
            }
        }
        #endregion
        
        //Sorts SampleRollStands by RollStand in descending order
        public void SortRollStands()
        {            
            SortedRollStands = CollectionViewSource.GetDefaultView(SampleRollStands);
            SortedRollStands.SortDescriptions.Add(new SortDescription("RollStandId", ListSortDirection.Descending));
        }

        #region Button SnapShotClick
        public void SnapShotClick()
        {
            
            //Captures if the line is running and the currently running 
            //LineSpeed from the Beckoff XML file.
            //If the SCCLINERUNNING is false then don't proceed
            XMLLineSpeed = new XMLLineSpeed();
            foreach (var xls in DataAccess.GetXMLLineSpeed())
            {
                FPM = xls.CurrentLineSpeed;
                IsRunning = xls.IsRunning;
            }

            if (IsRunning==false)
            {
                MessageBox.Show("Line is not running!");
            }
            else
            {
                //Loads currently running RollStands/Sides from Beckoff XML file
                //and stores them in an observable collection
                XMLRollStand = new ObservableCollection<XMLRollStand>();
                foreach (var xrs in DataAccess.GetXMLRollStand())
                {
                    XMLRollStand.Add(xrs);
                }

                //Loads a list of the most recent rolls ran at each rollstand
                //from BVP and stores it in an observable collection
                BVPRollStock = new ObservableCollection<Models.BVPRollStock>();
                foreach (var rstk in DataAccess.GetRunRollStock())
                {
                    BVPRollStock.Add(rstk);
                }

                //This query links the currently running rollstands and side running with the currently runnning rollstock
                var RollStock = from rs in BVPRollStock
                                from sampleRS in XMLRollStand.Where(x => rs.Stand == x.RollStand && rs.Side == x.RollStandSide)
                                orderby rs.Stand descending
                                select new { sampleRS.RollStand, sampleRS.RollStandSide, rs.Ticket_No, rs.Roll_Desc, rs.Roll_Width };

                //Takes results of the above RollStock query and stores it to the SampleRollStandModel

                SampleRollStands = new ObservableCollection<SampleRollStandModel>();
                foreach (var stk in RollStock)
                {     
                    SampleRollStands.Add(new SampleRollStandModel()
                    {
                        RollStandId = stk.RollStand,
                        Side = stk.RollStandSide,
                        RollNo = stk.Ticket_No,
                        RollDesc = stk.Roll_Desc,
                        RollWidth = Convert.ToDouble(stk.Roll_Width)
                    });
                }

                //Calls Method to sort SampleRollStands by RollStand in descending order
                SortRollStands();
            }
        }
        #endregion

        #region Sample Info Fields
        private ObservableCollection<PersonModel> _Persons;

        public ObservableCollection<PersonModel> Persons
        {
            get { return _Persons; }
            set
            {
                _Persons = value;
                NotifyOfPropertyChange();
            }
        }

        private ObservableCollection<BVPAdhesiveModel> _BVPAdhesives;

        public ObservableCollection<BVPAdhesiveModel> BVPAdhesives
        {
            get { return _BVPAdhesives; }
            set
            {
                _BVPAdhesives = value;
                NotifyOfPropertyChange();
            }
        }

        //private AdhesiveModel _SelectedAdhesives;

        //public AdhesiveModel SelectedAdhesives
        //{
        //    get { return _SelectedAdhesives; }
        //    set { _SelectedAdhesives = value; OnPropertyChanged(nameof(SelectedAdhesives)); }
        //}

        private DateTime? _CreateDateTime;

        public DateTime? CreateDateTime
        {
            get { return _CreateDateTime; }
            set
            {
                _CreateDateTime = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _PersonId;

        public int? PersonId
        {
            get { return _PersonId; }
            set
            {
                _PersonId = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _SampleNo;

        public int? SampleNo
        {
            get { return _SampleNo; }
            set
            {
                if(_SampleNo !=value)
                {
                    _SampleNo = value;
                    NotifyOfPropertyChange();
                    NotifyOfPropertyChange(nameof(CanDelete));
                }
            }
        }

        private int? _OrderNo;

        public int? OrderNo
        {
            get { return _OrderNo; }
            set
            {
                _OrderNo = value;
                NotifyOfPropertyChange();
            }
        }

        private string _TrialNo;

        public string TrialNo
        {
            get { return _TrialNo; }
            set
            {
                _TrialNo = value;
                NotifyOfPropertyChange();
            }
        }

        private string _Customer;

        public string Customer
        {
            get { return _Customer; }
            set
            {
                _Customer = value;
                NotifyOfPropertyChange();
            }
        }

        private double? _Caliper;

        public double? Caliper
        {
            get { return _Caliper; }
            set
            {
                _Caliper = value;
                NotifyOfPropertyChange();
            }
        }

        private string _Grade;

        public string Grade
        {
            get { return _Grade; }
            set
            {
                _Grade = value;
                NotifyOfPropertyChange();
            }
        }

        private double? _RunWidth;

        public double? RunWidth
        {
            get { return _RunWidth; }
            set
            {
                _RunWidth = value;
                NotifyOfPropertyChange();
            }
        }

        private double? _RunLength;

        public double? RunLength
        {
            get { return _RunLength; }
            set
            {
                _RunLength = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _NoOut;

        public int? NoOut
        {
            get { return _NoOut; }
            set
            {
                _NoOut = value;
                NotifyOfPropertyChange();
            }
        }

        private string _AdhesiveType;

        public string AdhesiveType
        {
            get { return _AdhesiveType; }
            set
            {
                _AdhesiveType = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _AdhesiveTemp;

        public int? AdhesiveTemp
        {
            get { return _AdhesiveTemp; }
            set
            {
                _AdhesiveTemp = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _FPM;

        public int? FPM
        {
            get { return _FPM; }
            set
            {
                _FPM = value;
                NotifyOfPropertyChange();
            }
        }

        private bool _IsValid;

        public bool IsValid
        {
            get { return _IsValid; }
            set
            {
                _IsValid = value;
                NotifyOfPropertyChange();
            }
        }
        #endregion

        #region RollStand Info Fields
        private ObservableCollection<SampleRollStandModel> _SampleRollStands = new ObservableCollection<SampleRollStandModel>();

        public ObservableCollection<SampleRollStandModel> SampleRollStands
        {
            get { return _SampleRollStands; }
            set
            {
                _SampleRollStands = value;
                NotifyOfPropertyChange();
            }
        }

        //private ObservableCollection<VMRollStandModel> _VMRollStands = new ObservableCollection<VMRollStandModel>();

        //public ObservableCollection<VMRollStandModel> SampleRollStands
        //{
        //    get { return _VMRollStands; }
        //    set { _VMRollStands = value; NotifyOfPropertyChange(nameof(SampleRollStands)); }
        //}

        //private SampleRollStandModel _SelectedSampleRollStands;

        //public SampleRollStandModel SelectedSampleRollStands
        //{
        //    get { return _SelectedSampleRollStands; }
        //    set { _SelectedSampleRollStands = value; OnPropertyChanged(nameof(SelectedSampleRollStands)); }
        //}

        private ObservableCollection<RollStandModel> _RollStand;

        public ObservableCollection<RollStandModel> RollStand
        {
            get { return _RollStand; }
            set
            {
                _RollStand = value;
                NotifyOfPropertyChange();
            }
        }

        private ObservableCollection<RollStandSideModel> _RollStandSide;

        public ObservableCollection<RollStandSideModel> RollStandSide
        {
            get { return _RollStandSide; }
            set
            {
                _RollStandSide = value;
                NotifyOfPropertyChange();
            }
        }

        private bool _IsRunning = false;

        public bool IsRunning
        {
            get { return _IsRunning; }
            set
            {
                _IsRunning = value;
                NotifyOfPropertyChange();
            }
        }

        private string _Stand;

        public string Stand
        {
            get { return _Stand; }
            set
            {
                _Stand = value;
                NotifyOfPropertyChange();
            }
        }

        private string _Side;

        public string Side
        {
            get { return _Side; }
            set
            {
                _Side = value;
                NotifyOfPropertyChange();
            }
        }

        private string _RollNo;

        public string RollNo
        {
            get { return _RollNo; }
            set
            {
                _RollNo = value;
                NotifyOfPropertyChange();
            }
        }

        private string _RollDesc;

        public string RollDesc
        {
            get { return _RollDesc; }
            set
            {
                _RollDesc = value;
                NotifyOfPropertyChange();
            }
        }

        private double? _RollWidth;

        public double? RollWidth
        {
            get { return _RollWidth; }
            set
            {
                _RollWidth = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _Tension;

        public int? Tension
        {
            get { return _Tension; }
            set
            {
                _Tension = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _Pressure;

        public int? Pressure
        {
            get { return _Pressure; }
            set
            {
                _Pressure = value;
                NotifyOfPropertyChange();
            }
        }
        #endregion

        #region Caliper Test Fields
        private double? _Caliper1;

        public double? Caliper1
        {
            get { return _Caliper1; }
            set
            {
                _Caliper1 = value;
                IsCaliperDirty = true;
                CalcCaliperAvg();
                NotifyOfPropertyChange();
            }
        }

        private double? _Caliper2;

        public double? Caliper2
        {
            get { return _Caliper2; }
            set
            {
                _Caliper2 = value;
                IsCaliperDirty = true;
                CalcCaliperAvg();
                NotifyOfPropertyChange();
            }
        }

        private double? _Caliper3;

        public double? Caliper3
        {
            get { return _Caliper3; }
            set
            {
                _Caliper3 = value;
                IsCaliperDirty = true;
                CalcCaliperAvg();
                NotifyOfPropertyChange();
            }
        }

        private double? _Caliper4;

        public double? Caliper4
        {
            get { return _Caliper4; }
            set
            {
                _Caliper4 = value;
                IsCaliperDirty = true;
                CalcCaliperAvg();
                NotifyOfPropertyChange();
            }
        }

        private double? _Caliper5;

        public double? Caliper5
        {
            get { return _Caliper5; }
            set
            {
                _Caliper5 = value;
                IsCaliperDirty = true;
                CalcCaliperAvg();
                NotifyOfPropertyChange();
            }
        }

        private double? _CaliperAvg;

        public double? CaliperAvg
        {
            get { return _CaliperAvg; }
            set
            {
                _CaliperAvg = value;
                NotifyOfPropertyChange();
            }
        }

        public bool IsCaliperDirty { get; set; }
        #endregion

        #region Burst Test Fields
        private int? _Burst1;

        public int? Burst1
        {
            get { return _Burst1; }
            set
            {
                _Burst1 = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _Burst2;

        public int? Burst2
        {
            get { return _Burst2; }
            set
            {
                _Burst2 = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _Burst3;

        public int? Burst3
        {
            get { return _Burst3; }
            set
            {
                _Burst3 = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _Burst4;

        public int? Burst4
        {
            get { return _Burst4; }
            set
            {
                _Burst4 = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _Burst5;

        public int? Burst5
        {
            get { return _Burst5; }
            set
            {
                _Burst5 = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _Burst6;

        public int? Burst6
        {
            get { return _Burst6; }
            set
            {
                _Burst6 = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _BurstAvg;

        public int? BurstAvg
        {
            get { return _BurstAvg; }
            set
            {
                _BurstAvg = value;
                NotifyOfPropertyChange();
            }
        }
        #endregion

        #region Measured Test Fields
        private int? _BasisWt;

        public int? BasisWt
        {
            get { return _BasisWt; }
            set
            {
                _BasisWt = value;
                NotifyOfPropertyChange();
            }
        }

        private double? _FinWidth;

        public double? FinWidth
        {
            get { return _FinWidth; }
            set
            {
                _FinWidth = value;
                NotifyOfPropertyChange();
            }
        }

        private double? _FinLength;

        public double? FinLength
        {
            get { return _FinLength; }
            set
            {
                _FinLength = value;
                NotifyOfPropertyChange();
            }
        }
        #endregion

        #region ZDT Test Fields
        private double? _ZDT1;

        public double? ZDT1
        {
            get { return _ZDT1; }
            set
            {
                _ZDT1 = value;
                NotifyOfPropertyChange();
            }
        }

        private double? _ZDT2;

        public double? ZDT2
        {
            get { return _ZDT2; }
            set
            {
                _ZDT2 = value;
                NotifyOfPropertyChange();
            }
        }

        private double? _ZDT3;

        public double? ZDT3
        {
            get { return _ZDT3; }
            set
            {
                _ZDT3 = value;
                NotifyOfPropertyChange();
            }
        }

        private double? _ZDTAvg;

        public double? ZDTAvg
        {
            get { return _ZDTAvg; }
            set
            {
                _ZDTAvg = value;
                NotifyOfPropertyChange();
            }
        }
        #endregion

        #region Tensile Test Fields
        private int? _MDTensile1;

        public int? MDTensile1
        {
            get { return _MDTensile1; }
            set
            {
                _MDTensile1 = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _MDTensile2;

        public int? MDTensile2
        {
            get { return _MDTensile2; }
            set
            {
                _MDTensile2 = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _MDTensile3;

        public int? MDTensile3
        {
            get { return _MDTensile3; }
            set
            {
                _MDTensile3 = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _MDTensileAvg;

        public int? MDTensileAvg
        {
            get { return _MDTensileAvg; }
            set
            {
                _MDTensileAvg = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _CDTensile1;

        public int? CDTensile1
        {
            get { return _CDTensile1; }
            set
            {
                _CDTensile1 = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _CDTensile2;

        public int? CDTensile2
        {
            get { return _CDTensile2; }
            set
            {
                _CDTensile2 = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _CDTensile3;

        public int? CDTensile3
        {
            get { return _CDTensile3; }
            set
            {
                _CDTensile3 = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _CDTensileAvg;

        public int? CDTensileAvg
        {
            get { return _CDTensileAvg; }
            set
            {
                _CDTensileAvg = value;
                NotifyOfPropertyChange();
            }
        }
        #endregion

        #region Moisture Content Test Fields
        private int? _FMCBefore;

        public int? FMCBefore
        {
            get { return _FMCBefore; }
            set
            {
                _FMCBefore = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _FMCAfter;

        public int? FMCAfter
        {
            get { return _FMCAfter; }
            set
            {
                _FMCAfter = value;
                NotifyOfPropertyChange();
            }
        }

        private double? _FMCAvg;

        public double? FMCAvg
        {
            get { return _FMCAvg; }
            set
            {
                _FMCAvg = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _MMCBefore;

        public int? MMCBefore
        {
            get { return _MMCBefore; }
            set
            {
                _MMCBefore = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _MMCAfter;

        public int? MMCAfter
        {
            get { return _MMCAfter; }
            set
            {
                _MMCAfter = value;
                NotifyOfPropertyChange();
            }
        }

        private double? _MMCAvg;

        public double? MMCAvg
        {
            get { return _MMCAvg; }
            set
            {
                _MMCAvg = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _BMCBefore;

        public int? BMCBefore
        {
            get { return _BMCBefore; }
            set
            {
                _BMCBefore = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _BMCAfter;

        public int? BMCAfter
        {
            get { return _BMCAfter; }
            set
            {
                _BMCAfter = value;
                NotifyOfPropertyChange();
            }
        }

        private double? _BMCAvg;

        public double? BMCAvg
        {
            get { return _BMCAvg; }
            set
            {
                _BMCAvg = value;
                NotifyOfPropertyChange();
            }
        }

        private double? _MCTotalAvg;

        public double? MCTotalAvg
        {
            get { return _MCTotalAvg; }
            set
            {
                _MCTotalAvg = value;
                NotifyOfPropertyChange();
            }
        }
        #endregion

        #region Water Immersion Test Fields
        private int? _FWIBefore;

        public int? FWIBefore
        {
            get { return _FWIBefore; }
            set
            {
                _FWIBefore = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _FWIAfter;

        public int? FWIAfter
        {
            get { return _FWIAfter; }
            set
            {
                _FWIAfter = value;
                NotifyOfPropertyChange();
            }
        }

        private double? _FWIAvg;

        public double? FWIAvg
        {
            get { return _FWIAvg; }
            set
            {
                _FWIAvg = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _MWIBefore;

        public int? MWIBefore
        {
            get { return _MWIBefore; }
            set
            {
                _MWIBefore = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _MWIAfter;

        public int? MWIAfter
        {
            get { return _MWIAfter; }
            set
            {
                _MWIAfter = value;
                NotifyOfPropertyChange();
            }
        }

        private double? _MWIAvg;

        public double? MWIAvg
        {
            get { return _MWIAvg; }
            set
            {
                _MWIAvg = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _BWIBefore;

        public int? BWIBefore
        {
            get { return _BWIBefore; }
            set
            {
                _BWIBefore = value;
                NotifyOfPropertyChange();
            }
        }

        private int? _BWIAfter;

        public int? BWIAfter
        {
            get { return _BWIAfter; }
            set
            {
                _BWIAfter = value;
                NotifyOfPropertyChange();
            }
        }

        private double? _BWIAvg;

        public double? BWIAvg
        {
            get { return _BWIAvg; }
            set
            {
                _BWIAvg = value;
                NotifyOfPropertyChange();
            }
        }

        private double? _WITotalAvg;

        public double? WITotalAvg
        {
            get { return _WITotalAvg; }
            set
            {
                _WITotalAvg = value;
                NotifyOfPropertyChange();
            }
        }

        private string _Notes;

        public string Notes
        {
            get { return _Notes; }
            set
            {
                _Notes = value;
                NotifyOfPropertyChange();
            }
        }
       #endregion


        public bool ValidateRollStand()
        {
            if (String.IsNullOrWhiteSpace(Stand) ||
                String.IsNullOrWhiteSpace(Side) ||
                String.IsNullOrWhiteSpace(RollNo) ||
                String.IsNullOrWhiteSpace(RollDesc))// ||
                //Int32.TryParse(Tension, out int tension) ||
                //Int32.TryParse(Pressure, out int pressure))
                return false;
            else
                return true;
        }

        public bool ValidateSampleInfo()
        {
            if (PersonId == null ||
                OrderNo == null ||
                String.IsNullOrWhiteSpace(Customer) ||
                Caliper == null ||
                String.IsNullOrWhiteSpace(Grade) ||
                RunWidth == null ||
                RunLength == null ||
                NoOut == null ||
                FPM == null ||
                String.IsNullOrWhiteSpace(AdhesiveType) ||
                AdhesiveTemp == null)
                return false;
            else
                return true;
        }

        #region Validate Required Fields
        public string Error => null;
        public string this[string propertyName]
        {
            get
            {
                string result = String.Empty;
                switch (propertyName)
                {
                    case nameof(PersonId):
                        if (PersonId == null)
                            result = "Taken By is required!";
                        break;
                    case nameof(OrderNo):
                        if (OrderNo == null)
                            result = "Order Number is required!";

                        else if (OrderNo <= 0)
                            result = "Order Number is not a valid number!";
                        break;
                    case nameof(Customer):
                        if (string.IsNullOrWhiteSpace(Customer))
                            result = "Customer Name is required!";
                        break;

                    case nameof(Caliper):
                        if (Caliper == null)
                            result = "Caliper is required!";
                        break;

                    case nameof(Grade):
                        if (string.IsNullOrWhiteSpace(Grade))
                            result = "Grade is required!";
                        break;

                    case nameof(RunWidth):
                        if (RunWidth == null)
                            result = "Run Width is required!";
                        break;

                    case nameof(RunLength):
                        if (RunLength == null)
                            result = "Run Length is required!";
                        break;

                    case nameof(NoOut):
                        if (NoOut == null)
                            result = "No Out is required!";
                        break;

                    case nameof(FPM):
                        if (FPM == null)
                            result = "FPM is required!";
                        break;

                    case nameof(AdhesiveType):
                        if (string.IsNullOrWhiteSpace(AdhesiveType))
                            result = "Adhesive Type is required!";
                        break;

                    case nameof(AdhesiveTemp):
                        if (AdhesiveTemp == null)
                            result = "Adhesive Temp is required!";
                        break;
                }
                return result;
            }
        }     
        #endregion
    }

 

    //public static class FocusManager
    //{
    //    public static bool SetFocus(this IViewAware screen, Expression<Func<object>> propertyExpression)
    //    {
    //        return SetFocus(screen, propertyExpression.GetMemberInfo().Name);
    //    }

    //    public static bool SetFocus(this IViewAware screen, string property)
    //    {
    //        Contract.Requires(property != null, "Property cannot be null.");
    //        var view = screen.GetView() as UserControl;
    //        if (view != null)
    //        {
    //            var control = FindChild(view, property);
    //            bool focus = control != null && control.Focus();
    //            return focus;
    //        }
    //        return false;
    //    }

    //    private static FrameworkElement FindChild(UIElement parent, string childName)
    //    {
    //        // Confirm parent and childName are valid. 
    //        if (parent == null || string.IsNullOrWhiteSpace(childName)) return null;

    //        FrameworkElement foundChild = null;

    //        int childrenCount = VisualTreeHelper.GetChildrenCount(parent);

    //        for (int i = 0; i < childrenCount; i++)
    //        {
    //            FrameworkElement child = VisualTreeHelper.GetChild(parent, i) as FrameworkElement;
    //            if (child != null)
    //            {

    //                BindingExpression bindingExpression = GetBindingExpression(child);
    //                if (child.Name == childName)
    //                {
    //                    foundChild = child;
    //                    break;
    //                }
    //                if (bindingExpression != null)
    //                {
    //                    if (bindingExpression.ResolvedSourcePropertyName == childName)
    //                    {
    //                        foundChild = child;
    //                        break;
    //                    }
    //                }
    //                foundChild = FindChild(child, childName);
    //                if (foundChild != null)
    //                {
    //                    if (foundChild.Name == childName)
    //                        break;
    //                    BindingExpression foundChildBindingExpression = GetBindingExpression(foundChild);
    //                    if (foundChildBindingExpression != null &&
    //                        foundChildBindingExpression.ResolvedSourcePropertyName == childName)
    //                        break;
    //                }

    //            }
    //        }

    //        return foundChild;
    //    }

    //    private static BindingExpression GetBindingExpression(FrameworkElement control)
    //    {
    //        if (control == null) return null;

    //        BindingExpression bindingExpression = null;
    //        var convention = ConventionManager.GetElementConvention(control.GetType());
    //        if (convention != null)
    //        {
    //            var bindablePro = convention.GetBindableProperty(control);
    //            if (bindablePro != null)
    //            {
    //                bindingExpression = control.GetBindingExpression(bindablePro);
    //            }
    //        }
    //        return bindingExpression;
    //    }
    //}
}
