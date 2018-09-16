using System.Windows;
using ZbW.Testing.Dms.Client.Model;

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
        private readonly Action _navigateBack;

        private string _benutzer;

        private string _bezeichnung;

        private DateTime _erfassungsdatum;

        private string _filePath;

        private bool _isRemoveFileEnabled;

        private string _selectedTypItem;

        private string _stichwoerter;

        private List<string> _typItems;

        private DateTime? _valutaDatum;

        private MetadataItem _metadataitems;


        public DocumentDetailViewModel(string benutzer, Action navigateBack)
        {
            _navigateBack = navigateBack;
            Benutzer = benutzer;
            Erfassungsdatum = DateTime.Now;
            TypItems = ComboBoxItems.Typ;

            CmdDurchsuchen = new DelegateCommand(OnCmdDurchsuchen);
            CmdSpeichern = new DelegateCommand(OnCmdSpeichern);
            _metadataitems = new MetadataItem();
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

        public DateTime? ValutaDatum
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

        private void OnCmdDurchsuchen()
        {
            var openFileDialog = new OpenFileDialog();
            var result = openFileDialog.ShowDialog();

            if (result.GetValueOrDefault())
            {
                _filePath = openFileDialog.FileName;
            }
        }

        private void OnCmdSpeichern()
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


            _metadataitems.AddFile(CreateMetadataItem(), _isRemoveFileEnabled);

            _navigateBack();
        }

        private Boolean isDocumentSelected()
        {
            return !String.IsNullOrEmpty(this._filePath);
        }


        private Boolean hasAllRequiredFieldSet()
        {
            return !String.IsNullOrEmpty(this.Bezeichnung) &&
                   this.ValutaDatum.HasValue &&
                   !String.IsNullOrEmpty(this.SelectedTypItem);
        }

        private MetadataItem CreateMetadataItem()
        {
            var metadataItem = new MetadataItem();
            metadataItem.User = Benutzer;
            metadataItem.Description = Bezeichnung;
            metadataItem.OriginalPath = this._filePath;
            metadataItem.IsRemoveFileEnabled = this.IsRemoveFileEnabled;
            metadataItem.Tag = this.Stichwoerter;
            metadataItem.Type = this.SelectedTypItem;
            metadataItem.ValutaDatum = (DateTime)this.ValutaDatum;
            metadataItem.AddingDate = DateTime.Now;

            return metadataItem;
        }
    }
}