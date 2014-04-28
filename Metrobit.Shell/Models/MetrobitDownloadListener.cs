using com.google.bitcoin.core;
using java.util;

namespace Metrobit.Shell.Models
{
    class MetrobitDownloadListener : DownloadListener
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override void doneDownload()
        {
            _log.Info("Download done");
            base.doneDownload();
        }

        protected override void progress(double pct, int blocksSoFar, Date date)
        {
            _log.InfoFormat("% Complete: {0}%, Blocks so far: {1}, Date: {2}", pct, blocksSoFar, date.toString());
            base.progress(pct, blocksSoFar, date);
        }

        protected override void startDownload(int blocks)
        {
            _log.InfoFormat("Start download of {0} blocks", blocks);
            base.startDownload(blocks);
        }

        public override void onBlocksDownloaded(Peer peer, Block block, int blocksLeft)
        {
            _log.InfoFormat("Block downloaded. Peer: {0}, Block: {1}, Blocks left: {2}", peer.toString(), block.toString(), blocksLeft);
            base.onBlocksDownloaded(peer, block, blocksLeft);
        }

        public override void onChainDownloadStarted(Peer peer, int blocksLeft)
        {
            _log.InfoFormat("Chain download started. Peer: {0}, Blocks left:{1}", peer.toString(), blocksLeft);
            base.onChainDownloadStarted(peer, blocksLeft);
        }

        public override void onPeerConnected(Peer peer, int peerCount)
        {
            _log.InfoFormat("Peer connected. Peer: {0}, Peer Count:{1}", peer.toString(), peerCount);
            base.onPeerConnected(peer, peerCount);
        }

        public override void onPeerDisconnected(Peer peer, int peerCount)
        {
            _log.InfoFormat("Peer disconnected. Peer: {0}, Peer Count:{1}", peer.toString(), peerCount);
            base.onPeerDisconnected(peer, peerCount);
        }

        public override Message onPreMessageReceived(Peer peer, Message m)
        {
            _log.InfoFormat("PreMessage received. Peer: {0}, Message: {1}", peer.toString(), m.toString());
            return base.onPreMessageReceived(peer, m);
        }

        public override void onTransaction(Peer peer, Transaction t)
        {
            _log.InfoFormat("New transaction. Peer: {0}, Transaction: {1}", peer.toString(), t.toString());
            base.onTransaction(peer, t);
        }
    }
}
