using System.Diagnostics.CodeAnalysis;

namespace ZbW.Testing.Dms.Client.Repositories
{
    using System.Collections.Generic;

    [ExcludeFromCodeCoverage]
    internal class ComboBoxItems
    {
        public static List<string> Typ =>
            new List<string>
                {
                    "Verträge",
                    "Quittungen"
                };
    }
}