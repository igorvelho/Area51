using System;
using System.Collections.Generic;
using System.Text;
using MicroServices.Infrastructure.RavenDB.Transformer;
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
                    Urls = new[] { "http://10.0.75.2:8080" },
                    Database = "DBTest"
                };
               
                store.Initialize();

                new ProductViewModelIndex().Execute(store);

                return store;
            });

        public static IDocumentStore Store =>
            LazyStore.Value;

    }
}
