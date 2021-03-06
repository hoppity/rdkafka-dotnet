using System;
using System.Collections.Generic;
using RdKafka.Internal;

namespace RdKafka
{
    public class Config
    {
        internal readonly SafeConfigHandle handle;

        public Config()
        {
            handle = SafeConfigHandle.Create();
        }

        public Dictionary<string, string> Dump() => handle.Dump();

        public string this[string name]
        {
            set
            {
                handle.Set(name, value);
            }
            get
            {
                return handle.Get(name);
            }
        }

        // Helpers for common config options
        public string GroupId
        {
            set { this["group.id"] = value; }
            get { return this["group.id"]; }
        }

        public bool EnableAutoCommit
        {
            set { this["enable.auto.commit"] = value ? "true" : "false"; }
            get { return this["enable.auto.commit"] == "true"; }
        }

        public delegate void LogCallback(string handle, int level, string fac, string buf);
        public LogCallback Logger { get; set; }

        public TimeSpan StatisticsInterval
        {
            set { this["statistics.interval.ms"] = ((int) value.TotalMilliseconds).ToString(); }
            get { return TimeSpan.FromMilliseconds(int.Parse(this["statistics.interval.ms"])); }
        }

        /// <summary>
        /// Sets the default topic configuration to use for automatically
        /// subscribed topics (e.g., through pattern-matched topics).
        /// </summary>
        public TopicConfig DefaultTopicConfig { get; set; }
    }
}
