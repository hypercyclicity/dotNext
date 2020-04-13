using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DotNext.Net.Cluster.Consensus.Raft.TransportServices
{
    using static Runtime.Intrinsics;

    internal partial class ServerExchange
    {
        private void BeginReceiveSnapshot(ReadOnlySpan<byte> input, EndPoint endPoint, CancellationToken token)
        {
            var snapshot = new ReceivedLogEntry(input, Reader, out var remotePort, out var senderTerm, out var snapshotIndex);
            ChangePort(ref endPoint, remotePort);
            task = server.ReceiveSnapshotAsync(endPoint, senderTerm, snapshot, snapshotIndex, token);
        }

        private async ValueTask<bool> ReceivingSnapshot(ReadOnlyMemory<byte> content, bool completed, CancellationToken token)
        {
            if (content.IsEmpty)
                completed = true;
            else
            {
                var result = await Writer.WriteAsync(content, token).ConfigureAwait(false);
                completed |= result.IsCompleted;
            }
            if (completed)
            {
                await Writer.CompleteAsync().ConfigureAwait(false);
                state = State.ReceivingSnapshotFinished;
            }
            return true;
        }

        private ValueTask<(PacketHeaders, int, bool)> RequestSnapshotChunk()
            => new ValueTask<(PacketHeaders, int, bool)>((new PacketHeaders(MessageType.Continue, FlowControl.Ack), 0, true));

        private async ValueTask<(PacketHeaders, int, bool)> EndReceiveSnapshot(Memory<byte> output)
        {
            var result = await Cast<Task<Result<bool>>>(Interlocked.Exchange(ref task, null)).ConfigureAwait(false);
            return (new PacketHeaders(MessageType.None, FlowControl.Ack), IExchange.WriteResult(result, output.Span) + sizeof(byte), false);
        }
    }
}