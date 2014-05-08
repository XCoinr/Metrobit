using System;
using System.Collections.Generic;
using com.google.bitcoin.core;
using GalaSoft.MvvmLight;
using java.math;
using java.util;
using Metrobit.Shell.Models;
using Metrobit.Shell.Utils;

namespace Metrobit.Shell.ViewModel
{
    public class TransactionViewModel : ViewModelBase
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Transaction _transaction;
        private readonly MetrobitWalletAppKit _appKit;

        public TransactionViewModel(Transaction transaction, MetrobitWalletAppKit appKit)
        {
            _transaction = transaction;
            _appKit = appKit;

            Credit = _transaction.getValueSentToMe(_appKit.wallet());

            try
            {
                Debit = _transaction.getValueSentFromMe(_appKit.wallet());
            }
            catch (ScriptException exception)
            {
                _log.Error(exception);
            }

            var transactionInputs = _transaction.getInputs();
            var transactionOutputs = _transaction.getOutputs();

            if (transactionInputs != null)
            {
                var firstInput = transactionInputs.get(0) as TransactionInput;
                if (firstInput != null)
                {
                    TransactionOutput myOutput = null;
                    TransactionOutput theirOutput = null;

                    if (transactionOutputs != null) 
                    {
                        for (int i = 0; i < transactionOutputs.size(); i++)
                        {
                            var transactionOutput = transactionOutputs.get(i) as TransactionOutput;
                            
                            if (transactionOutput != null && transactionOutput.isMine(perWalletModelData.getWallet())) {
                    myOutput = transactionOutput;
                }
                if (transactionOutput != null && !transactionOutput.isMine(perWalletModelData.getWallet())) {
                    theirOutput = transactionOutput;
                }
                        }
                
            }
        }

        if (credit != null && credit.compareTo(BigInteger.ZERO) > 0) {
            // Credit.
            try {
                String addressString = "";

                if (myOutput != null) {
                    Address toAddress = new Address(getNetworkParameters(), myOutput
                            .getScriptPubKey().getPubKeyHash());
                    addressString = toAddress.toString();
                }

                String label = null;
                if (perWalletModelData.getWalletInfo() != null) {
                    label = perWalletModelData.getWalletInfo().lookupLabelForReceivingAddress(addressString);
                }
                if (label != null && !label.equals("")) {
                    toReturn = controller.getLocaliser().getString("multiBitModel.creditDescriptionWithLabel",
                            new Object[]{addressString, label});
                } else {
                    toReturn = controller.getLocaliser().getString("multiBitModel.creditDescription",
                            new Object[]{addressString});
                }
            } catch (ScriptException e) {
                log.error(e.getMessage(), e);

            }
        }

        if (debit != null && debit.compareTo(BigInteger.ZERO) > 0) {
            // Debit.
            try {
                // See if the address is a known sending address.
                if (theirOutput != null) {
                    String addressString = theirOutput.getScriptPubKey().getToAddress(getNetworkParameters()).toString();
                    String label = null;
                    if (perWalletModelData.getWalletInfo() != null) {
                        label = perWalletModelData.getWalletInfo().lookupLabelForSendingAddress(addressString);
                    }
                    if (label != null && !label.equals("")) {
                        toReturn = controller.getLocaliser().getString("multiBitModel.debitDescriptionWithLabel",
                                new Object[]{addressString, label});
                    } else {
                        toReturn = controller.getLocaliser().getString("multiBitModel.debitDescription",
                                new Object[]{addressString});
                    }
                }
            } catch (ScriptException e) {
                log.error(e.getMessage(), e);
            }
        }
        return toReturn;
                }
            }
        }

        public TransactionConfidence.ConfidenceType ConfidenceType
        {
            get { return _transaction.getConfidence().getConfidenceType(); }
        }

        public int Depth
        {
            get { return _transaction.getConfidence().getDepthInBlocks(); }
        }

        public DateTime Timestamp
        {
            get { return _transaction.getUpdateTime().ToDateTime(); }
        }

        public BigInteger Credit { get; private set; }

        public BigInteger Debit { get; private set; }

        public string Description { get; private set; }
    }
}
