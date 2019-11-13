using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace CurrencyConverter.Service.Connector
{
    public abstract class BaseProxyService<T> : IDisposable where T : class
    {
        private readonly string _serviceEndpoint;
        private readonly object _locker = new object();
        private IChannelFactory<T> _channelFactory;
        private T _channel;
        private bool _disposed = false;

        public BaseProxyService(string serviceEndpoint)
        {
            _serviceEndpoint = serviceEndpoint;
        }

        protected T Channel 
        {
            get 
            {
                Initialize();
                return _channel;
            }
        }

        protected void CloseChannel() 
        {
            if (_channel != null) 
            {
                ((ICommunicationObject)_channel).Close();
            }
        }

        private void Initialize() 
        {
            lock (_locker) 
            {
                if (_channel != null) 
                {
                    return;
                }

                _channelFactory = new ChannelFactory<T>(new NetTcpBinding());
                _channel = _channelFactory.CreateChannel(new EndpointAddress(_serviceEndpoint));
            }
        }

        ~BaseProxyService() 
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposed) 
        {
            if (_disposed) 
            {
                return;
            }
            if (disposed) 
            {
                lock (_locker) 
                {
                    CloseChannel();

                    if (_channelFactory != null) 
                    {
                        ((IDisposable)_channelFactory).Dispose();
                    }

                    _channel = null;
                    _channelFactory = null;
                }
            }

            _disposed = true;
        }
    }
}
