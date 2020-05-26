using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfDemo.PauseAndResumeTask
{
    /// <summary>
    /// https://stackoverflow.com/questions/19613444/a-pattern-to-pause-resume-an-async-task
    /// </summary>
    public class PauseTokenSource
    {
        bool _paused = false;
        bool _pauseRequested = false;

        TaskCompletionSource<bool> _resumeRequestTcs;
        TaskCompletionSource<bool> _pauseConfirmationTcs;

        readonly SemaphoreSlim _stateAsyncLock = new SemaphoreSlim(1);
        readonly SemaphoreSlim _pauseRequestAsyncLock = new SemaphoreSlim(1);

        public PauseToken Token { get { return new PauseToken(this); } }

        /// <summary>
        /// 是否停止
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> IsPaused(CancellationToken token = default(CancellationToken))
        {
            await _stateAsyncLock.WaitAsync(token);
            try
            {
                return _paused;
            }
            finally
            {
                _stateAsyncLock.Release();
            }
        }

        /// <summary>
        /// 继续
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task ResumeAsync(CancellationToken token = default(CancellationToken))
        {
            await _stateAsyncLock.WaitAsync(token);
            try
            {
                if (!_paused)
                {
                    return;
                }

                await _pauseRequestAsyncLock.WaitAsync(token);
                try
                {
                    var resumeRequestTcs = _resumeRequestTcs;
                    _paused = false;
                    _pauseRequested = false;
                    _resumeRequestTcs = null;
                    _pauseConfirmationTcs = null;
                    resumeRequestTcs.TrySetResult(true);
                }
                finally
                {
                    _pauseRequestAsyncLock.Release();
                }
            }
            finally
            {
                _stateAsyncLock.Release();
            }
        }

        /// <summary>
        /// 请求暂停
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task PauseAsync(CancellationToken token = default(CancellationToken))
        {
            await _stateAsyncLock.WaitAsync(token);
            try
            {
                if (_paused)
                {
                    return;
                }

                Task pauseConfirmationTask = null;

                await _pauseRequestAsyncLock.WaitAsync(token);
                try
                {
                    _pauseRequested = true;
                    //Note that the RunContinuationsAsynchronously member is available in the TaskCreationOptions enumeration starting with the .NET Framework 4.6.
                    _resumeRequestTcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
                    _pauseConfirmationTcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
                    pauseConfirmationTask = WaitForPauseConfirmationAsync(token);
                }
                finally
                {
                    _pauseRequestAsyncLock.Release();
                }

                await pauseConfirmationTask;

                _paused = true;
            }
            finally
            {
                _stateAsyncLock.Release();
            }
        }

        private async Task WaitForResumeRequestAsync(CancellationToken token)
        {
            using (token.Register(() => _resumeRequestTcs.TrySetCanceled(), useSynchronizationContext: false))
            {
                await _resumeRequestTcs.Task;
            }
        }

        private async Task WaitForPauseConfirmationAsync(CancellationToken token)
        {
            using (token.Register(() => _pauseConfirmationTcs.TrySetCanceled(), useSynchronizationContext: false))
            {
                await _pauseConfirmationTcs.Task;
            }
        }

        /// <summary>
        /// 如果请求暂停，则停止
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        internal async Task PauseIfRequestedAsync(CancellationToken token = default(CancellationToken))
        {
            Task resumeRequestTask = null;

            await _pauseRequestAsyncLock.WaitAsync(token);
            try
            {
                if (!_pauseRequested)
                {
                    return;
                }
                resumeRequestTask = WaitForResumeRequestAsync(token);
                _pauseConfirmationTcs.TrySetResult(true);
            }
            finally
            {
                _pauseRequestAsyncLock.Release();
            }

            await resumeRequestTask;
        }
    }

}
