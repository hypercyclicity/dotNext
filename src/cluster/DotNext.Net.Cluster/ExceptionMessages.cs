using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Resources;

namespace DotNext
{
    using static Resources.ResourceManagerExtensions;

    [ExcludeFromCodeCoverage]
    internal static class ExceptionMessages
    {
        private static readonly ResourceManager Resources = new ResourceManager("DotNext.ExceptionMessages", Assembly.GetExecutingAssembly());

        internal static string CannotRemoveLocalNode => (string)Resources.Get();

        internal static string EntrySetIsEmpty => (string)Resources.Get();

        internal static string LocalNodeNotLeader => (string)Resources.Get();

        internal static string InvalidEntryIndex(long index) => Resources.Get().Format(index);

        internal static string InvalidAppendIndex => (string)Resources.Get();

        internal static string SnapshotDetected => (string)Resources.Get();

        internal static string RangeTooBig => (string)Resources.Get();

        internal static string UnexpectedError => (string)Resources.Get();

        internal static string NoAvailableReadSessions => (string)Resources.Get();

        internal static string InvalidLockToken => (string)Resources.Get();

        internal static string UnsupportedAddressFamily => (string)Resources.Get();

        internal static string NotEnoughSenders => (string)Resources.Get();

        internal static string DuplicateCorrelationId => (string)Resources.Get();

        internal static string UnexpectedUdpSenderBehavior => (string)Resources.Get();

        internal static string ExchangeCompleted => (string)Resources.Get();

        internal static string CanceledByRemoteHost => (string)Resources.Get();

        internal static string UnavailableMember => (string)Resources.Get();

        internal static string UnresolvedLocalMember => (string)Resources.Get();
    }
}