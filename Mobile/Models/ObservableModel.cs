using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mobile.Models
{
    /// <summary>
    /// Base class for models that need to implement INotifyPropertyChanged
    /// </summary>
    public abstract class ObservableModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
