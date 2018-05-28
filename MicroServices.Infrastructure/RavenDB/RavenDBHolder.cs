using System;
using System.Collections.Generic;
using System.Text;
using Raven.Client.Documents;

namespace MicroServices.Infrastructure.RavenDB
{
    public static class RavenDBHolder
    {
        private static readonly Lazy<IDocumentStore> LazyStore =
            new Lazy<IDocumentStore>(() =>
            {
                var store = new DocumentStore
                {
                    Urls = new[] { "http://localhost:8080" },
                    Database = "DBTest"
                };

                return store.Initialize();
            });

        public static IDocumentStore Store =>
            LazyStore.Value;
    }
}
