using System.Collections.Generic;
using System.ComponentModel;

namespace TocTocToc.Interfaces
{
    public interface IValidatable<T> : INotifyPropertyChanged
    {
        IList<IValidationRule<T>> Validations { get; }
        IList<string> Errors { get; set; }
        bool Validate();
        bool IsValid { get; set; }
    }
}
