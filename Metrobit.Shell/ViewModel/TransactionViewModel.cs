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

                            if (transactionOutput != null && transactionOutput.isMine(_appKit.wallet()))
                            {
                                myOutput = transactionOutput;
                            }

                            if (transactionOutput != null && !transactionOutput.isMine(_appKit.wallet()))
                            {
                                theirOutput = transactionOutput;
                            }
                        }
                    }

                    string toReturn = "";

                    if (Credit != null && Credit.compareTo(BigInteger.ZERO) > 0)
                    {
                        // Credit.
                        try
                        {
                            String addressString = "";

                            if (myOutput != null)
                            {
                                var toAddress = new Address(_appKit.@params(),
                                    myOutput.getScriptPubKey().getPubKeyHash());

                                addressString = toAddress.toString();
                            }

                            String label = null;

                            label = _appKit.GetReceivingAddressLabel(addressString);

                            if (label != null && !string.IsNullOrWhiteSpace(label))
                            {
                                toReturn = "Credit to '" + label + "'";
                            }
                            else
                            {
                                toReturn = "Credit to '" + addressString + "'";
                            }
                        }
                        catch (ScriptException e)
                        {
                            _log.Error(e.getMessage(), e);
                        }
                    }

                    if (Debit != null && Debit.compareTo(BigInteger.ZERO) > 0) 
                    {
                        // Debit.
                        try 
                        {
                            // See if the address is a known sending address.
                            if (theirOutput != null) 
                            {
                                String addressString = theirOutput.getScriptPubKey().getToAddress(_appKit.@params()).toString();
                                String label = null;
                    
                                label = _appKit.GetSendingAddressLabel(addressString);
                                
                                
                                if (label != null && !string.IsNullOrWhiteSpace(label)) 
                                {
                                    toReturn = "Sent to '" + label + "'";
                                } 
                                else 
                                {
                                    toReturn = "Sent to '" + addressString + "'";
                                }
                            }
                        } 
                        catch (ScriptException e) 
                        {
                            _log.Error(e.getMessage(), e);
                        }
                    }
                    
                    Description = toReturn;    
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
