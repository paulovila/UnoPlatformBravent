using System;
using UnoMvvm;

namespace UnoWebApiSwagger.ViewModels
{
    public class ErrorEvent : PubSubEvent<Exception> { }
}