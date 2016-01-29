﻿using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using YandexDisk.Client.Clients;
using YandexDisk.Client.Protocol;

namespace YandexDisk.Client.Http.Clients
{
    internal class CommandsClient : DiskClientBase, ICommandsClient
    {
        internal CommandsClient(ApiContext apiContext)
            : base(apiContext)
        { }

        public Task<Link> CreateDictionaryAsync(string path, CancellationToken cancellationToken)
        {
            return PutAsync<object, object, Link>("resources", new { path }, /*requestBody*/ null, cancellationToken);
        }

        public Task<Link> CopyAsync(CopyFileRequest request, CancellationToken cancellationToken)
        {
            return PostAsync<CopyFileRequest, object, Link>("resources/copy", request, /*requestBody*/ null, cancellationToken);
        }

        public Task<Link> MoveAsync(MoveFileRequest request, CancellationToken cancellationToken)
        {
            return PostAsync<CopyFileRequest, object, Link>("resources/move", request, /*requestBody*/ null, cancellationToken);
        }

        public Task<Link> DeleteAsync(DeleteFileRequest request, CancellationToken cancellationToken)
        {
            return DeleteAsync<CopyFileRequest, object, Link>("resources", request, /*requestBody*/ null, cancellationToken);
        }

        public Task<Link> EmptyTrashAsync(string path, CancellationToken cancellationToken)
        {
            return DeleteAsync<object, object, Link>("trash/resources", new { path }, /*requestBody*/ null, cancellationToken);
        }

        public Task<Link> RestoreFromTrashAsync(RestoreFromTrashRequest request, CancellationToken cancellationToken)
        {
            return PutAsync<RestoreFromTrashRequest, object, Link>("trash/resources", request, /*requestBody*/ null, cancellationToken);
        }

        public async Task<Operation> GetOperationStatus(Link link, CancellationToken cancellationToken)
        {
            var url = new Uri(link.Href);

            var method = new HttpMethod(link.Method);

            var requestMessage = new HttpRequestMessage(method, url);

            HttpResponseMessage responseMessage = await SendAsync(requestMessage, cancellationToken);

            Operation operation = await ReadResponse<Operation>(responseMessage, cancellationToken);

            if (operation == null)
            {
                throw new Exception("Unexpected empty result.");
            }

            return operation;
        }
    }
}
