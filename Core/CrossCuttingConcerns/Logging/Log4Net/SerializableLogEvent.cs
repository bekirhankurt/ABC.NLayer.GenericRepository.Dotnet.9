using log4net.Core;

namespace Core.CrossCuttingConcerns.Logging.Log4Net;

public class SerializableLogEvent(LoggingEvent loggingEvent)
{
    public object Message => loggingEvent.MessageObject;
}