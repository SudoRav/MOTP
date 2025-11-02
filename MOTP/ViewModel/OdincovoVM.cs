using MOTP.Model;

namespace MOTP.ViewModel
{
    class OdincovoVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
    public string ProductAvailability
    {
        get { return _pageModel.OdincovoStatus; }
        set { _pageModel.OdincovoStatus = value; OnPropertyChanged(); }
    }

    public OdincovoVM()
    {
        _pageModel = new PageModel();
        ProductAvailability = "Out of Stock";
    }
}
}
