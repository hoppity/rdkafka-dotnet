using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace RdKafka.Internal
{
    internal static class LibRdKafka
    {
        const long minVersion = 0x00090100;

#if NET451
        [DllImport("kernel32", SetLastError = true)]
        private static extern IntPtr LoadLibrary(string lpFileName);
#endif

        static LibRdKafka()
        {
#if NET451
            var is64 = IntPtr.Size == 8;
            try {
                LoadLibrary(is64 ? "x64/zlib.dll" : "x86/zlib.dll");
                LoadLibrary(is64 ? "x64/librdkafka.dll" : "x86/librdkafka.dll");
            }
            catch (Exception) { }
#endif

            // Warning: madness below, due to mono needing to load __Internal
            if (PlatformApis.IsDarwinMono)
            {
                _version = NativeDarwinMonoMethods.rd_kafka_version;
                _version_str = NativeDarwinMonoMethods.rd_kafka_version_str;
                _get_debug_contexts = NativeDarwinMonoMethods.rd_kafka_get_debug_contexts;
                _err2str = NativeDarwinMonoMethods.rd_kafka_err2str;
                _errno2err = NativeDarwinMonoMethods.rd_kafka_errno2err;
                _topic_partition_list_new = NativeDarwinMonoMethods.rd_kafka_topic_partition_list_new;
                _topic_partition_list_destroy = NativeDarwinMonoMethods.rd_kafka_topic_partition_list_destroy;
                _topic_partition_list_add = NativeDarwinMonoMethods.rd_kafka_topic_partition_list_add;
                _message_destroy = NativeDarwinMonoMethods.rd_kafka_message_destroy;
                _conf_new = NativeDarwinMonoMethods.rd_kafka_conf_new;
                _conf_destroy = NativeDarwinMonoMethods.rd_kafka_conf_destroy;
                _conf_dup = NativeDarwinMonoMethods.rd_kafka_conf_dup;
                _conf_set = NativeDarwinMonoMethods.rd_kafka_conf_set;
                _conf_set_dr_msg_cb = NativeDarwinMonoMethods.rd_kafka_conf_set_dr_msg_cb;
                _conf_set_rebalance_cb = NativeDarwinMonoMethods.rd_kafka_conf_set_rebalance_cb;
                _conf_set_offset_commit_cb = NativeDarwinMonoMethods.rd_kafka_conf_set_offset_commit_cb;
                _conf_set_log_cb = NativeDarwinMonoMethods.rd_kafka_conf_set_log_cb;
                _conf_set_stats_cb = NativeDarwinMonoMethods.rd_kafka_conf_set_stats_cb;
                _conf_set_default_topic_conf = NativeDarwinMonoMethods.rd_kafka_conf_set_default_topic_conf;
                _conf_get = NativeDarwinMonoMethods.rd_kafka_conf_get;
                _topic_conf_get = NativeDarwinMonoMethods.rd_kafka_topic_conf_get;
                _conf_dump = NativeDarwinMonoMethods.rd_kafka_conf_dump;
                _topic_conf_dump = NativeDarwinMonoMethods.rd_kafka_topic_conf_dump;
                _conf_dump_free = NativeDarwinMonoMethods.rd_kafka_conf_dump_free;
                _topic_conf_new = NativeDarwinMonoMethods.rd_kafka_topic_conf_new;
                _topic_conf_dup = NativeDarwinMonoMethods.rd_kafka_topic_conf_dup;
                _topic_conf_destroy = NativeDarwinMonoMethods.rd_kafka_topic_conf_destroy;
                _topic_conf_set = NativeDarwinMonoMethods.rd_kafka_topic_conf_set;
                _topic_conf_set_partitioner_cb = NativeDarwinMonoMethods.rd_kafka_topic_conf_set_partitioner_cb;
                _topic_partition_available = NativeDarwinMonoMethods.rd_kafka_topic_partition_available;
                _new = NativeDarwinMonoMethods.rd_kafka_new;
                _destroy = NativeDarwinMonoMethods.rd_kafka_destroy;
                _name = NativeDarwinMonoMethods.rd_kafka_name;
                _memberid = NativeDarwinMonoMethods.rd_kafka_memberid;
                _topic_new = NativeDarwinMonoMethods.rd_kafka_topic_new;
                _topic_destroy = NativeDarwinMonoMethods.rd_kafka_topic_destroy;
                _topic_name = NativeDarwinMonoMethods.rd_kafka_topic_name;
                _poll = NativeDarwinMonoMethods.rd_kafka_poll;
                _mem_free = NativeDarwinMonoMethods.rd_kafka_mem_free;
                _subscribe = NativeDarwinMonoMethods.rd_kafka_subscribe;
                _unsubscribe = NativeDarwinMonoMethods.rd_kafka_unsubscribe;
                _subscription = NativeDarwinMonoMethods.rd_kafka_subscription;
                _consumer_poll = NativeDarwinMonoMethods.rd_kafka_consumer_poll;
                _consumer_close = NativeDarwinMonoMethods.rd_kafka_consumer_close;
                _assign = NativeDarwinMonoMethods.rd_kafka_assign;
                _assignment = NativeDarwinMonoMethods.rd_kafka_assignment;
                _commit = NativeDarwinMonoMethods.rd_kafka_commit;
                _position = NativeDarwinMonoMethods.rd_kafka_position;
                _produce = NativeDarwinMonoMethods.rd_kafka_produce;
                _metadata = NativeDarwinMonoMethods.rd_kafka_metadata;
                _metadata_destroy = NativeDarwinMonoMethods.rd_kafka_metadata_destroy;
                _list_groups = NativeDarwinMonoMethods.rd_kafka_list_groups;
                _group_list_destroy = NativeDarwinMonoMethods.rd_kafka_group_list_destroy;
                _brokers_add = NativeDarwinMonoMethods.rd_kafka_brokers_add;
                _set_log_level = NativeDarwinMonoMethods.rd_kafka_set_log_level;
                _outq_len = NativeDarwinMonoMethods.rd_kafka_outq_len;
                _wait_destroyed = NativeDarwinMonoMethods.rd_kafka_wait_destroyed;
            }
            else
            {
                _version = NativeMethods.rd_kafka_version;
                _version_str = NativeMethods.rd_kafka_version_str;
                _get_debug_contexts = NativeMethods.rd_kafka_get_debug_contexts;
                _err2str = NativeMethods.rd_kafka_err2str;
                _errno2err = NativeMethods.rd_kafka_errno2err;
                _topic_partition_list_new = NativeMethods.rd_kafka_topic_partition_list_new;
                _topic_partition_list_destroy = NativeMethods.rd_kafka_topic_partition_list_destroy;
                _topic_partition_list_add = NativeMethods.rd_kafka_topic_partition_list_add;
                _message_destroy = NativeMethods.rd_kafka_message_destroy;
                _conf_new = NativeMethods.rd_kafka_conf_new;
                _conf_destroy = NativeMethods.rd_kafka_conf_destroy;
                _conf_dup = NativeMethods.rd_kafka_conf_dup;
                _conf_set = NativeMethods.rd_kafka_conf_set;
                _conf_set_dr_msg_cb = NativeMethods.rd_kafka_conf_set_dr_msg_cb;
                _conf_set_rebalance_cb = NativeMethods.rd_kafka_conf_set_rebalance_cb;
                _conf_set_offset_commit_cb = NativeMethods.rd_kafka_conf_set_offset_commit_cb;
                _conf_set_log_cb = NativeMethods.rd_kafka_conf_set_log_cb;
                _conf_set_stats_cb = NativeMethods.rd_kafka_conf_set_stats_cb;
                _conf_set_default_topic_conf = NativeMethods.rd_kafka_conf_set_default_topic_conf;
                _conf_get = NativeMethods.rd_kafka_conf_get;
                _topic_conf_get = NativeMethods.rd_kafka_topic_conf_get;
                _conf_dump = NativeMethods.rd_kafka_conf_dump;
                _topic_conf_dump = NativeMethods.rd_kafka_topic_conf_dump;
                _conf_dump_free = NativeMethods.rd_kafka_conf_dump_free;
                _topic_conf_new = NativeMethods.rd_kafka_topic_conf_new;
                _topic_conf_dup = NativeMethods.rd_kafka_topic_conf_dup;
                _topic_conf_destroy = NativeMethods.rd_kafka_topic_conf_destroy;
                _topic_conf_set = NativeMethods.rd_kafka_topic_conf_set;
                _topic_conf_set_partitioner_cb = NativeMethods.rd_kafka_topic_conf_set_partitioner_cb;
                _topic_partition_available = NativeMethods.rd_kafka_topic_partition_available;
                _new = NativeMethods.rd_kafka_new;
                _destroy = NativeMethods.rd_kafka_destroy;
                _name = NativeMethods.rd_kafka_name;
                _memberid = NativeMethods.rd_kafka_memberid;
                _topic_new = NativeMethods.rd_kafka_topic_new;
                _topic_destroy = NativeMethods.rd_kafka_topic_destroy;
                _topic_name = NativeMethods.rd_kafka_topic_name;
                _poll = NativeMethods.rd_kafka_poll;
                _mem_free = NativeMethods.rd_kafka_mem_free;
                _subscribe = NativeMethods.rd_kafka_subscribe;
                _unsubscribe = NativeMethods.rd_kafka_unsubscribe;
                _subscription = NativeMethods.rd_kafka_subscription;
                _consumer_poll = NativeMethods.rd_kafka_consumer_poll;
                _consumer_close = NativeMethods.rd_kafka_consumer_close;
                _assign = NativeMethods.rd_kafka_assign;
                _assignment = NativeMethods.rd_kafka_assignment;
                _commit = NativeMethods.rd_kafka_commit;
                _position = NativeMethods.rd_kafka_position;
                _produce = NativeMethods.rd_kafka_produce;
                _metadata = NativeMethods.rd_kafka_metadata;
                _metadata_destroy = NativeMethods.rd_kafka_metadata_destroy;
                _list_groups = NativeMethods.rd_kafka_list_groups;
                _group_list_destroy = NativeMethods.rd_kafka_group_list_destroy;
                _brokers_add = NativeMethods.rd_kafka_brokers_add;
                _set_log_level = NativeMethods.rd_kafka_set_log_level;
                _outq_len = NativeMethods.rd_kafka_outq_len;
                _wait_destroyed = NativeMethods.rd_kafka_wait_destroyed;
            }

            if ((long) version() < minVersion) {
                throw new FileLoadException($"Invalid librdkafka version {(long)version():x}, expected at least {minVersion:x}");
            }
        }

        [UnmanagedFunctionPointer(callingConvention: CallingConvention.Cdecl)]
        internal delegate void DeliveryReportCallback(
                IntPtr rk,
                /* const rd_kafka_message_t * */ ref rd_kafka_message rkmessage,
                IntPtr opaque);

        [UnmanagedFunctionPointer(callingConvention: CallingConvention.Cdecl)]
        internal delegate void CommitCallback(IntPtr rk,
                ErrorCode err,
                /* rd_kafka_topic_partition_list_t * */ IntPtr offsets,
                IntPtr opaque);

        [UnmanagedFunctionPointer(callingConvention: CallingConvention.Cdecl)]
        internal delegate void RebalanceCallback(IntPtr rk,
                ErrorCode err,
                /* rd_kafka_topic_partition_list_t * */ IntPtr partitions,
                IntPtr opaque);

        [UnmanagedFunctionPointer(callingConvention: CallingConvention.Cdecl)]
        internal delegate void LogCallback(IntPtr rk, int level, string fac, string buf);

        [UnmanagedFunctionPointer(callingConvention: CallingConvention.Cdecl)]
        internal delegate int StatsCallback(IntPtr rk, IntPtr json, UIntPtr json_len, IntPtr opaque);

        [UnmanagedFunctionPointer(callingConvention: CallingConvention.Cdecl)]
        internal delegate int PartitionerCallback(
            /* const rd_kafka_topic_t * */ IntPtr rkt,
            IntPtr keydata,
            UIntPtr keylen,
            int partition_cnt,
            IntPtr rkt_opaque,
            IntPtr msg_opaque);


        private static Func<IntPtr> _version;
        internal static IntPtr version() => _version();

        private static Func<IntPtr> _version_str;
        internal static IntPtr version_str() => _version_str();

        private static Func<IntPtr> _get_debug_contexts;
        internal static IntPtr get_debug_contexts() => _get_debug_contexts();

        private static Func<ErrorCode, IntPtr> _err2str;
        internal static IntPtr err2str(ErrorCode err) => _err2str(err);

        private static Func<IntPtr, IntPtr> _topic_partition_list_new;
        internal static IntPtr topic_partition_list_new(IntPtr size)
            => _topic_partition_list_new(size);

        private static Action<IntPtr> _topic_partition_list_destroy;
        internal static void topic_partition_list_destroy(IntPtr rkparlist)
            => _topic_partition_list_destroy(rkparlist);

        private static Func<IntPtr, string, int, IntPtr> _topic_partition_list_add;
        internal static IntPtr topic_partition_list_add(IntPtr rktparlist,
                string topic, int partition)
            => _topic_partition_list_add(rktparlist, topic, partition);

        private static Func<IntPtr, ErrorCode> _errno2err;
        internal static ErrorCode errno2err(IntPtr errno) => _errno2err(errno);

        private static Action<IntPtr> _message_destroy;
        internal static void message_destroy(IntPtr rkmessage) => _message_destroy(rkmessage);

        private static Func<SafeConfigHandle> _conf_new;
        internal static SafeConfigHandle conf_new() => _conf_new();

        private static Action<IntPtr> _conf_destroy;
        internal static void conf_destroy(IntPtr conf) => _conf_destroy(conf);

        private static Func<IntPtr, IntPtr> _conf_dup;
        internal static IntPtr conf_dup(IntPtr conf) => _conf_dup(conf);

        private static Func<IntPtr, string, string, StringBuilder, UIntPtr, ConfRes> _conf_set;
        internal static ConfRes conf_set(IntPtr conf, string name,
                string value, StringBuilder errstr, UIntPtr errstr_size)
            => _conf_set(conf, name, value, errstr, errstr_size);

        private static Action<IntPtr, DeliveryReportCallback> _conf_set_dr_msg_cb;
        internal static void conf_set_dr_msg_cb(IntPtr conf, DeliveryReportCallback dr_msg_cb)
            => _conf_set_dr_msg_cb(conf, dr_msg_cb);

        private static Action<IntPtr, RebalanceCallback> _conf_set_rebalance_cb;
        internal static void conf_set_rebalance_cb(IntPtr conf, RebalanceCallback rebalance_cb)
            => _conf_set_rebalance_cb(conf, rebalance_cb);

        private static Action<IntPtr, CommitCallback> _conf_set_offset_commit_cb;
        internal static void conf_set_offset_commit_cb(IntPtr conf, CommitCallback commit_cb)
            => _conf_set_offset_commit_cb(conf, commit_cb);

        private static Action<IntPtr, LogCallback> _conf_set_log_cb;
        internal static void conf_set_log_cb(IntPtr conf, LogCallback log_cb)
            => _conf_set_log_cb(conf, log_cb);

        private static Action<IntPtr, StatsCallback> _conf_set_stats_cb;
        internal static void conf_set_stats_cb(IntPtr conf, StatsCallback stats_cb)
            => _conf_set_stats_cb(conf, stats_cb);

        private static Action<IntPtr, IntPtr> _conf_set_default_topic_conf;
        internal static void conf_set_default_topic_conf(IntPtr conf, IntPtr tconf)
            => _conf_set_default_topic_conf(conf, tconf);

        private delegate ConfRes ConfGet(IntPtr conf, string name, StringBuilder dest,
                ref UIntPtr dest_size);
        private static ConfGet _conf_get;
        internal static ConfRes conf_get(IntPtr conf, string name,
                StringBuilder dest, ref UIntPtr dest_size)
            => _conf_get(conf, name, dest, ref dest_size);

        private static ConfGet _topic_conf_get;
        internal static ConfRes topic_conf_get(IntPtr conf, string name,
                StringBuilder dest, ref UIntPtr dest_size)
            => _topic_conf_get(conf, name, dest, ref dest_size);

        private delegate IntPtr ConfDump(IntPtr conf, out UIntPtr cntp);
        private static ConfDump _conf_dump;
        internal static IntPtr conf_dump(IntPtr conf, out UIntPtr cntp)
            => _conf_dump(conf, out cntp);

        private static ConfDump _topic_conf_dump;
        internal static IntPtr topic_conf_dump(IntPtr conf, out UIntPtr cntp)
            => _topic_conf_dump(conf, out cntp);

        private static Action<IntPtr, UIntPtr> _conf_dump_free;
        internal static void conf_dump_free(IntPtr arr, UIntPtr cnt)
            => _conf_dump_free(arr, cnt);

        private static Func<SafeTopicConfigHandle> _topic_conf_new;
        internal static SafeTopicConfigHandle topic_conf_new() => _topic_conf_new();

        private static Func<IntPtr, IntPtr> _topic_conf_dup;
        internal static IntPtr topic_conf_dup(IntPtr conf) => _topic_conf_dup(conf);

        private static Action<IntPtr> _topic_conf_destroy;
        internal static void topic_conf_destroy(IntPtr conf) => _topic_conf_destroy(conf);

        private static Func<IntPtr, string, string, StringBuilder, UIntPtr, ConfRes> _topic_conf_set;
        internal static ConfRes topic_conf_set(IntPtr conf, string name,
                string value, StringBuilder errstr, UIntPtr errstr_size)
            => _topic_conf_set(conf, name, value, errstr, errstr_size);

        private static Action<IntPtr, PartitionerCallback> _topic_conf_set_partitioner_cb;
        internal static void topic_conf_set_partitioner_cb(
                IntPtr topic_conf, PartitionerCallback partitioner_cb)
            => _topic_conf_set_partitioner_cb(topic_conf, partitioner_cb);

        private static Func<IntPtr, int, bool> _topic_partition_available;
        internal static bool topic_partition_available(IntPtr rkt, int partition)
            => _topic_partition_available(rkt, partition);

        private static Func<RdKafkaType, IntPtr, StringBuilder, UIntPtr, SafeKafkaHandle> _new;
        internal static SafeKafkaHandle kafka_new(RdKafkaType type, IntPtr conf,
                StringBuilder errstr, UIntPtr errstr_size)
            => _new(type, conf, errstr, errstr_size);

        private static Action<IntPtr> _destroy;
        internal static void destroy(IntPtr rk) => _destroy(rk);

        private static Func<IntPtr, IntPtr> _name;
        internal static IntPtr name(IntPtr rk) => _name(rk);

        private static Func<IntPtr, IntPtr> _memberid;
        internal static IntPtr memberid(IntPtr rk) => _memberid(rk);

        private static Func<IntPtr, string, IntPtr, SafeTopicHandle> _topic_new;
        internal static SafeTopicHandle topic_new(IntPtr rk, string topic, IntPtr conf)
            => _topic_new(rk, topic, conf);

        private static Action<IntPtr> _topic_destroy;
        internal static void topic_destroy(IntPtr rk) => _topic_destroy(rk);

        private static Func<IntPtr, IntPtr> _topic_name;
        internal static IntPtr topic_name(IntPtr rkt) => _topic_name(rkt);

        private static Func<IntPtr, IntPtr, IntPtr> _poll;
        internal static IntPtr poll(IntPtr rk, IntPtr timeout_ms) => _poll(rk, timeout_ms);

        private static Action<IntPtr, IntPtr> _mem_free;
        internal static void mem_free(IntPtr rk, IntPtr ptr)
            => _mem_free(rk, ptr);

        private static Func<IntPtr, IntPtr, ErrorCode> _subscribe;
        internal static ErrorCode subscribe(IntPtr rk, IntPtr topics) => _subscribe(rk, topics);

        private static Func<IntPtr, ErrorCode> _unsubscribe;
        internal static ErrorCode unsubscribe(IntPtr rk) => _unsubscribe(rk);

        private delegate ErrorCode Subscription(IntPtr rk, out IntPtr topics);
        private static Subscription _subscription;
        internal static ErrorCode subscription(IntPtr rk, out IntPtr topics)
            => _subscription(rk, out topics);

        private static Func<IntPtr, IntPtr, IntPtr> _consumer_poll;
        internal static IntPtr consumer_poll(IntPtr rk, IntPtr timeout_ms)
            => _consumer_poll(rk, timeout_ms);

        private static Func<IntPtr, ErrorCode> _consumer_close;
        internal static ErrorCode consumer_close(IntPtr rk) => _consumer_close(rk);

        private static Func<IntPtr, IntPtr, ErrorCode> _assign;
        internal static ErrorCode assign(IntPtr rk, IntPtr partitions)
            => _assign(rk, partitions);

        private delegate ErrorCode Assignment(IntPtr rk, out IntPtr topics);
        private static Assignment _assignment;
        internal static ErrorCode assignment(IntPtr rk, out IntPtr topics)
            => _assignment(rk, out topics);

        private static Func<IntPtr, IntPtr, bool, ErrorCode> _commit;
        internal static ErrorCode commit(IntPtr rk, IntPtr offsets, bool async)
            => _commit(rk, offsets, async);

        private static Func<IntPtr, IntPtr, IntPtr, ErrorCode> _position;
        internal static ErrorCode position(IntPtr rk, IntPtr partitions, IntPtr timeout_ms)
            => _position(rk, partitions, timeout_ms);

        private static Func<IntPtr, int, IntPtr, byte[], UIntPtr, byte[], UIntPtr,
                IntPtr, IntPtr> _produce;
        internal static IntPtr produce(
                IntPtr rkt,
                int partition,
                IntPtr msgflags,
                byte[] payload, UIntPtr len,
                byte[] key, UIntPtr keylen,
                IntPtr msg_opaque)
            => _produce(rkt, partition, msgflags, payload, len, key, keylen, msg_opaque);

        private delegate ErrorCode Metadata(IntPtr rk, bool all_topics,
                IntPtr only_rkt, out IntPtr metadatap, IntPtr timeout_ms);
        private static Metadata _metadata;
        internal static ErrorCode metadata(IntPtr rk, bool all_topics,
                IntPtr only_rkt, out IntPtr metadatap, IntPtr timeout_ms)
            => _metadata(rk, all_topics, only_rkt, out metadatap, timeout_ms);

        private static Action<IntPtr> _metadata_destroy;
        internal static void metadata_destroy(IntPtr metadata)
            => _metadata_destroy(metadata);

        private delegate ErrorCode ListGroups(IntPtr rk, string group,
                out IntPtr grplistp, IntPtr timeout_ms);
        private static ListGroups _list_groups;
        internal static ErrorCode list_groups(IntPtr rk, string group,
                out IntPtr grplistp, IntPtr timeout_ms)
            => _list_groups(rk, group, out grplistp, timeout_ms);

        private static Action<IntPtr> _group_list_destroy;
        internal static void group_list_destroy(IntPtr grplist)
            => _group_list_destroy(grplist);

        private static Func<IntPtr, string, IntPtr> _brokers_add;
        internal static IntPtr brokers_add(IntPtr rk, string brokerlist)
            => _brokers_add(rk, brokerlist);

        private static Action<IntPtr, IntPtr> _set_log_level;
        internal static void set_log_level(IntPtr rk, IntPtr level)
            => _set_log_level(rk, level);

        private static Func<IntPtr, IntPtr> _outq_len;
        internal static IntPtr outq_len(IntPtr rk) => _outq_len(rk);

        private static Func<IntPtr, IntPtr> _wait_destroyed;
        internal static IntPtr wait_destroyed(IntPtr timeout_ms)
            => _wait_destroyed(timeout_ms);

        private class NativeMethods
        {
            private const string DllName = "librdkafka";

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr rd_kafka_version();

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr rd_kafka_version_str();

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr rd_kafka_get_debug_contexts();

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr rd_kafka_err2str(ErrorCode err);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern ErrorCode rd_kafka_errno2err(IntPtr errno);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern /* rd_kafka_topic_partition_list_t * */ IntPtr
            rd_kafka_topic_partition_list_new(IntPtr size);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_topic_partition_list_destroy(
                    /* rd_kafka_topic_partition_list_t * */ IntPtr rkparlist);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern /* rd_kafka_topic_partition_t * */ IntPtr
            rd_kafka_topic_partition_list_add(
                    /* rd_kafka_topic_partition_list_t * */ IntPtr rktparlist,
                    string topic, int partition);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_message_destroy(
                    /* rd_kafka_message_t * */ IntPtr rkmessage);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern SafeConfigHandle rd_kafka_conf_new();

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_conf_destroy(IntPtr conf);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr rd_kafka_conf_dup(IntPtr conf);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern ConfRes rd_kafka_conf_set(
                    IntPtr conf,
                    [MarshalAs(UnmanagedType.LPStr)] string name,
                    [MarshalAs(UnmanagedType.LPStr)] string value,
                    StringBuilder errstr,
                    UIntPtr errstr_size);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_conf_set_dr_msg_cb(
                    IntPtr conf,
                    DeliveryReportCallback dr_msg_cb);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_conf_set_rebalance_cb(
                    IntPtr conf, RebalanceCallback rebalance_cb);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_conf_set_offset_commit_cb(
                    IntPtr conf, CommitCallback commit_cb);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_conf_set_log_cb(IntPtr conf, LogCallback log_cb);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_conf_set_stats_cb(IntPtr conf, StatsCallback stats_cb);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_conf_set_default_topic_conf(
                    IntPtr conf, IntPtr tconf);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern ConfRes rd_kafka_conf_get(
                    IntPtr conf,
                    [MarshalAs(UnmanagedType.LPStr)] string name,
                    StringBuilder dest, ref UIntPtr dest_size);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern ConfRes rd_kafka_topic_conf_get(
                    IntPtr conf,
                    [MarshalAs(UnmanagedType.LPStr)] string name,
                    StringBuilder dest, ref UIntPtr dest_size);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern /* const char ** */ IntPtr rd_kafka_conf_dump(
                    IntPtr conf, /* size_t * */ out UIntPtr cntp);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern /* const char ** */ IntPtr rd_kafka_topic_conf_dump(
                    IntPtr conf, out UIntPtr cntp);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_conf_dump_free(/* const char ** */ IntPtr arr, UIntPtr cnt);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern SafeTopicConfigHandle rd_kafka_topic_conf_new();

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern /* rd_kafka_topic_conf_t * */ IntPtr rd_kafka_topic_conf_dup(
                    /* const rd_kafka_topic_conf_t * */ IntPtr conf);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_topic_conf_destroy(IntPtr conf);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern ConfRes rd_kafka_topic_conf_set(
                    IntPtr conf,
                    [MarshalAs(UnmanagedType.LPStr)] string name,
                    [MarshalAs(UnmanagedType.LPStr)] string value,
                    StringBuilder errstr,
                    UIntPtr errstr_size);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_topic_conf_set_partitioner_cb(
                    IntPtr topic_conf, PartitionerCallback partitioner_cb);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool rd_kafka_topic_partition_available(
                    IntPtr rkt, int partition);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern SafeKafkaHandle rd_kafka_new(
                    RdKafkaType type, IntPtr conf,
                    StringBuilder errstr,
                    UIntPtr errstr_size);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_destroy(IntPtr rk);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern /* const char * */ IntPtr rd_kafka_name(IntPtr rk);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern /* char * */ IntPtr rd_kafka_memberid(IntPtr rk);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern SafeTopicHandle rd_kafka_topic_new(
                    IntPtr rk,
                    [MarshalAs(UnmanagedType.LPStr)] string topic,
                    /* rd_kafka_topic_conf_t * */ IntPtr conf);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_topic_destroy(IntPtr rk);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern /* const char * */ IntPtr rd_kafka_topic_name(IntPtr rkt);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr rd_kafka_poll(IntPtr rk, IntPtr timeout_ms);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_mem_free(IntPtr rk, IntPtr ptr);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern ErrorCode rd_kafka_subscribe(IntPtr rk,
                    /* const rd_kafka_topic_partition_list_t * */ IntPtr topics);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern ErrorCode rd_kafka_unsubscribe(IntPtr rk);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern ErrorCode rd_kafka_subscription(IntPtr rk,
                    /* rd_kafka_topic_partition_list_t ** */ out IntPtr topics);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern /* rd_kafka_message_t * */ IntPtr rd_kafka_consumer_poll(
                    IntPtr rk, IntPtr timeout_ms);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern ErrorCode rd_kafka_consumer_close(IntPtr rk);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern ErrorCode rd_kafka_assign(IntPtr rk,
                    /* const rd_kafka_topic_partition_list_t * */ IntPtr partitions);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern ErrorCode rd_kafka_assignment(IntPtr rk,
                    /* rd_kafka_topic_partition_list_t ** */ out IntPtr topics);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern ErrorCode rd_kafka_commit(
                    IntPtr rk,
                    /* const rd_kafka_topic_partition_list_t * */ IntPtr offsets,
                    bool async);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern ErrorCode rd_kafka_position(
                    IntPtr rk, IntPtr partitions, IntPtr timeout_ms);

            [DllImport(DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr rd_kafka_produce(
                    IntPtr rkt,
                    int partition,
                    IntPtr msgflags,
                    byte[] payload, UIntPtr len,
                    byte[] key, UIntPtr keylen,
                    IntPtr msg_opaque);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern ErrorCode rd_kafka_metadata(
                IntPtr rk, bool all_topics,
                /* rd_kafka_topic_t * */ IntPtr only_rkt,
                /* const struct rd_kafka_metadata ** */ out IntPtr metadatap,
                IntPtr timeout_ms);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_metadata_destroy(
                    /* const struct rd_kafka_metadata * */ IntPtr metadata);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern ErrorCode rd_kafka_list_groups(
                    IntPtr rk, string group, out IntPtr grplistp,
                    IntPtr timeout_ms);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_group_list_destroy(
                    IntPtr grplist);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr rd_kafka_brokers_add(IntPtr rk,
                    [MarshalAs(UnmanagedType.LPStr)] string brokerlist);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_set_log_level(IntPtr rk, IntPtr level);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr rd_kafka_outq_len(IntPtr rk);
            
            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr rd_kafka_wait_destroyed(IntPtr timeout_ms);
        }

        private class NativeDarwinMonoMethods
        {
            private const string DllName = "__Internal";

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr rd_kafka_version();

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr rd_kafka_version_str();

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr rd_kafka_get_debug_contexts();

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr rd_kafka_err2str(ErrorCode err);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern ErrorCode rd_kafka_errno2err(IntPtr errno);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern /* rd_kafka_topic_partition_list_t * */ IntPtr
            rd_kafka_topic_partition_list_new(IntPtr size);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_topic_partition_list_destroy(
                    /* rd_kafka_topic_partition_list_t * */ IntPtr rkparlist);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern /* rd_kafka_topic_partition_t * */ IntPtr
            rd_kafka_topic_partition_list_add(
                    /* rd_kafka_topic_partition_list_t * */ IntPtr rktparlist,
                    string topic, int partition);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_message_destroy(
                    /* rd_kafka_message_t * */ IntPtr rkmessage);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern SafeConfigHandle rd_kafka_conf_new();

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_conf_destroy(IntPtr conf);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr rd_kafka_conf_dup(IntPtr conf);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern ConfRes rd_kafka_conf_set(
                    IntPtr conf,
                    [MarshalAs(UnmanagedType.LPStr)] string name,
                    [MarshalAs(UnmanagedType.LPStr)] string value,
                    StringBuilder errstr,
                    UIntPtr errstr_size);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_conf_set_dr_msg_cb(
                    IntPtr conf,
                    DeliveryReportCallback dr_msg_cb);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_conf_set_rebalance_cb(
                    IntPtr conf, RebalanceCallback rebalance_cb);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_conf_set_offset_commit_cb(
                    IntPtr conf, CommitCallback commit_cb);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_conf_set_log_cb(IntPtr conf, LogCallback log_cb);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_conf_set_stats_cb(IntPtr conf, StatsCallback stats_cb);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_conf_set_default_topic_conf(
                    IntPtr conf, IntPtr tconf);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern ConfRes rd_kafka_conf_get(
                    IntPtr conf,
                    [MarshalAs(UnmanagedType.LPStr)] string name,
                    StringBuilder dest, ref UIntPtr dest_size);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern ConfRes rd_kafka_topic_conf_get(
                    IntPtr conf,
                    [MarshalAs(UnmanagedType.LPStr)] string name,
                    StringBuilder dest, ref UIntPtr dest_size);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern /* const char ** */ IntPtr rd_kafka_conf_dump(
                    IntPtr conf, /* size_t * */ out UIntPtr cntp);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern /* const char ** */ IntPtr rd_kafka_topic_conf_dump(
                    IntPtr conf, out UIntPtr cntp);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_conf_dump_free(/* const char ** */ IntPtr arr, UIntPtr cnt);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern SafeTopicConfigHandle rd_kafka_topic_conf_new();

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern /* rd_kafka_topic_conf_t * */ IntPtr rd_kafka_topic_conf_dup(
                    /* const rd_kafka_topic_conf_t * */ IntPtr conf);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_topic_conf_destroy(IntPtr conf);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern ConfRes rd_kafka_topic_conf_set(
                    IntPtr conf,
                    [MarshalAs(UnmanagedType.LPStr)] string name,
                    [MarshalAs(UnmanagedType.LPStr)] string value,
                    StringBuilder errstr,
                    UIntPtr errstr_size);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_topic_conf_set_partitioner_cb(
                    IntPtr topic_conf, PartitionerCallback partitioner_cb);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool rd_kafka_topic_partition_available(
                    IntPtr rkt, int partition);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern SafeKafkaHandle rd_kafka_new(
                    RdKafkaType type, IntPtr conf,
                    StringBuilder errstr,
                    UIntPtr errstr_size);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_destroy(IntPtr rk);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern /* const char * */ IntPtr rd_kafka_name(IntPtr rk);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern /* char * */ IntPtr rd_kafka_memberid(IntPtr rk);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern SafeTopicHandle rd_kafka_topic_new(
                    IntPtr rk,
                    [MarshalAs(UnmanagedType.LPStr)] string topic,
                    /* rd_kafka_topic_conf_t * */ IntPtr conf);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_topic_destroy(IntPtr rk);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern /* const char * */ IntPtr rd_kafka_topic_name(IntPtr rkt);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr rd_kafka_poll(IntPtr rk, IntPtr timeout_ms);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_mem_free(IntPtr rk, IntPtr ptr);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern ErrorCode rd_kafka_subscribe(IntPtr rk,
                    /* const rd_kafka_topic_partition_list_t * */ IntPtr topics);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern ErrorCode rd_kafka_unsubscribe(IntPtr rk);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern ErrorCode rd_kafka_subscription(IntPtr rk,
                    /* rd_kafka_topic_partition_list_t ** */ out IntPtr topics);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern /* rd_kafka_message_t * */ IntPtr rd_kafka_consumer_poll(
                    IntPtr rk, IntPtr timeout_ms);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern ErrorCode rd_kafka_consumer_close(IntPtr rk);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern ErrorCode rd_kafka_assign(IntPtr rk,
                    /* const rd_kafka_topic_partition_list_t * */ IntPtr partitions);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern ErrorCode rd_kafka_assignment(IntPtr rk,
                    /* rd_kafka_topic_partition_list_t ** */ out IntPtr topics);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern ErrorCode rd_kafka_commit(
                    IntPtr rk,
                    /* const rd_kafka_topic_partition_list_t * */ IntPtr offsets,
                    bool async);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern ErrorCode rd_kafka_position(
                    IntPtr rk, IntPtr partitions, IntPtr timeout_ms);

            [DllImport(DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr rd_kafka_produce(
                    IntPtr rkt,
                    int partition,
                    IntPtr msgflags,
                    byte[] payload, UIntPtr len,
                    byte[] key, UIntPtr keylen,
                    IntPtr msg_opaque);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern ErrorCode rd_kafka_metadata(
                IntPtr rk, bool all_topics,
                /* rd_kafka_topic_t * */ IntPtr only_rkt,
                /* const struct rd_kafka_metadata ** */ out IntPtr metadatap,
                IntPtr timeout_ms);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_metadata_destroy(
                    /* const struct rd_kafka_metadata * */ IntPtr metadata);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern ErrorCode rd_kafka_list_groups(
                    IntPtr rk, string group, out IntPtr grplistp,
                    IntPtr timeout_ms);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_group_list_destroy(
                    IntPtr grplist);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr rd_kafka_brokers_add(IntPtr rk,
                    [MarshalAs(UnmanagedType.LPStr)] string brokerlist);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void rd_kafka_set_log_level(IntPtr rk, IntPtr level);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr rd_kafka_outq_len(IntPtr rk);

            [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr rd_kafka_wait_destroyed(IntPtr timeout_ms);
        }
    }
}
