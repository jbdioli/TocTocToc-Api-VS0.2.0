using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace TocTocToc.Shared;

public class RxNetReplaySubject<T> : ISubject<T>
{
    private readonly ReplaySubject<IObservable<T>> _subjects;
    private readonly IObservable<T> _concatenatedSubjects;
    private ISubject<T> _subject;

    public RxNetReplaySubject()
    {
        _subjects = new ReplaySubject<IObservable<T>>(1);
        _concatenatedSubjects = _subjects.Concat();
        _subject = new ReplaySubject<T>();
        _subjects.OnNext(_subject);
    }

    public void Clear()
    {
        _subject.OnCompleted();
        _subject = new ReplaySubject<T>();
        _subjects.OnNext(_subject);
    }

    public void OnCompleted()
    {
        _subject.OnCompleted();
        _subjects.OnCompleted();
        _subject = new Subject<T>();
    }

    public void OnError(Exception error)
    {
        _subject.OnError(error);
    }

    public void OnNext(T value)
    {
        _subject.OnNext(value);
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
        return _concatenatedSubjects.Subscribe(observer);
    }
}