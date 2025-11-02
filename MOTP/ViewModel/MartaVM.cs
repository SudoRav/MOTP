using MOTP.Model;

namespace MOTP.ViewModel
{
    class MartaVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
    public string ProductAvailability
    {
        get { return _pageModel.MartaStatus; }
        set { _pageModel.MartaStatus = value; OnPropertyChanged(); }
    }

    public MartaVM()
    {
        _pageModel = new PageModel();
        ProductAvailability = "Out of Stock";
    }
}
}
