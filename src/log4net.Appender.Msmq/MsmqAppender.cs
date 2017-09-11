#region Copyright & License
//
// Copyright 2001-2005 The Apache Software Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
// http://svn.apache.org/viewvc/logging/log4net/trunk/examples/net/1.0/Appenders/SampleAppendersApp/cs/src/Appender/?pathrev=312291
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;

namespace log4net.Appender.Msmq
{
    /// <summary>
    /// Appender writes to a Microsoft Message Queue
    /// </summary>
    /// <remarks>
    /// This appender sends log events via a specified MSMQ.
    /// The queue specified in the QueueName (e.g. .\Private$\log-test) must already exist on
    /// the source machine.
    /// The message label and body are rendered using separate layouts.
    /// </remarks>
    public class MsmqAppender : AppenderSkeleton
    {
        private MessageQueue _queue;
        private string _queueName;
        private log4net.Layout.PatternLayout _labelLayout;

        public MsmqAppender()
        {
        }

        public string QueueName
        {
            get { return _queueName; }
            set { _queueName = value; }
        }

        public log4net.Layout.PatternLayout LabelLayout
        {
            get { return _labelLayout; }
            set { _labelLayout = value; }
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            if (_queue == null)
            {
                _queue = new MessageQueue(_queueName);
            }

            if (_queue != null)
            {
                var message = new Message();
                message.Label = RenderLabel(loggingEvent);

                using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                {
                    var writer = new System.IO.StreamWriter(stream, new System.Text.UTF8Encoding(false, true));
                    base.RenderLoggingEvent(writer, loggingEvent);
                    writer.Flush();
                    stream.Position = 0;
                    message.BodyStream = stream;

                    _queue.Send(message);
                }
            }
        }

        private string RenderLabel(LoggingEvent loggingEvent)
        {
            if (_labelLayout == null)
            {
                return null;
            }

            var writer = new System.IO.StringWriter();
            _labelLayout.Format(writer, loggingEvent);

            return writer.ToString();
        }
    }
}
