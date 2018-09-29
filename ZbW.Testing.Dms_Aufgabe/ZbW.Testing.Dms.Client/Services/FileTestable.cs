using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Win32;


namespace ZbW.Testing.Dms.Client.Services
{
    [ExcludeFromCodeCoverage]
    internal class FileTestable
    {
        private OpenFileDialog _openFileDialog;
        public virtual void OpenFileDialog()
        {
            _openFileDialog = new OpenFileDialog();         
        }

        public virtual String ShowDialog()
        {
            var result = _openFileDialog.ShowDialog();

            if (result.GetValueOrDefault())
            {
                return _openFileDialog.FileName;
            }

            return "";

        }
    }
}
