using System.Windows;
using ZbW.Testing.Dms.Client.Model;
using ZbW.Testing.Dms.Client.Services;

namespace ZbW.Testing.Dms.Client.ViewModels
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Win32;

    using Prism.Commands;
    using Prism.Mvvm;

    using ZbW.Testing.Dms.Client.Repositories;

    internal class DocumentDetailViewModel : BindableBase
    {
        private FileTestable _fileTestable = new FileTestable();

        private readonly Action _navigateBack;

        private string _benutzer;

        private string _bezeichnung;

        private DateTime _erfassungsdatum;

        public string _filePath { get; set; }

        private bool _isRemoveFileEnabled;

        private string _selectedTypItem;

        private string _stichwoerter;

        private List<string> _typItems;

        private DateTime _valutaDatum;

        private MetadataItem _metadataitems;

        private SerializeTestable _serializeTestable;


        public DocumentDetailViewModel(string benutzer, Action navigateBack)
        {
            _navigateBack = navigateBack;
            Benutzer = benutzer;
            Erfassungsdatum = DateTime.Now;
            ValutaDatum = DateTime.Today;
            TypItems = ComboBoxItems.Typ;
            _serializeTestable = new SerializeTestable();

            CmdDurchsuchen = new DelegateCommand(OnCmdDurchsuchen);
            CmdSpeichern = new DelegateCommand(OnCmdSpeichern);
            _metadataitems = new MetadataItem(_serializeTestable);
    }

        public string Stichwoerter
        {
            get
            {
                return _stichwoerter;
            }

            set
            {
                SetProperty(ref _stichwoerter, value);
            }
        }

        public string Bezeichnung
        {
            get
            {
                return _bezeichnung;
            }

            set
            {
                SetProperty(ref _bezeichnung, value);
            }
        }

        public List<string> TypItems
        {
            get
            {
                return _typItems;
            }

            set
            {
                SetProperty(ref _typItems, value);
            }
        }

        public string SelectedTypItem
        {
            get
            {
                return _selectedTypItem;
            }

            set
            {
                SetProperty(ref _selectedTypItem, value);
            }
        }

        public DateTime Erfassungsdatum
        {
            get
            {
                return _erfassungsdatum;
            }

            set
            {
                SetProperty(ref _erfassungsdatum, value);
            }
        }

        public string Benutzer
        {
            get
            {
                return _benutzer;
            }

            set
            {
                SetProperty(ref _benutzer, value);
            }
        }

        public DelegateCommand CmdDurchsuchen { get; }

        public DelegateCommand CmdSpeichern { get; }

        public DateTime ValutaDatum
        {
            get
            {
                return _valutaDatum;
            }

            set
            {
                SetProperty(ref _valutaDatum, value);
            }
        }

        public bool IsRemoveFileEnabled
        {
            get
            {
                return _isRemoveFileEnabled;
            }

            set
            {
                SetProperty(ref _isRemoveFileEnabled, value);
            }
        }

        internal void OnCmdDurchsuchen()
        {
           
            _fileTestable.OpenFileDialog();
            var result = _fileTestable.ShowDialog();
            _filePath = result;
        }

        internal void OnCmdSpeichern()
        {
            if (!this.hasAllRequiredFieldSet())
            {
                MessageBox.Show("Es müssen alle Pflichtfelder ausgefüllt werden!");
                return;
            }

            if (!this.isDocumentSelected())
            {
                MessageBox.Show("Bitte ein Dokument auswählen.");
                return;
            }


            _metadataitems.AddFile(CreateMetadataItem(_serializeTestable), _isRemoveFileEnabled);

            _navigateBack();
        }

        public Boolean isDocumentSelected()
        {
            return !String.IsNullOrEmpty(this._filePath);
        }


        public Boolean hasAllRequiredFieldSet()
        {
            return !String.IsNullOrEmpty(this.Bezeichnung) &&
                   this.ValutaDatum != null &&
                   !String.IsNullOrEmpty(this.SelectedTypItem);
        }

        public MetadataItem CreateMetadataItem(SerializeTestable serializeTestable)
        {
            var metadataItem = new MetadataItem(serializeTestable);
            var valutadatum = ValutaDatum;
            var addingdate = DateTime.Now;
            var stichwoerter = this.Stichwoerter;
            if (stichwoerter == null)
            {
                stichwoerter = "";
            }
            return metadataItem.Create(Benutzer, Bezeichnung, this._filePath, this.IsRemoveFileEnabled, stichwoerter, this.SelectedTypItem, valutadatum, addingdate);
            
        }
    }
}