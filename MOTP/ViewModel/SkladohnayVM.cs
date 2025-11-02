using MOTP.Model;

namespace MOTP.ViewModel
{
    class SkladohnayVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
    public string ProductAvailability
    {
        get { return _pageModel.SkladohnayStatus; }
        set { _pageModel.SkladohnayStatus = value; OnPropertyChanged(); }
    }

    public SkladohnayVM()
    {
        _pageModel = new PageModel();
        ProductAvailability = "Out of Stock";
    }
}
}
