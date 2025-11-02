using MOTP.Model;

namespace MOTP.ViewModel
{
    class SharapovoVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
        public string ProductAvailability
        {
            get { return _pageModel.SharapovoStatus; }
            set { _pageModel.SharapovoStatus = value; OnPropertyChanged(); }
        }

        public SharapovoVM()
        {
            _pageModel = new PageModel();
            ProductAvailability = "Out of Stock";
        }
    }
}
