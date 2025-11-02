using MOTP.Model;

namespace MOTP.ViewModel
{
    class HelkovskayVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
    public string ProductAvailability
    {
        get { return _pageModel.HelkovskayStatus; }
        set { _pageModel.HelkovskayStatus = value; OnPropertyChanged(); }
    }

    public HelkovskayVM()
    {
        _pageModel = new PageModel();
        ProductAvailability = "Out of Stock";
    }
}
}
